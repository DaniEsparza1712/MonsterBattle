using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Attack attack;
    public TMP_Text text;
    private PlayerManager _playerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(Attack newAttack, PlayerManager manager)
    {
        _playerManager = manager;
        SetAttack(newAttack);
        SetAction();
    }
    
    public void SetAttack(Attack newAttack)
    {
        attack = newAttack;
        text.text = $"{attack.attackName}: {attack.bpCost} BP";
    }

    private void SetAction()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(ExecuteButton);
    }

    private void ExecuteButton()
    {
        var bpSystem = _playerManager.activeMonster.GetComponent<BPSystem>();
        if (bpSystem.currentBP >= attack.bpCost)
        {
            PlayAnim();
            ProcessAttackData();
            ChangeState();
            
            bpSystem.SubtractBP(attack.bpCost);
        }
    }

    private void ProcessAttackData()
    {
        var opponent = _playerManager.opponent;
        var opponentLife = opponent.activeMonster.GetComponent<LifeSystem>();
    
        opponentLife.SubtractLife(attack.baseDamage);
    }

    private void ChangeState()
    {
        _playerManager.playerState.ChangeState(PlayerState.State.Attacking);
    }

    private void PlayAnim()
    {
        var animator = _playerManager.activeMonster.GetComponent<Animator>();
        Instantiate(attack.attackerFX, animator.gameObject.transform);
        if(attack.animation == Attack.AnimationType.Attack)
            animator.SetTrigger("Attack");
        else
            animator.SetTrigger("Pump");
    }
}
