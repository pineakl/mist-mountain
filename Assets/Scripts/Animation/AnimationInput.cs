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
    private static readonly int WalkSideFront = Animator.StringToHash("walk-side-front");
    private static readonly int ShootBack = Animator.StringToHash("shoot-back");
    private static readonly int ShootFront = Animator.StringToHash("shoot-front");
    private static readonly int ShootSide = Animator.StringToHash("shoot-side");
    private static readonly int ShootSideBack = Animator.StringToHash("shoot-side-back");
    private static readonly int ShootSideFront = Animator.StringToHash("shoot-side-front");

    private int _lastAnimation;
    private bool _onShooting;
    private bool _lastFlip;

    private void Update()
    {
        int state = getState(_commandInput.GetDir());
        
        if (state != _lastAnimation)
        {
            _lastAnimation = state;
            ICommand storedAnimationCommand = new AnimationCommand(_animator, state, _lastFlip);
            _invoker.AddCommand(storedAnimationCommand);
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            _onShooting = false;
        }
    }

    private int getState(Vector2 inputDir)
    {
        //  shoot
        float facingAngle = getFacingAngle();
        if (_commandInput.GetFire())
        {
            _onShooting = true;
            if (facingAngle >= -67.5 && facingAngle < -22.5) { _lastFlip = false; return ShootFront; }
            else if (facingAngle >= -22.5 && facingAngle < 22.5) { _lastFlip = false; return ShootSideFront; }
            else if (facingAngle >= 22.5 && facingAngle < 67.5) { _lastFlip = false; return ShootSide; }
            else if (facingAngle >= 67.5 && facingAngle < 112.5) { _lastFlip = false; return ShootSideBack; }
            else if (facingAngle >= 112.5 && facingAngle < 157.5) { _lastFlip = false; return ShootBack; }

            else if (facingAngle >= -112.5 && facingAngle < -67.5) { _lastFlip = true; return ShootSideFront; }
            else if (facingAngle >= -157.5 && facingAngle < -112.5) { _lastFlip = true; return ShootSide; }
            else if (facingAngle >= 157.5 || facingAngle < -157.5) { _lastFlip = true; return ShootSideBack; }
        }
        
        //  walk
        if (!_onShooting)
        {
            if (inputDir != Vector2.zero)
            {
                if (inputDir.x < 0) _lastFlip = true;
                else if (inputDir.x > 0) _lastFlip = false;

                if (inputDir.y == 1) return WalkBack;
                else if (inputDir.y == 0) return WalkSide;
                else if (inputDir.y == -1) return WalkFront;
                else if (inputDir.y > 0 && inputDir.y < 1) return WalkSideBack;
                else if (inputDir.y < 0 && inputDir.y > -1) return WalkSideFront;
            }

            //  ready
            if (facingAngle >= -67.5 && facingAngle < -22.5) { _lastFlip = false; return ReadyFront; }
            else if (facingAngle >= -22.5 && facingAngle < 22.5) { _lastFlip = false; return ReadySideFront; }
            else if (facingAngle >= 22.5 && facingAngle < 67.5) { _lastFlip = false; return ReadySide; }
            else if (facingAngle >= 67.5 && facingAngle < 112.5) { _lastFlip = false; return ReadySideBack; }
            else if (facingAngle >= 112.5 && facingAngle < 157.5) { _lastFlip = false; return ReadyBack; }

            else if (facingAngle >= -112.5 && facingAngle < -67.5) { _lastFlip = true; return ReadySideFront; }
            else if (facingAngle >= -157.5 && facingAngle < -112.5) { _lastFlip = true; return ReadySide; }
            else if (facingAngle >= 157.5 || facingAngle < -157.5) { _lastFlip = true; return ReadySideBack; }
        }

        return _lastAnimation;
    }

    private float getFacingAngle()
    {
        Vector2 unitPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 facing = _commandInput.GetAim() - unitPosition;
        float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
        return angle;
    }
}
