using MainGame.GameManagers;
using UnityEngine;
using static MainGame.Singletons.Shortcuts;

namespace MainGame.Gameplay.Items
{
    [CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "ScriptableObjects/WeaponUpgrade", order = 1)]
    public class WeaponUpgradeItem : Item
    {
        public float duration = 5f;
        public override void UseItem()
        {
            Get<ServiceLocator>().uiEventsManager.onWeaponUpgrade?.Invoke(duration);
        }
    }
}
