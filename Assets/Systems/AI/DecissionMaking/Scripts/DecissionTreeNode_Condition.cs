using System;
using UnityEngine;

public abstract class DecissionTreeNode_Condition : DecissionTreeNode
{
    public override void Execute()
    {
        int childrenToExecuteIndex = ConditionIsMeet() ? 0 : 1;

        transform.GetChild(childrenToExecuteIndex).
            GetComponent<DecissionTreeNode>().
            Execute();
    }

    protected abstract bool ConditionIsMeet();
}
