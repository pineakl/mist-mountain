using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotCommand : ICommand
{
    private LineRenderer _lineRenderer;
    private Vector3 _origin;
    private Vector3 _target;

    public BulletShotCommand(LineRenderer lineRenderer, Vector3 origin, Vector3 target)
    {
        _lineRenderer = lineRenderer;
        _origin = origin;
        _target = target;
    }

    public void Execute()
    {
        //Debug.Log(_origin);
        //BulletPool.Instance.Spawn(_origin);

        _lineRenderer.gameObject.SetActive(true);
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _origin);
        _lineRenderer.SetPosition(1, _target);
    }
}
