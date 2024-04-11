using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterUseParticle : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        particleSystem = GetComponent<ParticleSystem>();

        // Ensure the particle system exists
        if (particleSystem == null)
        {
            Debug.LogWarning("Particle system component not found.");
            return;
        }

        // Call the method to destroy this GameObject after the particle system has finished playing
        DestroyAfterPlay();
    }

    void DestroyAfterPlay()
    {
        // Get the duration of the particle system's main module
        float duration = particleSystem.main.duration + particleSystem.main.startLifetime.constant;

        // Delay the destruction of the GameObject until the particle system has finished playing
        Destroy(gameObject, duration);
    }
}
