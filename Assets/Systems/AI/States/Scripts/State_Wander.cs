using UnityEngine;

public class State_Wander : BaseState
{
    [SerializeField] float wanderingRadius = 5f;
    [SerializeField] float reachingDistance = 1.5f;

    Vector3 homeOrigin;
    Vector3 wanderPosition;

    protected override void Awake()
    {
        base.Awake();
        homeOrigin = transform.position;
        SelectWanderPosition();
    }

    protected override void Update()
    {
        base.Update();
        enemy.GetAgent().SetDestination(wanderPosition);

        if (Vector3.Distance(transform.position, wanderPosition) < reachingDistance)
        { SelectWanderPosition(); }
    }

    private void SelectWanderPosition()
    {
        Vector2 positionXY = Random.insideUnitCircle * wanderingRadius;
        Vector3 positionXZ = new Vector3(positionXY.x, 0f, positionXY.y);
        wanderPosition = homeOrigin + positionXZ;
    }
}
