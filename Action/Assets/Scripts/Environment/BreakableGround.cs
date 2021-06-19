using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGround : MonoBehaviour
{
    public float breakTime = 1f;
    public float destroyedTime = 5f;
    public float breakForce = 3f;

    private bool broken = false;

    private bool isActivated = false;
    private float onFloorTime = 0f;

    private void Update()
    {
        if (!isActivated)
            onFloorTime = Time.time;

        if (Time.time - onFloorTime >= breakTime && !broken)
        {
            transform.Translate(0, -breakForce, 0);
            broken = true;
        }

        if (Time.time - onFloorTime >= destroyedTime)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        isActivated = true;
    }
}
