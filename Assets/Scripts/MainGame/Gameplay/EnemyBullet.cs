using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Gameplay
{
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
}

