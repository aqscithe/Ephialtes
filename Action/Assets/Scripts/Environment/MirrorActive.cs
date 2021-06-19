using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorActive : MonoBehaviour
{
    bool active = false;
    LightDoor lightDoor = null;

    private void Awake()
    {
        if (transform.parent.CompareTag("LightDoor"))
            lightDoor = transform.parent.GetComponent<LightDoor>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "LightCollider")
            other.transform.parent.transform.GetComponent<HandleLightRaycast>().Activate();

        if (!active && other.gameObject.name == "LightVertex(Clone)")
        {
            active = true;
            if (lightDoor != null)
                lightDoor.IncrementMirrorsActivated();
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "LightCollider")
            other.transform.parent.GetComponent<HandleLightRaycast>().Deactivate();

        if (active && other.gameObject.name == "LightVertex(Clone)")
        {
            active = false;
            if (lightDoor != null)
                lightDoor.DecrementMirrorsActivated();
        }
    }
}
