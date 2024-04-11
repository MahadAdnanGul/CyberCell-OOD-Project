using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class TargetScanner : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private string targetTag = "Player";
    public float detectionRange = 5f; // Range within which to detect the player
    public LayerMask obstacleLayer; // Layers considered as obstacles
    private ContactFilter2D contactFilter;
    private bool targetFound = false;
    public bool IsTargetFound => targetFound;
    [HideInInspector] public GameObject currentTarget;
    
    private void Awake()
    {
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(targetLayer); // Set the layer mask to only detect objects with the player tag
        contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        Collider2D[] results = new Collider2D[1]; // Maximum number of colliders to check
        int colliderCount = Physics2D.OverlapCircle(transform.position, detectionRange, contactFilter, results);
        if (colliderCount == 0)
        {
            SetTarget(false,null);
            return;
        }

        if (colliderCount > 1)
        {
            SetTarget(false,null);
        }
        
        Collider2D collider = results[0];
        if (collider.CompareTag(targetTag))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (collider.transform.position - transform.position), 
                (collider.transform.position - transform.position).magnitude,
                obstacleLayer);
            if (hit.collider == null)
            {
                SetTarget(true,collider.gameObject);
            }
            else
            {
                SetTarget(false,null);
            }
        }

    }

    public float DistanceFromTarget()
    {
        return (transform.position - currentTarget.transform.position).magnitude;
    }

    private void SetTarget(bool isFound, GameObject target)
    {
        targetFound = isFound;
        currentTarget = target;
    }

    // Visualize the detection range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
