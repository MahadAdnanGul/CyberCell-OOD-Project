using UnityEngine;
using System.Collections;

public class BobSprite : MonoBehaviour
{
    public float bobHeight = 0.5f; // Height of the bobbing motion
    public float bobDuration = 1.0f; // Duration of one complete bob cycle
    public bool startBobbingOnAwake = true; // Whether to start bobbing automatically when the script starts

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;

        if (startBobbingOnAwake)
        {
            StartBobbing();
        }
    }

    public void StartBobbing()
    {
        LeanTween.moveY(gameObject, originalPosition.y + bobHeight, bobDuration / 2f)
            .setLoopPingPong(-1)
            .setEaseInOutSine();
    }

    public void StopBobbing()
    {
        LeanTween.cancel(gameObject);
        transform.position = originalPosition;
    }
}