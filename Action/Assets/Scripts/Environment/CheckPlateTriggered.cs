using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlateTriggered : MonoBehaviour
{

    public PressurePlate pressurePlate;

    private void Awake()
    {
        pressurePlate = transform.parent.GetComponent<PressurePlate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        pressurePlate.OnTriggerEnterChild(ref other);   
    }

    private void OnTriggerExit(Collider other)
    {
        pressurePlate.OnTriggerExitChild(ref other);
    }

}
