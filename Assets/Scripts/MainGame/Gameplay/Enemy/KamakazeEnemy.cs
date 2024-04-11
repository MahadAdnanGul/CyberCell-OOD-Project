using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamakazeEnemy : Enemy
{
    [SerializeField] private int explosionDamage = 2;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float fuseTimer = 2f;
    [SerializeField] private LayerMask explosionMask;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private GameObject explosionPrefab;
    private ContactFilter2D contactFilter;
    private IEnumerator fuseRoutine;

    protected override void Awake()
    {
        base.Awake();
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(explosionMask);
        contactFilter.useLayerMask = true;
        aiMovement.SetDelegates(OnTargetLocked,Death);
    }

    public void OnTargetLocked()
    {
        StartCoroutine(FuseCoroutine());
    }

    private void Explode()
    {
        Collider2D[] results = new Collider2D[10]; // Maximum number of colliders to check
        int colliderCount = Physics2D.OverlapCircle(transform.position, explosionRadius, contactFilter, results);
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        

        for (int i = 0; i < colliderCount; i++)
        {
            IDamagable damagable = results[i].GetComponent<IDamagable>();
            if (damagable!= null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position,
                    (results[i].transform.position - transform.position).normalized,
                    (results[i].transform.position - transform.position).magnitude, obstacleLayer);
                
                //Ensure no wall obstruction and then apply damage
                if (hit.collider == null)
                {
                    damagable.TakeDamage(explosionDamage);
                }
            }
        }
    }

    IEnumerator FuseCoroutine()
    {
        yield return new WaitForSeconds(fuseTimer);
        Death();
    }
    
    protected override void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().Damage);
            Destroy(other.gameObject);
        }
    }

    protected override void Death()
    {
        StopAllCoroutines();
        Explode();
        base.Death();
    }
    
    
}
