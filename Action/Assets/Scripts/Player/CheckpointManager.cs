using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
     public Transform lastCheckpoint = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
            lastCheckpoint = other.transform;
    }
}
