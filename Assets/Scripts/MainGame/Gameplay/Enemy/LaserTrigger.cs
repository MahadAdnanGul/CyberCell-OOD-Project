using UnityEngine;

namespace MainGame.Gameplay.Enemy
{
    public class LaserTrigger : MonoBehaviour
    {
        public int Damage = 1;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player.Player>().TakeDamage(Damage);
            }
        }
    }
}
