using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    [SerializeField] Light spotlight = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToggleFlashlight"))
        {
            if (spotlight.enabled)
                spotlight.enabled = false;
            else
                spotlight.enabled = true;
        }
    }
}
