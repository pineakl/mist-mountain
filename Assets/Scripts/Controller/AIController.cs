using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : AbstractController
{
    [SerializeField] private UnitData _targetData;
    [SerializeField] private DataHolder _selfData;

    private Vector2 _moveDir;
    private Vector2 _lookDir;
    private bool _isometric;
    private bool _hitWindow;
    private bool _hit;
    private System.Random _rand;

    private Vector2 _thisPos;
    private Vector2 _targetPos;

    private enum State
    {
        Approach,
        Idle,
        Roaming,
        Charge,
        Attack,
        Flee,
        Dying
    }

    private State _currentState;

    private void Start()
    {
        _rand = new System.Random();
        _isometric = true;
    }

    private void OnEnable()
    {
        _currentState = State.Approach;
    }

    private void OnDisable()
    {
        if (IsInvoking("RandomizeState")) CancelInvoke("RandomizeState");
        if (IsInvoking("HitOnce")) CancelInvoke("HitOnce");
    }

    private void RandomizeState()
    {
        int rng = _rand.Next(0, 5);
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
        //  Position Tracking
        _thisPos = new Vector2(transform.position.x, transform.position.z);
        _targetPos = new Vector2(_targetData.UnitPosition.x, _targetData.UnitPosition.z);
        
        //  State Machine
        switch (_currentState)
        {
            case State.Approach:
                makeApproach();
                break;

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
                makeAttack();
                break;

            case State.Flee:
                makeFlee();
                break;

            case State.Dying:
                makeDying();
                break;
        }
    }

    private void LateUpdate()
    {
        _hit = false;
    }

    private void makeApproach()
    {
        if (_isometric) _isometric = false;

        Vector2 approachDir = (_targetPos - _thisPos).normalized;

        _moveDir = approachDir;

        if ((_targetPos - _thisPos).magnitude < 10f)
        {
            _currentState = State.Idle;
            InvokeRepeating("RandomizeState", 0, 1f);
        }

        CheckFleeExit();
        CheckDyingExit();
    }

    private void makeIdle()
    {
        if (!_isometric) _isometric = true;
        _moveDir = Vector2.zero;

        CheckApproachExit();
        CheckChargeExit();
        CheckFleeExit();
        CheckDyingExit();
    }

    private void makeMove()
    {
        if (!_isometric) _isometric = true;
        _lookDir = _moveDir;

        CheckApproachExit();
        CheckChargeExit();
        CheckFleeExit();
        CheckDyingExit();
    }

    private void makeCharge()
    {
        if (_isometric) _isometric = false;

        Vector2 chargeDir = (_targetPos - _thisPos).normalized;

        _moveDir = chargeDir;

        if ((_targetPos - _thisPos).magnitude < 1.5f)
        {
            _currentState = State.Attack;
            InvokeRepeating("HitOnce", 0f, 2f);
        }

        CheckFleeExit();
        CheckDyingExit();
    }

    private void makeAttack()
    {
        _moveDir = Vector2.zero;

        if (_hitWindow)
        {
            _hit = true;
            _hitWindow = false;
        }

        if ((_targetPos - _thisPos).magnitude > 3f)
        {
            _currentState = State.Charge;
            if (IsInvoking("HitOnce")) CancelInvoke("HitOnce");
        }

        CheckFleeExit();
        CheckDyingExit();
    }

    private void makeFlee() 
    {
        if (_isometric) _isometric = false;

        Vector2 fleeDir = (_thisPos - _targetPos).normalized;
        _moveDir = fleeDir;

        //CheckApproachExit();
        CheckDyingExit();
    }

    private void makeDying()
    {
        _moveDir = Vector2.zero;
    }

    private void CheckApproachExit()
    {
        if ((_targetPos - _thisPos).magnitude > 15f)
        {
            _currentState = State.Approach;
            if (IsInvoking("RandomizeState")) CancelInvoke("RandomizeState");
            if (IsInvoking("HitOnce")) CancelInvoke("HitOnce");
        }
    }

    private void CheckChargeExit()
    {
        if ((_targetPos - _thisPos).magnitude < 8f || _selfData.GetDamaged())
        {
            _currentState = State.Charge;
            if (IsInvoking("RandomizeState")) CancelInvoke("RandomizeState");
        }
    }

    private void CheckFleeExit()
    {
        if (_selfData.GetCritical()) _currentState = State.Flee;
    }

    private void CheckDyingExit()
    {
        if (_selfData.DeathFlag) _currentState = State.Dying;
    }

    private void HitOnce()
    {
        _hitWindow = true;
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

    public override bool GetIsometric()
    {
        return _isometric;
    }
}
