using UnityEngine;

public class Condition_HasTarget : DecissionTreeNode_Condition
{
    protected override bool ConditionIsMeet()
    {
        return enemy.HasTarget();
    }
}
