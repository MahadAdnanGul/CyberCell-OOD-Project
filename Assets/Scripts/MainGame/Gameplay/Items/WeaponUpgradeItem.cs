using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shortcuts;

[CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "ScriptableObjects/WeaponUpgrade", order = 1)]
public class WeaponUpgradeItem : Item
{
    public float duration = 5f;
    public override void UseItem()
    {
        Get<ServiceLocator>().uiEventsManager.onWeaponUpgrade?.Invoke(duration);
    }
}
