using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shortcuts;
public class PlayerInventory : MonoBehaviour, ISubscriber
{
    private Item[] items;
    [SerializeField] private int maxItemSlots = 3;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Start()
    {
        items = new Item[maxItemSlots];
    }

    public void ConsumeItem(int index)
    {
        if (items[index-1] != null)
        {
            Get<ServiceLocator>().uiEventsManager.onItemConsumed?.Invoke(index-1);
            items[index-1].UseItem();
            items[index-1] = null;
            
        }
    }

    public void AddItem(Item item)
    {
        int availableIndex;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                availableIndex = i;
                items[availableIndex] = item;
                Get<ServiceLocator>().uiEventsManager.onItemAdded?.Invoke(item.sprite,availableIndex);
                return;
            }
        }
    }

    public void SubscribeEvents()
    {
        Get<ServiceLocator>().inputEventManager.onItemSlotEvent += ConsumeItem;
    }

    public void UnsubscribeEvents()
    {
        Get<ServiceLocator>().inputEventManager.onItemSlotEvent -= ConsumeItem;
    }
}
