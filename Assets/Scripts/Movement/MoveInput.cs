using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveInput : MonoBehaviour
{
    [SerializeField] private AbstractController _commandInput;
    [SerializeField] private float _speed;
    [SerializeField] private float _chargeSpeed;

    private Rigidbody _controlledBody;
    private bool _stopping;

    private Invoker _invoker;

    private RaycastHit _hit;

    private void Start() 
    {
        _invoker = Invoker.Instance;
        _controlledBody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        // Get Input and isometric relative
        Vector2 moveVelocity = _commandInput.GetDir();
        bool isometric = _commandInput.GetIsometric();
        
        // Check if shoot stagger
        if (_stopping) moveVelocity = Vector2.zero;

        // Speed change
        float speed = _speed;
        if (_commandInput.GetRush()) speed = _chargeSpeed;

        ICommand storedMoveCommand = new MoveCommand(_controlledBody, moveVelocity, speed, isometric);
        _invoker.AddCommand(storedMoveCommand);
    }

    private void Update()
    {
        if (_commandInput.GetFire()) 
        {
            _stopping = true;
            StartCoroutine(endStop(0.2f));
        }
    }

    private IEnumerator endStop(float second)
    {
        yield return new WaitForSeconds(second);
        _stopping = false;
    }
}
