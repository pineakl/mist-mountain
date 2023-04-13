using UnityEngine;

public class MoveCommand : ICommand
{
    Rigidbody _unitBody;
    Vector3 _moveDir;

    /// <summary>
    /// Create a move command with target rigidbody and desired velocity.
    /// The passed Vector2(x, y) value will be translated to x,z plane.
    /// </summary>
    public MoveCommand(Rigidbody unitBody, Vector2 moveDir)
    {
        _unitBody = unitBody;
        _moveDir = new Vector3(
            moveDir.x,
            0f,
            moveDir.y
        );
    }

    public void Execute()
    {
        _unitBody.velocity = _moveDir;
    }
}
