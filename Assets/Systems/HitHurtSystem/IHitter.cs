using UnityEngine;

public interface IHitter
{
    public float GetDamage();           // Devolver el da�o que hace el Hitter
    public Transform GetTransform();    // Devolver la transform del agresor
}
