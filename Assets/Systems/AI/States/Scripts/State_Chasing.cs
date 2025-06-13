using UnityEngine;

public class State_Chasing : BaseState
{
    protected override void Update()
    {
        base.Update();

        Transform target = enemy.GetTarget();
        enemy.GetAgent().SetDestination(target.position);
    }
}
