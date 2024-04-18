using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [Header("Current Monster")] 
    public Transform spawnPoint;
    public GameObject activeMonster;
    private int _currentIndex;
    [Header("Info")] 
    public List<GameObject> monsters = new List<GameObject>();
    public PlayerState playerState;
    public UnityEvent changeMonsterEvent;
    [Header("Opponent")] 
    public PlayerManager opponent;
    
    public void InitializePlayerManager()
    {
        _currentIndex = 0;
        activeMonster = Instantiate(monsters[_currentIndex], spawnPoint);
    }

    public void SetMonsters(GameObject[] monstersToSet)
    {
        monsters.Clear();
        foreach (var monster in monstersToSet)
        {
            monsters.Add(monster);
        }
    }

    public void ManageMonsterDeath()
    {
        if (activeMonster.GetComponent<LifeSystem>().currentLife == 0)
        {
            if(_currentIndex < monsters.Count - 1)
                ChangeMonster();
            else
            {
                activeMonster.GetComponent<Animator>().SetTrigger("Die");
                playerState.ChangeState(PlayerState.State.End);
                opponent.playerState.ChangeState(PlayerState.State.End);
            }
        }
    }
    
    public void ChangeMonster()
    {
        activeMonster.SetActive(false);
        _currentIndex++;
        activeMonster = Instantiate(monsters[_currentIndex], spawnPoint);
        changeMonsterEvent.Invoke();
    }
}
