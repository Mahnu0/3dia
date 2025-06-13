using UnityEngine;

public class Condition_HasAlreadyVisitedTheLastTargetPosition : DecissionTreeNode_Condition
{
    protected override bool ConditionIsMeet()
    {
        return enemy.HasAlreadyVisitedTheLastTargetPosition();
    }
}
