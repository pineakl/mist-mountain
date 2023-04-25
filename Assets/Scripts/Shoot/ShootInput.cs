using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInput : MonoBehaviour
{
    [SerializeField] private AbstractController _commandInput;
    [SerializeField] private SphereCollider _shootOrigin;
    [SerializeField] private AudioSource _source;
    [SerializeField] private LineRenderer _lineRenderer;

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
            //  Shoot sound play
            ICommand storedSoundPlayCommand = new SoundPlayCommand(_source);
            _invoker.AddCommand(storedSoundPlayCommand);

            Vector3 shootPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            Vector3 aimPosition = new Vector3(_commandInput.GetAim().x, _shootOrigin.transform.position.y, _commandInput.GetAim().y);
            Ray ray = new Ray(shootPosition, (aimPosition - shootPosition).normalized);

            // Shoot Effect Spawn
            ICommand storedBulletShotCommand = new BulletShotCommand(_lineRenderer, shootPosition, aimPosition);
            _invoker.AddCommand(storedBulletShotCommand);
            Debug.DrawLine(shootPosition, aimPosition, Color.red);

            if (Physics.Raycast(ray, out _hit))
            {
                ICommand storedShootCommand = new ShootCommand(_hit.collider.transform.parent);
                _invoker.AddCommand(storedShootCommand);
            }
        }
    }
}
