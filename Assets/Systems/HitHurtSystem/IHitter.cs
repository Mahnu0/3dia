using UnityEngine;

public interface IHitter
{
    public float GetDamage();           // Devolver el daño que hace el Hitter
    public Transform GetTransform();    // Devolver la transform del agresor
}
