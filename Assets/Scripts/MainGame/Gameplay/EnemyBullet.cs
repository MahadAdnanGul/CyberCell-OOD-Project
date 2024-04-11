using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
