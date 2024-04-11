using System;
using System.Collections;
using System.Collections.Generic;
using Mahad.GameConstants;
using UnityEngine;
using static MahadLibShortcuts;

public class Player : MonoBehaviour, ISubscriber, IDamagable
{
    [SerializeField] private float damageCooldown = 2f;
    [SerializeField] private float playerShotCooldown = 2f;
    private bool isInvulnerable = false;
    private int health = 4;
    private int superMeter = 0;
    [SerializeField] Shooter shooter;
    [SerializeField] private SuperAttack superAttack;
    [SerializeField] private PlayerMovementController movement;
    [SerializeField] private MeleeAttackHandler meleeHandler;
    [SerializeField] private InvulnerabilityEffect invulnerabilityEffect;

    private PlayerAnimationController animController;


    private float timeElapsed = 0;
    private bool canPlayerShoot = true;
    private bool isWeaponUpgraded = false;

    private void Awake()
    {
        animController = GetComponent<PlayerAnimationController>();
    }

    private void Start()
    {
        Get<ServiceLocator>().uiEventsManager.onInitializeHud?.Invoke(health);
    }
    
    private void Update()
    {
        if (!canPlayerShoot)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= playerShotCooldown)
            {
                timeElapsed = 0;
                canPlayerShoot = true;
            }
        }
    }

    private void WeaponUpgrade(float duration)
    {
        if (!isWeaponUpgraded)
        {
            isWeaponUpgraded = true;
            StartCoroutine(UpgradeRoutine(duration, playerShotCooldown));
            playerShotCooldown = 0.25f;
        }
    }

    private IEnumerator UpgradeRoutine(float duration, float originalValue)
    {
        yield return new WaitForSeconds(duration);
        playerShotCooldown = originalValue;
        isWeaponUpgraded = false;
    }

    private void Shoot()
    {
        if (canPlayerShoot)
        {
            bool isChanged = animController.SetAnimation("Shoot");
            if (isChanged)
            {
                FreezeUnfreezeAnimations();
            }
            
            shooter.Shoot(new Vector2(Utilities.DirectionToInt(movement.PlayerDirection),0));
            canPlayerShoot = false;
            timeElapsed = 0;
        }
    }

    private void FreezeUnfreezeAnimations()
    {
        movement.ToggleFreezeAnimations(true);
        Invoke(nameof(UnfreezeAnimations),animController.GetCurrentAnimationDuration());
    }

    private void UseSuperAttack()
    {
        if (superMeter >= 5)
        {
            Get<ServiceLocator>().uiEventsManager.onSuperUsed?.Invoke();
            superMeter = 0;
            superAttack.Shoot(new Vector2(Utilities.DirectionToInt(movement.PlayerDirection),0));
        }
    }

    private void MeleeAttack()
    {
        meleeHandler.MeleeAttack();
        bool isChanged = animController.SetAnimation("RunAttack");
        if (isChanged)
        {
            FreezeUnfreezeAnimations();
        }
    }

    private void UnfreezeAnimations()
    {
        movement.ToggleFreezeAnimations(false);
    }
    public void SubscribeEvents()
    {
        Get<ServiceLocator>().inputEventManager.onShootEvent += Shoot;
        Get<ServiceLocator>().inputEventManager.onMeleeEvent += MeleeAttack;
        Get<ServiceLocator>().inputEventManager.onSuperEvent += UseSuperAttack;
        Get<ServiceLocator>().uiEventsManager.onEnemyDamaged += IncrementSuper;
        Get<ServiceLocator>().uiEventsManager.onHealed += HealPlayer;
        Get<ServiceLocator>().uiEventsManager.onInvincible += Invincibility;
        Get<ServiceLocator>().uiEventsManager.onWeaponUpgrade += WeaponUpgrade;
    }

    public void UnsubscribeEvents()
    {
        Get<ServiceLocator>().inputEventManager.onShootEvent -= Shoot;
        Get<ServiceLocator>().inputEventManager.onMeleeEvent -= MeleeAttack;
        Get<ServiceLocator>().inputEventManager.onSuperEvent -= UseSuperAttack;
        Get<ServiceLocator>().uiEventsManager.onEnemyDamaged -= IncrementSuper;
        Get<ServiceLocator>().uiEventsManager.onHealed -= HealPlayer;
        Get<ServiceLocator>().uiEventsManager.onInvincible -= Invincibility;
        Get<ServiceLocator>().uiEventsManager.onWeaponUpgrade -= WeaponUpgrade;
    }

    private void IncrementSuper(int amount)
    {
        superMeter += amount;
    }

    private IEnumerator GetHitRoutine(float time)
    {
        if (isInvulnerable)
        {
            yield break;
        }
        isInvulnerable = true;

        yield return new WaitForSeconds(time);

        isInvulnerable = false;
    }

    public void TakeDamage(int amount)
    {
        if (!isInvulnerable)
        {
            health -= amount;
            if (health <= 0)
            {
                health = 0;
                Get<ServiceLocator>().uiEventsManager.onGameOver?.Invoke();
            }
            Get<ServiceLocator>().uiEventsManager.onDamageTaken?.Invoke(amount);
            Invincibility(damageCooldown);
        }
    }

    private void Invincibility(float time)
    {
        invulnerabilityEffect.PlayEffect(time);
        StartCoroutine(GetHitRoutine(time));
    }

    public void HealPlayer(int amount)
    {
        health += amount;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().Damage);
        }
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}
