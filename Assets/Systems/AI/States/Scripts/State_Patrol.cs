using UnityEngine;

public class State_Patrol : BaseState
{
    [SerializeField] Transform patrolPointsParent;
    [SerializeField] int startingPatrolPointIndex = 0;
    [SerializeField] float reachThreshold = 1.5f;

    int currentPatrolPointIndex;

    protected override void Awake()
    {
        base.Awake();
        currentPatrolPointIndex = startingPatrolPointIndex;
    }

    protected override void Update()
    {
        base.Update();
        Vector3 patrolPointPosition = patrolPointsParent.GetChild(currentPatrolPointIndex).position;

        if (Vector3.Distance(transform.position, patrolPointPosition) < reachThreshold)
        {
            currentPatrolPointIndex++;
            if (currentPatrolPointIndex >= patrolPointsParent.childCount)
                { currentPatrolPointIndex = 0; }
        }

        enemy.GetAgent().destination = patrolPointPosition;
    }
}
