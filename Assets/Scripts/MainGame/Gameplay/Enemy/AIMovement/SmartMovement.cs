using System;
using System.Collections;
using System.Collections.Generic;
using Mahad.GameConstants;
using UnityEngine;

public class SmartMovement : FixedMovement
{
    [SerializeField] private TargetScanner scanner;
    [SerializeField] private float distanceToMaintain = 2f;
    [SerializeField] private float idealDistance = 3.5f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Shooter shooter;
    private float targetDistance;
    private AIState currentState = AIState.Patrol;
    private bool maintainingDistance = false;
    private float colliderDistance;
    private bool cornered = false;
    private bool isShotReady = true;
    [SerializeField] private float shotCooldown = 1.25f;
    [SerializeField] private float maintainSpeedMult = 2f;
    private float timeElapsed = 0;


    protected override void Start()
    {
        base.Start();
        colliderDistance = GetComponent<BoxCollider2D>().bounds.extents.x;
        Debug.Log(colliderDistance);
    }

    private void CheckForObstruction()
    {
        int dir = Utilities.DirectionToInt(movementDirection);
        
        // Perform raycast in the movement direction
        
        Debug.DrawRay(transform.position,new Vector3(dir*(colliderDistance+0.1f),0,0),Color.red,0.1f);
        if (Physics2D.Raycast(transform.position, new Vector2(dir,0), colliderDistance+0.1f, groundMask))
        {
            Debug.Log("Wall ahead!");
            cornered = true;
        }
    }

    private void Update()
    {
        CheckForObstruction();
        if (isShotReady)
        {
            timeElapsed = 0;
        }
        else
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= shotCooldown)
            {
                isShotReady = true;
            }
        }
    }

    private void SetState(AIState newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
            switch (currentState)
            {
                case AIState.Patrol:
                    maintainingDistance = false;
                    Initialize();
                    break;
                case AIState.InRange:
                    maintainingDistance = false;
                    break;
                case AIState.MaintainDistance:
                    maintainingDistance = true;
                    break;
            }
        }
    }
    
    public override void Move(float speed)
    {
        
        if (!scanner.IsTargetFound)
        {
            SetState(AIState.Patrol);
        }
        else
        {
            if (cornered)
            {
                SetState(AIState.InRange);
            }
            else
            {
                if (!maintainingDistance)
                {
                    if (GetDistanceFrom(scanner.currentTarget.transform.position) < distanceToMaintain)
                    {
                        SetState(AIState.MaintainDistance);
                    }
                    else
                    {
                        SetState(AIState.InRange);
                    }
                }

                if (GetDistanceFrom(scanner.currentTarget.transform.position) >= idealDistance)
                {
                    maintainingDistance = false;
                }
            }
            
        }
        
        switch (currentState)
        {
            case AIState.Patrol:
                base.Move(speed);
                break;
            case AIState.InRange:
                HandleInRangeMovement();
                break;
            case AIState.MaintainDistance:
                HandleMaintainDistance(speed);
                break;
                
        }
    }
    
    private void HandleInRangeMovement()
    {
        Debug.Log("InRange");
        SetVelocity(0);
        if (isShotReady)
        {
            timeElapsed = 0;
            shooter.Shoot((scanner.currentTarget.transform.position - transform.position ).normalized);
            isShotReady = false;
        }
    }

    private void HandleMaintainDistance(float speed)
    {
        SetVelocity(-GetTargetDirection()*speed*maintainSpeedMult);
    }

    private float GetDistanceFrom(Vector3 target)
    {
        return Mathf.Abs(transform.position.x - target.x);
    }

    private int GetTargetDirection()
    {
        if (scanner.currentTarget == null)
        {
            Debug.LogError("Scanner target is null! Invalid use of this function!");
            return 0;
        }
        
        if (transform.position.x > scanner.currentTarget.transform.position.x + targetDistance)
        {
            return -1;
        }

        return 1;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, idealDistance);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, distanceToMaintain);
    }
    
}


