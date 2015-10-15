﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Inventory that has a number of slots
/// </summary>
public class InventoryScript : MonoBehaviour
{
    public List<InventoryItem> Inventory = new List<InventoryItem>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void AddResources(string resourceType, int amount)
    {
        var i = Inventory.FirstOrDefault(j => j.ResourceType == resourceType || j.IsEmpty);
        if (i == null)
        {
            i = new InventoryItem(resourceType, 0);
            Inventory.Add(i);
        }
        i.ResourceType = resourceType;
        i.Amount += amount;
    }

    public void RemoveResourcse(string resourceType, int amount)
    {
        if (amount < 0) throw new InvalidOperationException();
        if (amount == 0) return;

        var i = Inventory.FirstOrDefault(j => j.ResourceType == resourceType);
        if (i == null) throw new InvalidOperationException();

        if (amount > i.Amount) throw new InvalidOperationException();
        i.Amount -= amount;
    }

    public int GetResourceCount(string resourceType)
    {
        var i = Inventory.FirstOrDefault(j => j.ResourceType == resourceType);
        if (i == null) return 0;
        return i.Amount;
    }






    [Serializable]
    public class InventoryItem
    {
        public string ResourceType;
        public int Amount;

        public InventoryItem(string resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }

        public InventoryItem Copy()
        {
            return new InventoryItem(ResourceType, Amount);
        }

        public static InventoryItem Empty
        {
            get { return new InventoryItem("", 0); }
        }

        public bool IsEmpty { get { return ResourceType == "" || Amount == 0; } }
    }

    public InventoryItem GetSlot(int slotNum)
    {
        fillSlots(slotNum);

        return Inventory[slotNum].Copy();
    }

    private void fillSlots(int slotNum)
    {
        while (Inventory.Count <= slotNum)
        {
            Inventory.Add(InventoryItem.Empty);
        }
    }

    public void SetSlot(int slotNum, InventoryItem value)
    {
        if (value.IsEmpty) { ClearSlot(slotNum); return; }
        fillSlots(slotNum);

        Inventory[slotNum] = value;
    }

    public void ClearSlot(int slotNum)
    {
        fillSlots(slotNum);

        Inventory[slotNum] = InventoryItem.Empty;
    }
}
