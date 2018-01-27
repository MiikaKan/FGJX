using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceiver : MonoBehaviour {

	public void Receive(float signalStrength)
    {
        print("Receiving signal of strength: " + signalStrength);
    }

    private void OnParticleTrigger()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
        other.GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
        //List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>();
        //ParticlePhysicsExtensions.GetCollisionEvents(other.GetComponent<ParticleSystem>(), gameObject, events);

        for (int i = 0; i < particles.Count; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.startColor = Color.red;
        }
        print("Receiving signal");
    }
}
