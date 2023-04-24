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
        Flashing targetFlash = _target.GetComponent<Flashing>();
        DataHolder dataHold = _target.GetComponent<DataHolder>();
        if (targetFlash) targetFlash.flash(0.1f);
        if (dataHold) dataHold.SubstractHealth();
    }
}
