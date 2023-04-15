using UnityEngine;
using UnityEngine.Events;

public class MoveInput : MonoBehaviour
{
    [SerializeField] private Invoker _invoker;
    [SerializeField] private Controller _commandInput;
    private Rigidbody _controlledBody;

    private void Start() 
    {
        _controlledBody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        ICommand storedMoveCommand = new MoveCommand(_controlledBody, _commandInput.getDir());
        _invoker.AddCommand(storedMoveCommand);
    }
}
