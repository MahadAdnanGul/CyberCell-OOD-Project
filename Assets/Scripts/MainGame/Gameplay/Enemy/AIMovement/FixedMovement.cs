using System;
using System.Collections;
using System.Collections.Generic;
using Mahad.GameConstants;
using UnityEngine;


public class FixedMovement : AIMovement
{
    [SerializeField] private float moveRadius = 4f;
    private Vector2 initialPosition;

    private bool hasStarted = false;
    

    protected virtual void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        hasStarted = false;
        initialPosition = transform.position;
    }

    public override void Move(float speed)
    {
        PerformFixedMovement(speed);
    }

    private void PerformFixedMovement(float speed)
    {
        if (!hasStarted)
        {
            SetVelocity(speed);
            hasStarted = true;
        }

        if (transform.position.x >= initialPosition.x + moveRadius)
        {
            SetVelocity(-speed);
        }
        else if (transform.position.x <= initialPosition.x - moveRadius)
        {
            SetVelocity(speed);
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, moveRadius);
    }
}
