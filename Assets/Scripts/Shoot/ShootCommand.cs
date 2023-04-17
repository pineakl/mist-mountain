using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand
{
    private Transform _target;

    public ShootCommand(Transform target)
    {
        _target = target;
    }

    public void Execute()
    {
        Debug.Log("Hit" + _target.name);
    }
}
