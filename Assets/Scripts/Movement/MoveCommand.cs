using UnityEngine;

public class MoveCommand : ICommand
{
    private Rigidbody _unitBody;
    private Vector3 _moveDir;
    private float _speed;
    private bool _isometric;

    /// <summary>
    /// Create a move command with target rigidbody and desired velocity.
    /// The passed Vector2(x, y) value will be translated to x,z plane.
    /// </summary>
    public MoveCommand(Rigidbody unitBody, Vector2 moveDir, float speed, bool isometric)
    {
        _unitBody = unitBody;
        _moveDir = new Vector3(
            moveDir.x,
            0f,
            moveDir.y
        );
        _speed = speed;
        _isometric = isometric;
    }

    public void Execute()
    {
        Vector3 worldVelocity = _moveDir;
        if (_isometric) worldVelocity = _unitBody.transform.TransformDirection(_moveDir);

        _unitBody.MovePosition(_unitBody.position + worldVelocity * _speed * Time.deltaTime);
    }
}
