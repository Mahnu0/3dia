using UnityEngine;

public interface ITargeteable
{
    enum Faction
    {
        Player,
        Enemy,
        Ally,
    }

    public Faction GetFaction();
    public Transform GetTransform();
}
