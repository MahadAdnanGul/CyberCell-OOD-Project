using UnityEngine;

namespace MainGame.Gameplay.Player
{
    public class SuperAttack : MonoBehaviour
    {
        public Bullet projectilePrefab; // Prefab of the projectile
        public Transform spawnPoint; // Point from where the projectile will be spawned
        public float projectileSpeed = 10f; // Speed at which the projectile is launched
        [SerializeField] private int damage = 1;
        public void Shoot(Vector2 dir)
        {
            // Instantiate the projectile at the spawn point position and rotation
            Bullet projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            projectile.SetDamage(damage); 

            // Get the Rigidbody component of the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        

            if (rb != null)
            {
                // Launch the projectile in the direction of the spawn point's forward vector
                rb.velocity = new Vector2( projectileSpeed * dir.x,projectileSpeed*dir.y);
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D component!");
            }
        }
    }
}
