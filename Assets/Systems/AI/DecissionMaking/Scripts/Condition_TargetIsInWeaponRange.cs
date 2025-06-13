using UnityEngine;

public class Condition_TargetIsInWeaponRange : DecissionTreeNode_Condition
{
    protected override bool ConditionIsMeet()
    {
        return enemy.TargetIsInRange();
    }
}
