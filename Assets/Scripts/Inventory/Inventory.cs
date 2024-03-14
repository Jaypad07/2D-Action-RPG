using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemData[] starterItems;
    [SerializeField] private int inventorySize;
    private ItemSlot[] _itemSlots;

    public InventoryUI UI;

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Add the starter items.
    private void Start()
    {
        _itemSlots = new ItemSlot[inventorySize];

        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i] = new ItemSlot();
        }

        for (int i = 0; i < starterItems.Length; i++)
        {
            AddItem(starterItems[i]);
        }
    }

    // Adds an item to the inventory.
    public void AddItem(ItemData item)
    {
        ItemSlot slot = FindAvailableItemSlot(item);

        if (slot != null)
        {
            slot.Quantity++;
            UI.UpdateUI(_itemSlots);
            return;
        }

        slot = GetEmptySlot();

        if (slot != null)
        {
            slot.Item = item;
            slot.Quantity = 1;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return;
        }
        
        UI.UpdateUI(_itemSlots);
    }

    // Removes the requested item from the inventory.
    public void RemoveItem(ItemData item)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == item)
            {
                RemoveItem(_itemSlots[i]);
                return;
            }
        }
    }

    // Removes the requested item from the inventory.
    public void RemoveItem(ItemSlot slot)
    {
        if (slot == null)
        {
            Debug.LogError("Can't remove item from inventory");
            return;
        }

        slot.Quantity--;

        if (slot.Quantity <= 0)
        {
            slot.Item = null;
            slot.Quantity = 0;
        }
        
        UI.UpdateUI(_itemSlots);
    }

    // Returns in item slot that the requested item can fit into.
    ItemSlot FindAvailableItemSlot(ItemData item)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == item && _itemSlots[i].Quantity < item.MaxStackSize)
            {
                return _itemSlots[i];
            }
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null)
            {
                return _itemSlots[i];
            }
        }
        
        return null;
    }

    public void UseItem(ItemSlot slot)
    {
        if (slot.Item is MeleeWeaponItemData || slot.Item is RangedWeaponItemData)
        {
            Player.Instance.EquipCtrl.Equip(slot.Item);
        }
        else if (slot.Item is FoodItemData)
        {
            FoodItemData food = slot.Item as FoodItemData;
            Player.Instance.Heal(food.HealthToGive);
            
            RemoveItem(slot);
        }
    }

    public bool HasItem(ItemData item)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == item && _itemSlots[i].Quantity > 0)
            {
                return true;
            }
        }

        return false;
    }
}
