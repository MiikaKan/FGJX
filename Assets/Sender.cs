using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour {

    private float signalStrength = 1f;

    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);

        SignalBouncer b = collisionEvents[0].colliderComponent.GetComponent<SignalBouncer>();

        if (b)
        {
            signalStrength *= b.BounceAmount;
        }

        print(signalStrength);
    }
}
