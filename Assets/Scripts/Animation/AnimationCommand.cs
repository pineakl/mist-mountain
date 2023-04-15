using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCommand : ICommand
{
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private int _clipHash;
        private bool _flip;

        public AnimationCommand(Animator animator, int clipHash, bool flip)
        {
            _animator = animator;
            _spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
            _clipHash = clipHash;
            _flip = flip;
        }

        public void Execute()
        {
            _animator.CrossFade(_clipHash, 0, 0);
            _spriteRenderer.flipX = _flip;
        }
}
