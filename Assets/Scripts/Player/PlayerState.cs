using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : MonoBehaviour
{
    public enum State
    {
        Selecting,
        Attacking,
        AttackingIdle,
        Waiting,
        GettingHit
    }
    public State currentState;
    public UnityEvent onChangeToSelecting;
    public UnityEvent onChangeToAttacking;
    public UnityEvent onChangeToAttackingIdle;
    public UnityEvent onChangeToWaiting;
    public UnityEvent onChangeGettingHit;
    [Header("State Vars")] 
    private float _timer = 0;
    public float attackingTime;

    public void ChangeState(State state)
    {
        switch (state)
        {
            case State.Selecting:
                currentState = State.Selecting;
                onChangeToSelecting.Invoke();
                break;
            case State.Attacking:
                currentState = State.Attacking;
                _timer = 0;
                onChangeToAttacking.Invoke();
                break;
            case State.AttackingIdle:
                currentState = State.AttackingIdle;
                onChangeToAttackingIdle.Invoke();
                break;
            case State.Waiting:
                currentState = State.Waiting;
                onChangeToWaiting.Invoke();
                break;
            case State.GettingHit:
                currentState = State.GettingHit;
                onChangeGettingHit.Invoke();
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Attacking)
        {
            _timer += Time.deltaTime;
            if(_timer >= attackingTime)
                ChangeState(State.Selecting);
        }
        
    }
}
