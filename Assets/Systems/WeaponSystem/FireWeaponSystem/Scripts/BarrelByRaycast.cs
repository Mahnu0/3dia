using UnityEngine;

public class BarrelByRaycast : BarrelBase, IHitter
{
    [SerializeField] float range = 15f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    [SerializeField] float damage = 0.25f;
    [SerializeField] float horizontalDispersionAngle = 0f;
    [SerializeField] float verticalDispersionAngle = 0f;

    [SerializeField] float shotsPerSecond = 4f;

    float lastShotTime = 0f;

    bool isContinuousShooting = false;

    void Update()
    {
        if (isContinuousShooting)
            { ShootOnce(); }
    }

    public override void ShootOnce()
    {
        float timeBetweenShots = 1f / shotsPerSecond;

        if ((Time.time - lastShotTime) > timeBetweenShots)
        {
            lastShotTime = Time.time;

            Vector3 shotDirection = transform.forward;
            Quaternion horizontalDeviation =
                Quaternion.AngleAxis(
                    Random.Range(-horizontalDispersionAngle, horizontalDispersionAngle),
                    transform.up
                    );
            Quaternion verticalDeviation =
                Quaternion.AngleAxis(
                    Random.Range(-verticalDispersionAngle, verticalDispersionAngle),
                    transform.right
                    );
            shotDirection = verticalDeviation * (horizontalDeviation * shotDirection);

            Vector3 trailStartPoint = transform.position;
            Vector3 trailEndPoint = transform.position + (shotDirection * range);

            if (Physics.Raycast(
                    transform.position,
                    shotDirection,
                    out RaycastHit hit,
                    range,
                    layerMask))
            {
                HurtCollider hurtCollider = hit.collider.GetComponent<HurtCollider>();
                hurtCollider?.NotifyHit(this);
                trailEndPoint = hit.point;
            }

            ShotTrailManager.SpawnShotTrail(trailStartPoint, trailEndPoint);
        }
    }

    public override void StartShooting()
    {
        isContinuousShooting = true;
    }

    public override void StopShooting()
    {
        isContinuousShooting = false;
    }

    float IHitter.GetDamage()
    {
        return damage;
    }

    Transform IHitter.GetTransform()
    {
        return transform;
    }
}
