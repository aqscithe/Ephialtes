using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] FollowPlayer fp = null;

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Light"))
        {
            Debug.Log(other.gameObject.name + " is lighting " + gameObject.name);
            fp.lit = true;
        }
    }*/

    private void FixedUpdate()
    {
        fp.lit = false;
    }

    // have enemies send a raycast back out to detect if anything is obstructing path to light
    private void OnTriggerStay(Collider other)
    {
        if (!fp.playerDetected)
            return;

        if (!other.CompareTag("Light"))
            if (!other.CompareTag("Thunder"))
                return;

        if (other.CompareTag("Light"))
        {
            Vector3 spotPos = other.transform.parent.position;
            Vector3 lightDir = transform.position - spotPos;

            RaycastHit hit;

            if (Physics.Raycast(spotPos, lightDir, out hit, lightDir.magnitude))
            {
                //Debug.DrawLine(transform.position, spotPos);
                //Debug.DrawLine(hit.point, hit.point + new Vector3(0, 10, 0));
                //Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.gameObject != transform.parent.gameObject)
                {
                    fp.lit = false;
                    return;
                }
            }
        }

        if (other.bounds.Contains(other.bounds.min) && other.bounds.Contains(other.bounds.max))
        {
            fp.lit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Equals("Light"))
            return;

        Debug.Log(this.gameObject.name + " is no longer lit");
        fp.lit = false;
    }
}
