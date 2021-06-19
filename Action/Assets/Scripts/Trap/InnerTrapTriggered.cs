using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerTrapTriggered : MonoBehaviour
{
    Trap trap;

    private void Awake()
    {
        trap = transform.parent.GetComponent<Trap>();
    }

    public void SetRadius(float radius)
    {
        transform.localScale = new Vector3(radius, 0.1f, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        trap.OnTriggerEnterChild(ref other);
    }
}
