using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MahadLibShortcuts;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour, ISubscriber, IDamagable
{
    [SerializeField] private float health = 5f;
    [SerializeField] private float speed = 2f;
    private BoxCollider2D collider;
    [SerializeField] private int collisionDamage = 1;
    [SerializeField] protected AIMovement aiMovement;
    [SerializeField] private EnemyHurtEffect hurtEffect;
    private bool isAlive;
    public int CollisionDamage => collisionDamage;

    protected virtual void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    protected virtual void FixedUpdate()
    {
        aiMovement.Move(speed);
    }

    public virtual void TakeDamage(int amount)
    {
        hurtEffect.PlayEffect();
        health -= amount;
        if (health <= 0)
        {
            isAlive = false;
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
    
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(CollisionDamage);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().Damage);
            other.GetComponent<Bullet>().DestroyBullet();
        }
    }

    public virtual void SubscribeEvents()
    {
       
    }

    public virtual void UnsubscribeEvents()
    {
        
    }
}
