using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    public LifeBar lifeBar;
    public BPBar bpBar;
    public RectTransform attackButtonContainer;
    public TMP_Text monsterName;
    public GameObject attackButtonPrefab;
    private Monster _monster;
    // Start is called before the first frame update
    public void InitializeUI()
    {
        _playerManager = GetComponent<PlayerManager>();
        GetMonsterInfo();
        if(attackButtonContainer)
            FillOutAttacks();
    }

    public void GetMonsterInfo()
    {
        _monster = _playerManager.activeMonster.GetComponent<Monster>();
        lifeBar.lifeSystem = _monster.gameObject.GetComponent<LifeSystem>();
        lifeBar.StartUp();
        monsterName.text = _monster.monsterName;
        
        if (bpBar)
        {
            bpBar.bpSystem = _monster.gameObject.GetComponent<BPSystem>();
            bpBar.StartUp();
        }
    }

    public void FillOutAttacks()
    {
        foreach (RectTransform child in attackButtonContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _monster.attacks.Length; i++)
        {
            var attackButton = Instantiate(attackButtonPrefab, attackButtonContainer).GetComponent<AttackButton>();
            attackButton.SetUp(_monster.attacks[i], _playerManager);
        }
    }

    public void SetActiveAttackBox(bool active)
    {
        attackButtonContainer.gameObject.SetActive(active);
    }
}
