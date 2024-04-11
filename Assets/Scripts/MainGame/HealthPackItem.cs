using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shortcuts;

[CreateAssetMenu(fileName = "HealthPack", menuName = "ScriptableObjects/HealthItem", order = 1)]
public class HealthPackItem : Item
{
    public int healAmount = 2;
    
    public override void UseItem()
    {
        Get<ServiceLocator>().uiEventsManager.onHealed?.Invoke(healAmount);
    }
}
