using System;
using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    public UnityEvent <IHitter, HurtCollider> onHitReceived;

    public void NotifyHit(IHitter hitter)
    {
        onHitReceived.Invoke(hitter, this);
    }
}
