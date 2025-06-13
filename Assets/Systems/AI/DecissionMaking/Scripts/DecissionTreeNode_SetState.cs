using UnityEngine;

public class DecissionTreeNode_SetState : DecissionTreeNode
{
    [SerializeField] BaseState state;

    public override void Execute()
    {
        enemy.ChangeStateTo(state);
    }
}
