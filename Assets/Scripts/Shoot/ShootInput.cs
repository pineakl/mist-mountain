using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInput : MonoBehaviour
{
    [SerializeField] private Invoker _invoker;
    [SerializeField] private Controller _commandInput;
    [SerializeField] private BoxCollider _shootOrigin;

    private RaycastHit _hit;

    private void Update()
    {
        if (_commandInput.GetFire())
        {
            Vector3 shootPosition = _shootOrigin.transform.position;
            Vector3 aimPosition = new Vector3(_commandInput.GetAim().x, _shootOrigin.transform.position.y, _commandInput.GetAim().y);
            Ray ray = new Ray(shootPosition, aimPosition);
            
            if (Physics.Raycast(ray, out _hit))
            {
                ICommand storedShootCommand = new ShootCommand(_hit.collider.transform.parent);
                _invoker.AddCommand(storedShootCommand);
            }
        }
    }
}
