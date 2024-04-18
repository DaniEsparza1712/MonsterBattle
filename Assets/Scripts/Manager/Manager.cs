using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public Server server;
    public Client client;
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

    public void ReceiveAttackThread()
    {
        int playerType = PlayerPrefs.GetInt("Type", 1);
        StartCoroutine(playerType == 0 ? server.ReceiveThread() : client.ReceiveThread());
    }

    public void OpponentSendAttackFX()
    {
        int playerType = PlayerPrefs.GetInt("Type", 1);
        var attack = AttacksManager.Instance.Attacks[playerType == 0 ? server.receivedValue : client.receivedValue];
        var opponentMonster = opponent.activeMonster;
        opponentMonster.GetComponent<Animator>().SetTrigger(attack.animation.ToString());
        Instantiate(attack.attackerFX, opponent.transform);
    }

    public void ReceiveFX()
    {
        int playerType = PlayerPrefs.GetInt("Type", 1);
        _opponentAttack = AttacksManager.Instance.Attacks[playerType == 0 ? server.receivedValue : client.receivedValue];
        
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
        int playerType = PlayerPrefs.GetInt("Type", 1);

        localMonster.AddAttack(_localAttack.playerAttack);
        localMonster.AddDefense(_localAttack.playerDefense);
        
        opponentMonster.SubtractAttack(_localAttack.enemyAttack);
        opponentMonster.SubtractDefense(_localAttack.enemyDefense);

        var damage = AttacksManager.Instance.EvalDamage(_localAttack, localMonster, opponentMonster);
        Instantiate(_localAttack.hitFX, opponent.activeMonster.transform);
        
        opponent.activeMonster.GetComponent<Animator>().SetTrigger("Hit");
        opponent.activeMonster.GetComponent<LifeSystem>().SubtractLife(damage);
        
        //Send to Client
        if (playerType == 0)
        {
            StartCoroutine(server.SendThread(AttacksManager.Instance.Attacks.IndexOf(_localAttack)));
        }
        else
        {
            StartCoroutine(client.SendThread(AttacksManager.Instance.Attacks.IndexOf(_localAttack)));
        }
    }

    private void Update()
    {

    }
}
