using UnityEngine;

public abstract class AbstractController : MonoBehaviour
{
    public abstract Vector2 GetDir();
    public abstract Vector2 GetAim();
    public abstract bool GetFire();
    public abstract bool GetIsometric();
}