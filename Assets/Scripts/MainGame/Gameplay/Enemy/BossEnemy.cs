using System.Collections;
using MainGame.GameManagers;
using MainGame.Gameplay.Enemy.AIMovement;
using MainGame.Gameplay.Player;
using UnityEngine;
using Random = UnityEngine.Random;
using static MainGame.Singletons.Shortcuts;

namespace MainGame.Gameplay.Enemy
{
    public class BossEnemy : Enemy
    {
        [SerializeField] private float laserChargeTime = 3f;
        [SerializeField] private float laserStayTime = 3f;
        [SerializeField] private float reactionTime = 5f;
        [SerializeField] private TargetScanner scanner;
        [SerializeField] private BossLaser laserWarning;
        [SerializeField] private Transform laserPosition;
        [SerializeField] private float showerHeight = 5f;
        [SerializeField] private float showerRange = 8f;
        [SerializeField] private int showerCount = 5;
        [SerializeField] private float showerCooldown = 2f;

        [SerializeField] private Shooter bossShooter;
        public LayerMask groundLayer;
    

        private float timeElapsed = 0f;
        private bool isInSequence = false;

        private int count = 0;


        private void Update()
        {
            //Debug.DrawRay(transform.position,transform.forward *100f, Color.red,0.1f);
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= reactionTime && !isInSequence && scanner.IsTargetFound)
            {
                if (count == 0)
                {
                    StartCoroutine(LaserSequence());
                
                }
                else
                {
                    StartCoroutine(ShowerSequence());
                }

                count++;
                if (count > 1)
                {
                    count = 0;
                }

            }
        }

        private IEnumerator LaserSequence()
        {
            isInSequence = true;
            BossLaser laserInstance = Instantiate(laserWarning);
            laserInstance.SetDamage(CollisionDamage);
        
            laserInstance.transform.position = laserPosition.position;
            laserInstance.transform.LookAt(scanner.currentTarget.transform);
            laserInstance.transform.localScale = new Vector3(laserWarning.transform.localScale.x,
                laserInstance.transform.localScale.y, RayCastCheck(laserInstance.transform));
            yield return new WaitForSeconds(laserChargeTime);
            laserInstance.ToggleDamagingLaser(true);
            yield return new WaitForSeconds(laserStayTime);
            Destroy(laserInstance.gameObject);
            timeElapsed = 0;
            isInSequence = false;
        }

        private float RayCastCheck(Transform laser)
        {
            RaycastHit2D hit = Physics2D.Raycast(laser.position, laser.forward, 1000f,groundLayer);

            // Check if the ray hits something
            if (hit.collider != null)
            {
                // If the ray hits the ground, do something
                return (new Vector2(transform.position.x,transform.position.y) - hit.point).magnitude;
                // You can perform actions like stopping movement, triggering effects, etc.
            }

            return 0;
        }

        private IEnumerator ShowerSequence()
        {
            isInSequence = true;
            int currentMeteorCount = 0;
            while (currentMeteorCount<showerCount)
            {
                currentMeteorCount++;
                float randomXOffset = Random.Range(0, showerRange);
                bossShooter.spawnPoint.position = new Vector3(transform.position.x - randomXOffset, showerHeight, 0);
                bossShooter.Shoot(Vector3.down);
                yield return new WaitForSeconds(showerCooldown);
            
            }
            isInSequence = false;
        }
    
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, showerHeight);
        
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, showerRange);
        }

        protected override void Death()
        {
            Get<ServiceLocator>().uiEventsManager.onGameWon?.Invoke();
            base.Death();
        }
    }
}
