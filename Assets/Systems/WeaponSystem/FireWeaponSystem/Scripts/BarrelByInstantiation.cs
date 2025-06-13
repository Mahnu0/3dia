using UnityEngine;

public class BarrelByInstantiation : BarrelBase
{
    [SerializeField] GameObject projectilePrefab;

    public override void ShootOnce()
    {
        Instantiate(
            projectilePrefab, 
            transform.position,
            transform.rotation);
    }
}
