using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Sight : MonoBehaviour
{
    [SerializeField] float range = 15f;
    [SerializeField] float width = 10f;
    [SerializeField] float height = 7f;

    [SerializeField] LayerMask visibleLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] LayerMask occludingLayerMask = Physics.DefaultRaycastLayers;

    public List<ITargeteable> targeteables = new();
    ITargeteable parentTargeteable;

    private void Awake()
    {
        parentTargeteable = GetComponentInParent<ITargeteable>();
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(
            transform.position + (transform.forward * (range / 2f)),
            (Vector3.forward * (range / 2f)) +
            (Vector3.right * (width / 2f)) +
            (Vector3.up * (height / 2f)),
            transform.rotation,
            visibleLayerMask);

        targeteables.Clear();

        foreach (Collider c in colliders)
        {
            ITargeteable targeteable = c.GetComponent<ITargeteable>();
            if (targeteable != null)
            {
                if (IsVisibleBecauseFaction(targeteable))
                {
                    bool hasLineOfSight = true;
                    if (Physics.Raycast(transform.position, c.transform.position, out RaycastHit hit, range, occludingLayerMask))
                    { hasLineOfSight = hit.collider == c; }
                    if (hasLineOfSight) { targeteables.Add(targeteable); }
                }
            }
        }

        // TODO: Ordenar por distancia
    }

    private bool IsVisibleBecauseFaction(ITargeteable targeteable)
    {
        Debug.Log("parentTargeteable - Faction" + parentTargeteable.GetFaction());
        Debug.Log("targeteable - Faction" + targeteable.GetFaction());

        bool isVisibleBecauseFaction = false;
        switch (parentTargeteable.GetFaction())
        {
            case ITargeteable.Faction.Player:
                break;
            case ITargeteable.Faction.Enemy:
                isVisibleBecauseFaction = targeteable.GetFaction() != ITargeteable.Faction.Enemy;
                break;
            case ITargeteable.Faction.Ally:
                isVisibleBecauseFaction = targeteable.GetFaction() == ITargeteable.Faction.Enemy;
                break;
        }

        return isVisibleBecauseFaction;
    }

    public ITargeteable GetClosestTarget()
    {
        return (targeteables.Count > 0) ? targeteables[0] : null;
    }
}
