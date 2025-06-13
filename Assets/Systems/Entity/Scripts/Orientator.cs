using UnityEngine;

public class Orientator : MonoBehaviour
{
    float angularSpeed = 360f;

    public void SetAngularSpeed(float angularSpeed)
        { this.angularSpeed = angularSpeed; }

    public void OrientateTo(Vector3 desiredDirection)
    {
        float angleToApply = angularSpeed * Time.deltaTime;
        // Distancia angular entre transform.forward y desiredDirection
        float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);
        float realAngleToApply = Mathf.Sign(angularDistance) * Mathf.Min(angleToApply, Mathf.Abs(angularDistance));
        Quaternion rotationToApply = Quaternion.AngleAxis(realAngleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }
}
