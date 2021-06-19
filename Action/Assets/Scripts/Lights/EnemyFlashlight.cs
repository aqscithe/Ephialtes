using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashlight : MonoBehaviour
{
    private GameObject player = null;
    private Light parentLight;
    private SphereCollider sphCollider;

    // Start is called before the first frame update
    void Start()
    {
        parentLight = transform.parent.GetComponent<Light>();
        sphCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayerDetected())
            Debug.Log("shot");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        player = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        player = null;
    }

    public bool isPlayerDetected()
    {
        if (player == null)
            return false;

        Vector3 dir = (transform.parent.rotation * Vector3.forward).normalized;
        //field of view
        float alpha = parentLight.spotAngle / 2;
        //vector from origin to target object
        Vector3 OP = (player.transform.position - transform.parent.position).normalized;

        float beta = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, OP));

        if (beta <= alpha && rayHits(transform.parent.position, player.transform.position))
            return true;
        else
            return false;
    }

    public bool rayHits(Vector3 from, Vector3 to)
    {
        RaycastHit hit;

        Vector3 lightDir = (transform.parent.rotation * Vector3.forward).normalized;
        Vector3 dir = (to - from).normalized;

        float radius = sphCollider.radius;

        //float sx = player.transform.localScale.x / 2;
        //float sy = player.transform.localScale.y;

        ////raycast to player's edge (top, bot, right, left)
        //Vector3[] toEdges = new Vector3[] { new Vector3(to.x, to.y + sy, to.z),
        //                                    new Vector3(to.x, to.y - sy, to.z),
        //                                    new Vector3(to.x + sx * lightDir.z, to.y, to.z + sx * lightDir.x),
        //                                    new Vector3(to.x - sx * lightDir.z, to.y, to.z - sx * lightDir.x) };

        //Vector3[] dirEdges = new Vector3[] { (toEdges[0] - from).normalized,
        //                                     (toEdges[1] - from).normalized,
        //                                     (toEdges[2] - from).normalized,
        //                                     (toEdges[3] - from).normalized };

        //for (int i = 0; i < 4; ++i)
        //{
            Debug.DrawLine(from, to);
            if (Physics.Raycast(from, dir, out hit, radius))
                if (hit.collider.gameObject.CompareTag("Player"))
                    return true;
        //}

        return false;
    }
}
