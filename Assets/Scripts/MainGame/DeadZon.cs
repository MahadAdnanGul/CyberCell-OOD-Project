using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shortcuts;

public class DeadZon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Get<ServiceLocator>().uiEventsManager.onGameOver?.Invoke();
    }
}
