using MainGame.Gameplay.Player;
using UnityEngine;

namespace MainGame.Gameplay.Enemy
{
    public class FlyingEnemy : Enemy
    {
        [SerializeField] private Shooter shooter;
        [SerializeField] private float shootCooldown;
        private float timeElapsed = 0;


        private void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= shootCooldown)
            {
                timeElapsed = 0;
                shooter.Shoot(new Vector2(0,-1));
            }
        }
    }
}
