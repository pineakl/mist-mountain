using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveInput : MonoBehaviour
{
    [SerializeField] private AbstractController _commandInput;
    private Rigidbody _controlledBody;
    private bool _stopping;

    private Invoker _invoker;

    private void Start() 
    {
        _invoker = Invoker.Instance;
        _controlledBody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        Vector2 moveVelocity = _commandInput.GetDir();
        if (_stopping) moveVelocity = Vector2.zero;

        ICommand storedMoveCommand = new MoveCommand(_controlledBody, moveVelocity);
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
