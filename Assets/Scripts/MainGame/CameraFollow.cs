using System.Collections;
using System.Collections.Generic;
using Mahad.GameConstants;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerMovementController target; // The target object to follow
    public float followSpeed = 2f;
    public Vector3 offset; // The offset distance between the camera and the target
    

    [SerializeField] private float lookOffsetDown = 5f;
    [SerializeField] private float lookOffsetUp = 3f;

    private float currentoffset = 0;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentoffset = -lookOffsetDown;
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            currentoffset = lookOffsetUp;
        }

        else
        {
            currentoffset = 0;
        }
        if (target != null)
        {
            int dir = Utilities.DirectionToInt(target.PlayerDirection);
            float desiredXOffset = offset.x * dir;
            Vector3 desiredPosition = target.transform.position;
            desiredPosition = new Vector3(target.transform.position.x + desiredXOffset, (target.transform.position.y+offset.y+currentoffset),target.transform.position.z + offset.z);
            Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, followSpeed*Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
