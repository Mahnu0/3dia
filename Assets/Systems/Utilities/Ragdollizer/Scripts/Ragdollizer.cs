using UnityEngine;

public class Ragdollizer : MonoBehaviour
{
    [SerializeField] bool ragdollizeOnAwake = false;

    Rigidbody[] rigidbodies;
    Collider[] colliders;
    Animator animator;

    [Header("Debug")]
    [SerializeField] bool debugRagdollize;
    [SerializeField] bool debugDeRagdollize;

    void OnValidate()
    {
        if (debugRagdollize)
        {
            debugRagdollize = false;
            Ragdollize();
        }

        if (debugDeRagdollize)
        {
            debugDeRagdollize = false;
            DeRagdollize();
        }
    }


    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        animator = GetComponent<Animator>();

        if (ragdollizeOnAwake)
        {
            animator.enabled = false;
        }
        else
        {
            foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = true; }
            foreach (Collider c in colliders) { c.enabled = false; }
        }
    }

    public void Ragdollize()
    {
        foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = false; }
        foreach (Collider c in colliders) { c.enabled = true; }
        animator.enabled = false;
    }

    public void DeRagdollize()
    {
        foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = true; }
        foreach (Collider c in colliders) { c.enabled = false; }
        animator.enabled = true;
    }
}
