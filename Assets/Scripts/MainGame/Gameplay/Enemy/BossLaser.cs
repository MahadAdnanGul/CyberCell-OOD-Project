using UnityEngine;

namespace MainGame.Gameplay.Enemy
{
    public class BossLaser : MonoBehaviour
    {
        [SerializeField] private LaserTrigger damagingLaser;
    
    
        public void SetDamage(int dmg)
        {
            damagingLaser.Damage = dmg;
        }
   

        public void ToggleDamagingLaser(bool val)
        {
            damagingLaser.gameObject.SetActive(val);
        }
    }
}
