using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private Rigidbody _controlledBody;
    private UserInput _input = null;
    private Vector2 _moveDir;

    private void Awake() 
    {
        _input = new UserInput();
    }

    private void Start()
    {}

    // Subscribe to Unity new input system
    private void OnEnable() 
    {
        _input.Enable();
        _input.Player.Movement.performed += onMovementPerformed;
        _input.Player.Movement.canceled += onMovementCanceled;
    }

    // Unsubscribe Unity new input system
    private void OnDisable() 
    {
        _input.Disable();
        _input.Player.Movement.performed -= onMovementPerformed;
        _input.Player.Movement.canceled -= onMovementCanceled;
    }

    // Read value from input system
    private void onMovementPerformed(InputAction.CallbackContext value)
    {
        _moveDir = value.ReadValue<Vector2>();
    }

    // Reset value to zero
    private void onMovementCanceled(InputAction.CallbackContext value)
    {
        _moveDir = Vector2.zero;
    }

    public Vector2 getMoveDir()
    {
        return _moveDir;
    }
}
