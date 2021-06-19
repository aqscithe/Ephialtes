using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHealthbar : MonoBehaviour
{

    HealthManager hm = null;
    List<GameObject> healthSlots = new List<GameObject>();

    private void Awake()
    {
        hm = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        foreach (Transform child in transform.GetChild(0).transform)
        {
            healthSlots.Add(child.gameObject);
        }

        ActivateHealthSlots();
    }

    private void Update()
    {
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        for (int i = 0; i < hm.maxHealth; i++)
        {
            if (hm.health < i + 1)
                healthSlots[i].gameObject.SetActive(false);
        }
    }

    public void ActivateHealthSlots()
    {
        Debug.Log("max health " + hm.maxHealth.ToString());
        for (int i = 0; i < hm.maxHealth; ++i)
        {
            healthSlots[i].gameObject.SetActive(true);
        }
    }
}
