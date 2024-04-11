using UnityEngine;

namespace MainGame
{
    public class DestroyAfterUseParticle : MonoBehaviour
    {
        private ParticleSystem particles;

        void Start()
        {
            // Get the ParticleSystem component attached to this GameObject
            particles = GetComponent<ParticleSystem>();

            // Ensure the particle system exists
            if (particles == null)
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
            float duration = particles.main.duration + particles.main.startLifetime.constant;

            // Delay the destruction of the GameObject until the particle system has finished playing
            Destroy(gameObject, duration);
        }
    }
}
