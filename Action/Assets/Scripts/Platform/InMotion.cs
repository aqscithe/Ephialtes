using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    LIGHT,
    DARK,
}

public class InMotion : MonoBehaviour
{
    public PlatformType t = PlatformType.LIGHT;
    [HideInInspector] public bool isMoving;

    private bool lit = false;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = t == PlatformType.DARK;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lit)
            isMoving = t == PlatformType.LIGHT;
        else
            isMoving = t == PlatformType.DARK;

        lit = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Light"))
            if (!other.CompareTag("Thunder"))
                return;

        if (other.CompareTag("Light"))
        {
            if (other.gameObject.name.Contains("LightVertex"))
            {
                lit = true;
                return;
            }    
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
                    lit = false;
                    return;
                }
            }
        }

        lit = true;
    }
}
