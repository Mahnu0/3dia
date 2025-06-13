using UnityEngine;

public abstract class BarrelBase : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShot;

    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            ShootOnce();
        }
    }

    public virtual void ShootOnce()
    {
        throw new System.Exception("This barrel doesn't support ShootOnce");
    }

    public virtual void StartShooting()
    {
        throw new System.Exception("This barrel doesn't support StartShooting");
    }

    public virtual void StopShooting()
    {
        throw new System.Exception("This barrel doesn't support StopShooting");
    }
}
