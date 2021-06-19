using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0f ||
            Mathf.Abs(Input.GetAxis("Horizontal")) > 0f)
        {
            MovePlayer mp = other.GetComponent<MovePlayer>();
            mp.Climb(climbSpeed);
        }
    }
}
