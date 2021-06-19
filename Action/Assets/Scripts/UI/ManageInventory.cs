using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageInventory : MonoBehaviour
{
    GameObject player = null;
    Inventory inventory = null;
    GameObject specialInventory = null;
    GameObject regularInventory = null;

    int inventorySlots = 0;

    bool flashlightObtained = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        specialInventory = transform.GetChild(0).transform.GetChild(0).gameObject;
        regularInventory = transform.GetChild(0).transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        inventorySlots = regularInventory.transform.childCount;
    }

    private void Update()
    {
        CheckFlashlightObtained();
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < inventorySlots; ++i)
        {
            if (i + 1 > inventory.keys.Count)
                regularInventory.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
            else
                regularInventory.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void CheckFlashlightObtained()
    {
        if (flashlightObtained)
            return;

        if (player.transform.GetChild(0).gameObject.activeSelf)
        {
            Debug.Log("Player has obtained flashlight");
            specialInventory.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            flashlightObtained = true;
        }
            
    }
}
