using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MahadLibShortcuts;


public class MeleeAttackHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer meleeSprite;
    [SerializeField] private BoxCollider2D meleeTrigger;
    [SerializeField] private Transform triggerPosition;
    [SerializeField] private float meleeCooldown = 0.25f;
    private bool isAttacking = false;
    
    
    public void MeleeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            meleeTrigger.transform.localPosition = new Vector3(triggerPosition.localPosition.x,triggerPosition.localPosition.y,triggerPosition.localPosition.z);
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        meleeSprite.enabled = true;
        yield return new WaitForFixedUpdate();
        float timeElapsed = 0;
        HashSet<IDamagable> enemies = new HashSet<IDamagable>();
        while (timeElapsed < meleeCooldown)
        {
            CheckForEnemies(ref enemies);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        meleeSprite.enabled = false;
        isAttacking = false;
    }

    private void CheckForEnemies(ref HashSet<IDamagable> taggedEnemies)
    {
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        meleeTrigger.OverlapCollider(filter, results);
        foreach (Collider2D collider in results)
        {
            IDamagable enemy = collider.GetComponent<IDamagable>();
            if (collider.CompareTag("Enemy") && collider.GetComponent<IDamagable>()!=null && !taggedEnemies.Contains(enemy))
            {
                Debug.Log("Enemy Hit!");
                collider.GetComponent<IDamagable>().TakeDamage(1);
                Get<ServiceLocator>().uiEventsManager.onEnemyDamaged?.Invoke(1);
                taggedEnemies.Add(enemy);
            }
        }
    }
}
