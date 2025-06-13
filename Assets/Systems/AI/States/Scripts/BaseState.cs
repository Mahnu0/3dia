using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected Enemy enemy;

    public virtual void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }

    protected virtual void Awake() { }
    protected virtual void OnEnable() { }
    protected virtual void Update() { }
    protected virtual void OnDisable() { }
}
