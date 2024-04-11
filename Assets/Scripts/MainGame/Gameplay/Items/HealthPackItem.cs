using MainGame.GameManagers;
using UnityEngine;
using static MainGame.Singletons.Shortcuts;

namespace MainGame.Gameplay.Items
{
    [CreateAssetMenu(fileName = "HealthPack", menuName = "ScriptableObjects/HealthItem", order = 1)]
    public class HealthPackItem : Item
    {
        public int healAmount = 2;
    
        public override void UseItem()
        {
            Get<ServiceLocator>().uiEventsManager.onHealed?.Invoke(healAmount);
        }
    }
}
