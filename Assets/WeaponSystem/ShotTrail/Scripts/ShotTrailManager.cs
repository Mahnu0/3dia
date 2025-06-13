using UnityEngine;

public class ShotTrailManager : MonoBehaviour
{
    [SerializeField] GameObject trailPrefab;

    static ShotTrailManager instance;

    static public void SpawnShotTrail(Vector3 startPosition, Vector3 endPosition)
    {
        instance.InstantiateTrail(startPosition, endPosition);
    }

    private void InstantiateTrail(Vector3 startPosition, Vector3 endPosition)
    {
        GameObject trailGO = Instantiate(trailPrefab);
        ShotTrail shotTrail = trailGO.GetComponent<ShotTrail>();
        shotTrail.InitShotTrail(startPosition, endPosition);
    }

    private void Awake()
    {
        instance = this;
    }
}
