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
        WaitingHit,
        GettingHit,
        End
    }
    public State currentState;
    public UnityEvent onChangeToSelecting;
    public UnityEvent onChangeToAttacking;
    public UnityEvent onChangeToAttackingIdle;
    public UnityEvent onChangeToWaiting;
    public UnityEvent onChangeToWaitingHit;
    public UnityEvent onChangeGettingHit;
    public UnityEvent onEnd;
    [Header("State Vars")] 
    [SerializeField]
    private float _timer = 0;
    public float attackingTime;

    public void ChangeState(State state)
    {
        _timer = 0;
        switch (state)
        {
            case State.Selecting:
                currentState = State.Selecting;
                onChangeToSelecting.Invoke();
                break;
            case State.Attacking:
                currentState = State.Attacking;
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
            case State.WaitingHit:
                currentState = State.WaitingHit;
                onChangeToWaitingHit.Invoke();
                break;
            case State.GettingHit:
                currentState = State.GettingHit;
                onChangeGettingHit.Invoke();
                break;
            case State.End:
                currentState = State.End;
                onEnd.Invoke();
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
        if (currentState == State.Attacking || currentState == State.AttackingIdle || currentState == State.WaitingHit || currentState == State.GettingHit)
        {
            _timer += Time.deltaTime;
            if (_timer >= attackingTime)
            {
                switch (currentState)
                {
                    case State.Attacking:
                        ChangeState(State.AttackingIdle);
                        break;
                    case State.AttackingIdle:
                        ChangeState(State.Waiting);
                        break;
                    case State.WaitingHit:
                        ChangeState(State.GettingHit);
                        break;
                    case State.GettingHit:
                        ChangeState(State.Selecting);
                        break;
                }
            }
        }
    }
}
