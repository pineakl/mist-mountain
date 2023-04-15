using UnityEngine;
using UnityEngine.Events;

public class MoveInput : MonoBehaviour
{
    [SerializeField] private Controller _commandInput;
    private Rigidbody _controlledBody;
    private ActionInvoker _actionInvoker;

    private void Start() 
    {
        _controlledBody = gameObject.GetComponent<Rigidbody>();
        _actionInvoker = new ActionInvoker();
    }

    private void FixedUpdate() 
    {
        ICommand storedMoveCommand = new MoveCommand(_controlledBody, _commandInput.getMoveDir());
        _actionInvoker.AddCommand(storedMoveCommand);
    }
}
