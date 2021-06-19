using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class Revealed : MonoBehaviour
{
    private MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mr.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
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

                if (hit.collider.gameObject != this.gameObject)
                {
                    mr.enabled = false;
                    return;
                }
            }
        }

        mr.enabled = true;
    }
}
