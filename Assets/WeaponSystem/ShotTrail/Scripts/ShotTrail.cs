using System;
using DG.Tweening;
using UnityEngine;

public class ShotTrail : MonoBehaviour
{
    [SerializeField] int numPositions = 10;
    [SerializeField] float shrinkingDuration = 0.25f;
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void InitShotTrail(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3[] positions = new Vector3[numPositions];

        for (int i = 0; i < numPositions; i++)
            { positions[i] = Vector3.Lerp(startPosition, endPosition, (1f / numPositions) * i); }
        lineRenderer.SetPositions(positions);

        DOTween.To(
                () => lineRenderer.widthMultiplier,
                (x) => lineRenderer.widthMultiplier = x,
                0f,
                shrinkingDuration
            ).OnComplete(
                () => Destroy(gameObject)
            );
    }
}
