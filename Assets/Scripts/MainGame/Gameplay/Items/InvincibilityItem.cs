using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shortcuts;

[CreateAssetMenu(fileName = "InvincibilityItem", menuName = "ScriptableObjects/Invincibility", order = 1)]
public class InvincibilityItem : Item
{
    public float duration = 5f;
    public override void UseItem()
    {
        Get<ServiceLocator>().uiEventsManager.onInvincible?.Invoke(duration);
    }
}
