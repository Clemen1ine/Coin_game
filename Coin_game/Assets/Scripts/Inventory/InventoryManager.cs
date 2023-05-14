using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
  public static InventoryManager inventoryManager; 
  public int maxStackedItems = 4;
  public InventorySlot[] inventorySlots;
  public GameObject InventoryItemPrefab;

  private void Awake()
  {
    inventoryManager = this;
  }

  public bool AddItem(Item item)
  {
    
    for (int i = 0; i < inventorySlots.Length; i++)
    {
      InventorySlot slot = inventorySlots[i];
      InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
      if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
      {
        itemInSlot.count++;
        itemInSlot.RefreshCount();
        return true;
      }
    }
    
    for (int i = 0; i < inventorySlots.Length; i++)
    {
      InventorySlot slot = inventorySlots[i];
      InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
      if (itemInSlot == null)
      {
        SpawnNewItem(item, slot);
        return true;
      }
    }
    return false;
  }

  public void SpawnNewItem(Item item, InventorySlot slot)
  {
    GameObject nrwItemGo = Instantiate(InventoryItemPrefab, slot.transform);
    InventoryItem inventoryItem = nrwItemGo.GetComponent<InventoryItem>();
    inventoryItem.InitialiseItem(item);
  }
}
