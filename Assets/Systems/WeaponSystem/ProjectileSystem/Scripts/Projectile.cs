using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum SpeedMode
    {
        ApplyOnStart,
        ApplyAllTheTime,
    }

    [SerializeField] SpeedMode speedMode = SpeedMode.ApplyOnStart;
    [SerializeField] Vector3 speed = Vector3.forward;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] GameObject[] resultsPrefab;
    [SerializeField] bool destroyOnCollision;
    [SerializeField] bool instantiateResultsOnCollision = true;
    [SerializeField] bool instantiateResultsOnLifetimeEnd = false;


    Rigidbody rigidbody;
    float consumedLifeTime;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        consumedLifeTime = 0f;
    }

    private void Start()
    {
        if (speedMode == SpeedMode.ApplyOnStart)
        {
            rigidbody.linearVelocity = transform.TransformDirection(speed);
        }
    }

    private void Update()
    {
        if (speedMode == SpeedMode.ApplyAllTheTime)
        {
            rigidbody.linearVelocity = transform.TransformDirection(speed);
        }
        consumedLifeTime += Time.deltaTime;
        if (consumedLifeTime >= lifeTime)
        {
            Destroy(gameObject);
            if (instantiateResultsOnLifetimeEnd)
            {
                InstantiateResults();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnCollision)
        {
            Destroy(gameObject);
            if (instantiateResultsOnCollision)
            {
                InstantiateResults();
            }
        }
    }

    bool resultsAlreadyInstantiated = false;
    void InstantiateResults()
    {
        if (!resultsAlreadyInstantiated)
        {
            resultsAlreadyInstantiated = true;
            foreach (GameObject result in resultsPrefab)
            {
                Instantiate(result, transform.position, transform.rotation);
            }
        }
    }
}
