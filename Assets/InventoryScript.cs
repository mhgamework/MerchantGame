using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryScript : MonoBehaviour {
    public List<InventoryItem> Inventory = new List<InventoryItem>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void AddResources(string resourceType, int amount)
    {
        var i = Inventory.FirstOrDefault(j => j.ResourceType == resourceType);
        if (i == null)
        {
            i = new InventoryItem(resourceType, 0);
            Inventory.Add(i);
        }

        i.Amount += amount;
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
    }
}
