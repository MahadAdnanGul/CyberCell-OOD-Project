using UnityEngine;

namespace MainGame.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D bulletRB;
        [SerializeField] private float bulletLife = 2f;
        [SerializeField] private GameObject particlePrefab;
        private int damage = 1;
        public int Damage => damage;
    
        public void SetDamage(int amount)
        {
            damage = amount;
        }
        void Start()
        {
            Destroy(gameObject,bulletLife);
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Enemy"))
            {
                DestroyBullet();
            }
        }

        public void DestroyBullet()
        {
            GameObject particle = Instantiate(particlePrefab);
            particle.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}

