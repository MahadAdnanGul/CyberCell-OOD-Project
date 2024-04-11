using MainGame.GameManagers;
using MainGame.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static MainGame.Singletons.Shortcuts;
namespace MainGame.UI
{
    public class InventoryUI : MonoBehaviour, ISubscriber
    {
        [SerializeField] private Image[] slotItems;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void AddItem(Sprite sprite, int index)
        {
            slotItems[index].sprite = sprite;
            slotItems[index].gameObject.SetActive(true);
        }

        private void ConsumeItem(int index)
        {
            slotItems[index].sprite = null;
            slotItems[index].gameObject.SetActive(false);
        }

        public void SubscribeEvents()
        {
            Get<ServiceLocator>().uiEventsManager.onItemAdded += AddItem;
            Get<ServiceLocator>().uiEventsManager.onItemConsumed += ConsumeItem;
        }

        public void UnsubscribeEvents()
        {
            Get<ServiceLocator>().uiEventsManager.onItemAdded -= AddItem;
            Get<ServiceLocator>().uiEventsManager.onItemConsumed -= ConsumeItem;
        }
    }
}
