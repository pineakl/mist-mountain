using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInput : MonoBehaviour
{
    [SerializeField] private Invoker _invoker;
    [SerializeField] private Controller _commandInput;
    [SerializeField] private Animator _animator;

    private static readonly int ReadyBack = Animator.StringToHash("ready-back");
    private static readonly int ReadyFront = Animator.StringToHash("ready-front");
    private static readonly int ReadySide = Animator.StringToHash("ready-side");
    private static readonly int ReadySideBack = Animator.StringToHash("ready-side-back");
    private static readonly int ReadySideFront = Animator.StringToHash("ready-side-front");
    private static readonly int WalkBack = Animator.StringToHash("walk-back");
    private static readonly int WalkFront = Animator.StringToHash("walk-front");
    private static readonly int WalkSide = Animator.StringToHash("walk-side");
    private static readonly int WalkSideBack = Animator.StringToHash("walk-side-back");
    private static readonly int WalkSideFront = Animator.StringToHash("walk-front-front");
    private static readonly int ShootBack = Animator.StringToHash("shoot-back");
    private static readonly int ShootFront = Animator.StringToHash("shoot-front");
    private static readonly int ShootSide = Animator.StringToHash("shoot-side");
    private static readonly int ShootSideBack = Animator.StringToHash("shoot-side-back");
    private static readonly int ShootSideFront = Animator.StringToHash("shoot-front-front");

    private Vector2 _lastFrameVelocity;
    private bool _lastFlip;

    private void Update()
    {
        if (_commandInput.GetDir() != _lastFrameVelocity)
        {
            int state = getState(_commandInput.GetDir());
            if (_commandInput.GetDir().x < 0)
            {
                _lastFlip = true;
            }
            else if (_commandInput.GetDir().x > 0)
            {
                _lastFlip = false;
            }
            ICommand storedAnimationCommand = new AnimationCommand(_animator, state, _lastFlip);
            _invoker.AddCommand(storedAnimationCommand);
        }
        _lastFrameVelocity = _commandInput.GetDir();
    }

    private void FixedUpdate() 
    {
        Vector2 unitPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 facing = _commandInput.GetAim() - unitPosition;
        float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
    }

    private int getState(Vector2 inputDir)
    {
        if (inputDir == Vector2.zero)
        {
            return ReadySideBack;
        }
        else
        {
            return WalkSideBack;
        }
    }
}
