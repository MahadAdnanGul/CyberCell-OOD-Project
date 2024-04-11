
using System;
using Mahad.GameConstants;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class AIMovement : MonoBehaviour
{
    protected bool targetReached = false;
    
    protected bool targetLocked = false;

    protected Direction movementDirection = Direction.Left;
    
    private Rigidbody2D rb;
    public abstract void Move(float speed);


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void SetDelegates(Utilities.VoidSignal func1, Utilities.VoidSignal func2)
    {
        
    }
    
    protected void SetVelocity(float x)
    {
        rb.velocity = new Vector2(x,rb.velocity.y);
        if (x > 0)
        {
            movementDirection = Direction.Right;
        }

        if (x < 0)
        {
            movementDirection = Direction.Left;
        }
        
        if (movementDirection == Direction.Right)
        {
            Quaternion newRotation = Quaternion.Euler(0,0,0);
            transform.rotation = newRotation;
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(0,180,0);
            transform.rotation = newRotation;
        }
    }
    
}
