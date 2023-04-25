using UnityEngine;

public abstract class AbstractController : MonoBehaviour
{
    /// <Summary>
    /// Get movement direction from controller
    /// </Summary>
    public abstract Vector2 GetDir();

    /// <Summary>
    /// Get aim direction
    /// </Summary>
    public abstract Vector2 GetAim();

    /// <Summary>
    /// Get one-press fire action from controller
    /// </Summary>
    public abstract bool GetFire();

    /// <Summary>
    /// Get wether movement is isometric to modify to worldVelocity
    /// </Summary>
    public abstract bool GetIsometric();

    /// <Summary>
    /// Get rushing state to modify movement speed
    /// </Summary>
    public abstract bool GetRush();
}