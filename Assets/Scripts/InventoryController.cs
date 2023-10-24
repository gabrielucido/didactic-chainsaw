using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private List<Sprite> items;
    [SerializeField] public Sprite equipedItem;

    [SerializeField] private GameObject _inventoryRenderer;

    void Start()
    {
        items = new List<Sprite>();

        if (_inventoryRenderer == null)
        {
            throw new NotImplementedException("Inventory Renderer not set!");
        }
    }


    private void Update()
    {
        if (equipedItem != null)
        {
            _inventoryRenderer.GetComponent<Image>().sprite = equipedItem;
        }
        else
        {
            _inventoryRenderer.GetComponent<Image>().sprite = null;
        }
    }

    public void PickUpItem(GameObject item)
    {
        items.Add(item.GetComponentInChildren<SpriteRenderer>().sprite);
        Destroy(item);
    }

    public void EquipItem(GameObject item)
    {
        if (equipedItem == null)
        {
            equipedItem = item.GetComponentInChildren<SpriteRenderer>().sprite;
            Destroy(item);
        }
        else
        {
            // TODO: Simulate inventory full
        }
    }

    public void DropItem()
    {
        equipedItem = null;
    }

    public Sprite getEquipedItem()
    {
        return equipedItem;
    }
}