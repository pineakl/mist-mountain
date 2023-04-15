using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInput : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetAnimation()
    {
        Debug.Log("Animation Invoked");
    }
}
