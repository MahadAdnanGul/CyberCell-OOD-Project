
using UnityEngine;
using Mahad.GameConstants;

public class KamakazeMovement : AIMovement
{
    [SerializeField] private TargetScanner scanner;

    private GameObject target;
    private Vector3 targetPosition;
    
    private float fixedFrameDistanceInterval;
    private Utilities.VoidSignal onTargetReached;
    private Utilities.VoidSignal onTargetLocked;

    public override void SetDelegates(Utilities.VoidSignal targetLockCallback, Utilities.VoidSignal targetReachCallback)
    {
        onTargetLocked = targetLockCallback;
        onTargetReached = targetReachCallback;
    }

    public override void Move(float speed)
    {
        if (targetReached)
            return;


        if (scanner.IsTargetFound && !targetLocked)
        {
            targetLocked = true;
            onTargetLocked?.Invoke();
            fixedFrameDistanceInterval = Time.fixedDeltaTime * speed;
            target = scanner.currentTarget;
            Debug.Log("Interval: "+fixedFrameDistanceInterval);
        }

        if (targetLocked)
        {
            targetPosition = new Vector3(target.transform.position.x,transform.position.y);
            Vector3 normalizedVelocity = (targetPosition - transform.position).normalized;
            SetVelocity(normalizedVelocity.x * speed);
        }
        
        
        if ((transform.position - targetPosition).magnitude <= fixedFrameDistanceInterval*2)
        {
            targetReached = true;
            onTargetReached?.Invoke();
            SetVelocity(0);
        }


    }
}
