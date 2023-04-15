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
        if (_commandInput.getDir() != _lastFrameVelocity)
        {
            int state = getState(_commandInput.getDir());
            if (_commandInput.getDir().x < 0)
            {
                _lastFlip = true;
            }
            else if (_commandInput.getDir().x > 0)
            {
                _lastFlip = false;
            }
            ICommand storedAnimationCommand = new AnimationCommand(_animator, state, _lastFlip);
            _invoker.AddCommand(storedAnimationCommand);
        }
        _lastFrameVelocity = _commandInput.getDir();
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
