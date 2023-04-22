using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : AbstractController
{
    
    private Vector2 _moveDir;
    private Vector2 _lookDir;
    private bool _hit;
    
    private enum State
    {
        Idle,
        Moving,
        Attack
    }

    private State _currentState;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _currentState = State.Moving;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Idle:
                makeIdle();
                break;

            case State.Moving:
                makeMove();
                break;

            case State.Attack:
                break;
        }
    }

    private void makeIdle()
    {
        _moveDir = Vector2.zero;
    }

    private void makeMove()
    {
        _moveDir = new Vector2(1, 0);

        _lookDir = _moveDir;
    }

    public override Vector2 GetDir()
    {
        return _moveDir;
    }

    public override Vector2 GetAim()
    {
        return _lookDir;
    }

    public override bool GetFire()
    {
        return _hit;
    }
}
