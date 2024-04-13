using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public PlayerManager localPlayer;
    public PlayerManager opponent;
    public List<Attack> attacks = new List<Attack>();

    public UnityEvent onSendInfo;
    public UnityEvent onReceiveInfo;

    private Attack _localAttack;
    private Attack _opponentAttack;

    private void Awake()
    {
        AttacksManager.Instance = new AttacksManager(attacks);
    }

    public void SendAttack(Attack attack)
    {
        var index = AttacksManager.Instance.Attacks.IndexOf(attack);
        _localAttack = attack;
        onSendInfo.Invoke();
    }

    private void ReceiveAttack(int index)
    {
        var attack = AttacksManager.Instance.Attacks[index];
        _opponentAttack = attack;
        if (localPlayer.playerState.currentState == PlayerState.State.Waiting)
        {
            //Local Player
            localPlayer.playerState.ChangeState(PlayerState.State.WaitingHit);
            
            onReceiveInfo.Invoke();
            
            //Opponent
            opponent.activeMonster.GetComponent<Animator>().SetTrigger(attack.animation.ToString());
            Instantiate(attack.attackerFX, opponent.activeMonster.transform);
        }
    }

    public void ReceiveFX()
    {
        var localMonster = localPlayer.activeMonster.GetComponent<Monster>();
        var opponentMonster = opponent.activeMonster.GetComponent<Monster>();
        
        opponentMonster.AddAttack(_opponentAttack.playerAttack);
        opponentMonster.AddDefense(_opponentAttack.playerDefense);
        
        localMonster.SubtractAttack(_opponentAttack.enemyAttack);
        localMonster.SubtractDefense(_opponentAttack.enemyDefense);

        var damage = AttacksManager.Instance.EvalDamage(_opponentAttack, opponentMonster, localMonster);
        
        Instantiate(_opponentAttack.hitFX, localPlayer.activeMonster.transform);
            
        localPlayer.activeMonster.GetComponent<Animator>().SetTrigger("Hit");
        localPlayer.activeMonster.GetComponent<LifeSystem>().SubtractLife(damage);
    }

    public void SendFX()
    {
        var localMonster = localPlayer.activeMonster.GetComponent<Monster>();
        var opponentMonster = opponent.activeMonster.GetComponent<Monster>();
        
        localMonster.AddAttack(_localAttack.playerAttack);
        localMonster.AddDefense(_localAttack.playerDefense);
        
        opponentMonster.SubtractAttack(_localAttack.enemyAttack);
        opponentMonster.SubtractDefense(_localAttack.enemyDefense);

        var damage = AttacksManager.Instance.EvalDamage(_localAttack, localMonster, opponentMonster);
        Instantiate(_localAttack.hitFX, opponent.activeMonster.transform);
        
        opponent.activeMonster.GetComponent<Animator>().SetTrigger("Hit");
        opponent.activeMonster.GetComponent<LifeSystem>().SubtractLife(damage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveAttack(Random.Range(0, 19));
        }
    }
}
