using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private Item item;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = item.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInventory>() != null)
        {
            other.GetComponent<PlayerInventory>().AddItem(item);
            Destroy(gameObject);
        }
    }
}
