using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeInput : MonoBehaviour
{
    [SerializeField] private AbstractController _commandInput;
    [SerializeField] private UnitData _targetData;

    private Invoker _invoker;

    private void Start()
    {
        _invoker = Invoker.Instance;
    }

    private void Update()
    {
        if (_commandInput.GetFire())
        {
            ICommand storedShootCommand = new ShootCommand(_targetData.UnitTransform);
            _invoker.AddCommand(storedShootCommand);
        }
    }
}
