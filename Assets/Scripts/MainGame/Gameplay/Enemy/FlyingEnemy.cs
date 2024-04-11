using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
