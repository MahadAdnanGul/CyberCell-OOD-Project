using MainGame.GameManagers;
using UnityEngine;
using static MainGame.Singletons.Shortcuts;

namespace MainGame.Gameplay.Items
{
    [CreateAssetMenu(fileName = "InvincibilityItem", menuName = "ScriptableObjects/Invincibility", order = 1)]
    public class InvincibilityItem : Item
    {
        public float duration = 5f;
        public override void UseItem()
        {
            Get<ServiceLocator>().uiEventsManager.onInvincible?.Invoke(duration);
        }
    }
}
