using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Controller : AbstractController
{
    private UserInput _input = null;
    private Vector2 _moveDir;
    private Plane _downPlane;
    private Vector3 _mousePosition;
    private bool _mouseClick;

    private void Awake() 
    {
        _input = new UserInput();
    }

    private void Start()
    {
        _downPlane = new Plane(Vector3.down, 0);
    }

    // Subscribe to Unity new input system
    private void OnEnable() 
    {
        _input.Enable();
        
        _input.Player.Movement.performed += onMovementPerformed;
        _input.Player.Movement.canceled += onMovementCanceled;

        _input.Player.Mouse.performed += onMousePosition;

        _input.Player.MouseClick.performed += onMouseLeftClick;
    }

    // Unsubscribe Unity new input system
    private void OnDisable() 
    {
        _input.Disable();
        
        _input.Player.Movement.performed -= onMovementPerformed;
        _input.Player.Movement.canceled -= onMovementCanceled;

        _input.Player.Mouse.performed -= onMousePosition;

        _input.Player.MouseClick.performed -= onMouseLeftClick;
    }
    private void Update()
    {
        if (!GameManager.Instance.IsPlayerAlive())
        {
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate() 
    {
        if (_mouseClick) _mouseClick = false;
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

    private void onMousePosition(InputAction.CallbackContext value)
    {
        Vector3 mousePositionWithDepth = new Vector3(value.ReadValue<Vector2>().x, value.ReadValue<Vector2>().y, Camera.main.nearClipPlane);
        Ray ray = Camera.main.ScreenPointToRay(mousePositionWithDepth);
        if (_downPlane.Raycast(ray, out float distance))
        {
            _mousePosition = ray.GetPoint(distance);
        }
    }

    private void onMouseLeftClick(InputAction.CallbackContext data)
    {
        _mouseClick = true;
    }

    public override Vector2 GetDir()
    {
        return _moveDir;
    }

    public override Vector2 GetAim()
    {
        Vector2 mousePosition2D = new Vector2(_mousePosition.x, _mousePosition.z);
        return mousePosition2D;
    }

    public override bool GetFire()
    {
        return _mouseClick;
    }

    public override bool GetIsometric()
    {
        return true;
    }
}
