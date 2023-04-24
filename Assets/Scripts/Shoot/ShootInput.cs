using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInput : MonoBehaviour
{
    [SerializeField] private AbstractController _commandInput;
    [SerializeField] private SphereCollider _shootOrigin;

    private Invoker _invoker;

    private RaycastHit _hit;

    private void Start()
    {
        _invoker = Invoker.Instance;
    }

    private void Update()
    {
        if (_commandInput.GetFire())
        {
            Vector3 shootPosition = _shootOrigin.transform.position;
            Vector3 aimPosition = new Vector3(_commandInput.GetAim().x, _shootOrigin.transform.position.y, _commandInput.GetAim().y);
            Ray ray = new Ray(shootPosition, (aimPosition - shootPosition).normalized);
            
            if (Physics.Raycast(ray, out _hit))
            {
                ICommand storedShootCommand = new ShootCommand(_hit.collider.transform.parent);
                _invoker.AddCommand(storedShootCommand);
            }
        }
    }
}
