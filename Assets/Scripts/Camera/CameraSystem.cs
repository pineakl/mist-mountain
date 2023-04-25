using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// Define how main camera behave, mainly to track player position
/// </Summary>
public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Transform _trackingTarget;

    private Vector3 _initialPosition;
    private Vector3 _initialTrackingPosition;
    private Vector3 _deltaPosition;

    private void Start()
    {
        _initialPosition = transform.position;
        _initialTrackingPosition = _trackingTarget.position;
    }

    private void Update()
    {
        _deltaPosition = _trackingTarget.position - _initialTrackingPosition;    
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(
            _initialPosition.x + _deltaPosition.x,
            transform.position.y,
            _initialPosition.z + _deltaPosition.z
        );
    }
}
