using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : AbstractController
{
    
    private Vector2 _moveDir;
    private Vector2 _lookDir;
    private bool _hit;
    private System.Random _rand;
    
    private enum State
    {
        Idle,
        Roaming,
        Charge,
        Attack
    }

    private State _currentState;

    private void Start()
    {
        _rand = new System.Random();
    }

    private void OnEnable()
    {
        _currentState = State.Roaming;
        InvokeRepeating("RandomizeState", 0, 1f);
    }

    private void OnDisable()
    {
        if (IsInvoking("RandomizeState")) CancelInvoke("RandomizeState");
    }

    private void RandomizeState()
    {
        int rng = _rand.Next(0, 5);
        Debug.Log(rng);
        if (rng > 0)
        {
            _currentState = State.Roaming;
            switch (rng)
            {
                case 1:
                    _moveDir = new Vector2(1, 0);
                    break;

                case 2:
                    _moveDir = new Vector2(-1, 0);
                    break;

                case 3:
                    _moveDir = new Vector2(0, 1);
                    break;

                case 4:
                    _moveDir = new Vector2(0, -1);
                    break;
            }
        }
        else
        {
            _currentState = State.Idle;
        }
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Idle:
                makeIdle();
                break;

            case State.Roaming:
                makeMove();
                break;

            case State.Charge:
                makeCharge();
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
        _lookDir = _moveDir;
    }

    private void makeCharge()
    {
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 chargeDir = (Vector2.zero - thisPos).normalized;

        _moveDir = chargeDir;
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
