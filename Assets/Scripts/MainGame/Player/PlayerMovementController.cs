using System;
using System.Collections;
using System.Collections.Generic;
using Mahad.GameConstants;
using Unity.VisualScripting;
using UnityEngine;
using static MahadLibShortcuts;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerMovementController : MonoBehaviour,ISubscriber
{
    [SerializeField] 
    private float speed = 2f;

    [SerializeField] 
    private float jumpForce = 10f;
    
    [SerializeField] 
    private float dashForce = 10f;

    [SerializeField] 
    private float dashDuration = 0.25f;
    
    [SerializeField] 
    private float dashCooldown = 2f;

    [SerializeField] 
    private PlayerAnimationController animController;

    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;
    private bool isPlayerMoving = false;
    private LayerMask groundLayer;
    private bool isGrounded = true;
    private bool isDashing = false;
    private bool isDashReady = true;
    private float dashTimeElapsed = 0;

    private float originalGravityScale;

    private bool freezeAnimations = false;
    
    
    // right true | left false
    private Direction playerDirection = Direction.Right;
    public Direction PlayerDirection => playerDirection;
    public bool IsGrounded => isGrounded;

    public Vector2 GetVelocity()
    {
        return playerRB.velocity;
    }

    private void SetAnimation(string animationName)
    {
        if (!freezeAnimations)
        {
            animController.SetAnimation(animationName);
        }
    }

    public void ToggleFreezeAnimations(bool val)
    {
        freezeAnimations = val;
    }

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        groundLayer  = LayerMask.GetMask("Ground");
        originalGravityScale = playerRB.gravityScale;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void MovePlayer(Direction direction)
    {
        if(isDashing)
            return;
        float x = Utilities.DirectionToInt(direction);
        playerDirection = direction;
        playerRB.velocity = new Vector2(speed * x, playerRB.velocity.y);
        isPlayerMoving = true;
        if (isGrounded && !isDashing)
        {
            SetAnimation("Running");
        }
        
    }
    
    private void Jump()
    {
        if (isGrounded && !isDashing)
        {
            SetAnimation("JumpRise");
            playerRB.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void Dash()
    {
        if(!isDashReady)
            return;
        isDashReady = false;
        isDashing = true;
        playerRB.gravityScale = 0;
        playerRB.velocity = Vector2.zero;
        int dir = Utilities.DirectionToInt(playerDirection);
        playerRB.AddForce(new Vector2(dashForce*dir,0),ForceMode2D.Impulse);

        if (isGrounded)
        {
            SetAnimation("GroundDash");
        }
        else
        {
            SetAnimation("JumpDash");
        }
        
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (isGrounded)
        {
            return;
        }
        
        if (other.gameObject.CompareTag(GameConstants.Tags.ground))
        {
            GroundCheck();
        }
    }

    private void GroundCheck()
    {
        Vector2 boxSize = new Vector2(playerCollider.bounds.size.x *0.95f,playerCollider.bounds.size.y);

        // Calculate the origin of the box cast
        Vector2 boxOrigin = playerCollider.bounds.center;

        // Calculate the direction of the box cast (straight down)
        Vector2 boxDirection = Vector2.down;

        // Calculate the angle of the box cast (0 degrees for straight down)
        float boxAngle = 0f;

        // Calculate the maximum distance for the box cast (half the height of the collider)
        float maxDistance = 0.05f;

        // Fire the box cast
        RaycastHit2D[] hits = Physics2D.BoxCastAll(boxOrigin, boxSize, boxAngle, boxDirection, maxDistance, groundLayer);

        // Check if the box cast hit something
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.CompareTag("Ground"))
                {
                    isGrounded = true;
                }
            }
        }
    }

   
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.Tags.ground))
        {
            isGrounded = false;
        }
    }

    private void LateUpdate()
    {
        if (!isPlayerMoving && !isDashing)
        {
            StopMovement();
        }
        isPlayerMoving = false;

        if (isDashing)
        {
            HandleDashDuration();
        }
        if (!isDashReady && !isDashing)
        {
            HandleDashCoodown();
        }

        if (playerDirection == Direction.Right)
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

    private void HandleDashCoodown()
    {
        if (dashTimeElapsed <= dashCooldown)
        {
            dashTimeElapsed += Time.deltaTime;
        }
        else
        {
            isDashReady = true;
            dashTimeElapsed = 0;
        }
    }

    private void HandleDashDuration()
    {
        if (dashTimeElapsed <= dashDuration)
        {
            dashTimeElapsed += Time.deltaTime;
        }
        else
        {
            playerRB.gravityScale = originalGravityScale;
            isDashing = false;
            dashTimeElapsed = 0;
        }
    }

   

    private void StopMovement()
    {
        playerRB.velocity = new Vector2(0,playerRB.velocity.y);
        if (isGrounded && !isDashing)
        {
            SetAnimation("Idle");
        }
      
    }

    public void SubscribeEvents()
    {
        Get<ServiceLocator>().inputEventManager.onMoveInputEvent += MovePlayer;
        Get<ServiceLocator>().inputEventManager.onJumpEvent += Jump;
        Get<ServiceLocator>().inputEventManager.onDashEvent += Dash;
    }

    public void UnsubscribeEvents()
    {
        Get<ServiceLocator>().inputEventManager.onMoveInputEvent -= MovePlayer;
        Get<ServiceLocator>().inputEventManager.onJumpEvent -= Jump;
        Get<ServiceLocator>().inputEventManager.onDashEvent -= Dash;
    }
}
