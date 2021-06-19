using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDoor : MonoBehaviour
{
    int mirrorCount = 0;
    int mirrorsActiveCount = 0;
    bool doorDeactivated = false;

    private void Start()
    {
        mirrorCount = transform.childCount - 1;
    }

    private void Update()
    {
        if (!doorDeactivated)
            AllMirrorsActivated();
    }

    private void AllMirrorsActivated()
    {
        if (mirrorCount == mirrorsActiveCount)
        {
            GameObject door = transform.GetChild(0).gameObject;
            door.GetComponent<MeshRenderer>().enabled = false;
            door.GetComponent<BoxCollider>().enabled = false;
            doorDeactivated = true;
        }
    }

    public void IncrementMirrorsActivated()
    {
        mirrorsActiveCount++;
    }

    public void DecrementMirrorsActivated()
    {
        mirrorsActiveCount--;
    }
}
