using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> item;
    public GameObject cellContainer;
    public KeyCode showInventory;
    public KeyCode takeButton;

    private void Start()
    {
        cellContainer.SetActive(false);
        item = new List<Item>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            item.Add(new Item());
        }
    }

    private void Update()
    {
        ToggleInventory();
        if (Input.GetKeyDown(takeButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.GetComponent<Item>())
                {
                    for (int i = 0; i < U; i++)
                    {
                        
                    }
                }
            }
        }
    }
    private void ToggleInventory()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (cellContainer.activeSelf)
            {
                cellContainer.SetActive(false);
            }
            else
            {
                cellContainer.SetActive(true);
            }
        }
    }
}
