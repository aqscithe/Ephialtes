using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HandleLightRaycast : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField] int reflections = 3;
    [SerializeField] float maxLength = 100f;
    [SerializeField] float noHitLength = 10f;
    [Range(1f, 12f)]
    [SerializeField] float RIAir = 1f;
    [Range(1f, 12f)]
    [SerializeField] float RIGlass = 1.517f;

    [SerializeField] Vector3 zeroPos = new Vector3(0f, 0f, 0f);
    [Range(0, 5)]
    [SerializeField] int rayDir = 0;

    [SerializeField] GameObject reflectLights = null;
    Transform[] reflectLightsChildren;
    [SerializeField] GameObject lightVertexPrefab = null;

    MeshCollider lightCollider = null;

    Vector3[] rayDirs = null;
    LineRenderer lineRenderer = null;
    Ray ray;
    RaycastHit hit;

    int reflectionCount = 0;

    int activeMirrors = 0;
    bool active = false;


    private void Awake()
    {
        reflectLights = new GameObject("ReflectionLights");
        lineRenderer = GetComponent<LineRenderer>();
        lightCollider = transform.GetChild(0).GetComponent<MeshCollider>();
        for (int i = 0; i < reflections; ++i)
        {
            AddLightVertex();
        }
    }

    private void Start()
    {
        reflectLightsChildren = reflectLights.GetComponentsInChildren<Transform>();
        rayDirs = new[] { transform.forward, transform.forward * -1, transform.up, transform.up * -1, transform.right, transform.right * -1 };
    }

    private void Update()
    {
        if (!active)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;   

        reflectionCount = 0;

        //UpdateReflections();
        float remainingLength = maxLength;

        for (int i = 1; i <= reflections; ++i)
        {
            if (i == 1)
            {
                ray = new Ray(transform.position, GetRayDirection());
                reflectLightsChildren[i].position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + zeroPos;
                reflectLightsChildren[i].GetComponent<Light>().enabled = true;
                Debug.DrawRay(transform.position, ray.direction, Color.white);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
                {
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(0, hit.point);
                }
            }
            else if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {

                UpdateRemainingLength(ref remainingLength);
                if (hit.transform.CompareTag("Glass"))
                {
                    RefractRay(i);
                }
                else if (hit.transform.CompareTag("Mirror"))
                {
                    ReflectRay(i);
                    //reflectionCount++;
                }
                else
                {
                    reflectLightsChildren[i - 1].position = hit.point;
                    //reflectionCount++;
                }

            }
            else
            {
                Vector3 currentPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                Vector3 pos = ray.direction.normalized * noHitLength + currentPos;
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
            }
        }
        //Disable inactive reflective lights
        //for (int i = reflectionCount + 1; i <= reflections; ++i)
        //{
        //    tempLightsChildren[i].GetComponent<Light>().enabled = false;
        //    //tempLightsChildren[i].GetComponent<SphereCollider>().enabled = false;
        //}

    }

    public void Activate()
    {
        activeMirrors++;
        if (!active && activeMirrors > 0)
            active = true;
    }

    public void Deactivate()
    {
        activeMirrors--;
        if (active && activeMirrors <= 0)
            active = false;
    }

    private void AddLightVertex()
    {
        GameObject light = Instantiate(lightVertexPrefab, Vector3.zero, Quaternion.identity);
        light.transform.parent = reflectLights.transform;
    }

    private void UpdateReflections()
    {
        reflectLightsChildren = reflectLights.GetComponentsInChildren<Transform>();
        if (reflections < reflectLightsChildren.Length - 1)
        {
            int start = reflectLightsChildren.Length - 1;
            int end = reflections;
            for (int i = start; i >= end; --i)
                Destroy(reflectLightsChildren[i].gameObject);
        }
        else if (reflections > reflectLightsChildren.Length - 1)
        {
            int difference = reflections - (reflectLightsChildren.Length - 1);
            for (int i = 0; i < difference; ++i)
                AddLightVertex();
        }
    }

    private void ReflectRay(int i)
    {
        ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
        Debug.DrawRay(hit.point, ray.direction, Color.yellow);
        reflectLightsChildren[i - 1].position = hit.point;
        reflectLightsChildren[i - 1].GetComponent<Light>().enabled = true;
        reflectLightsChildren[i - 1].GetComponent<SphereCollider>().enabled = true;
    }

    private void RefractRay(int i)
    {
        ray = new Ray(hit.point, Refract(RIAir, RIGlass, hit.normal, ray.direction));
        Debug.DrawRay(hit.point, ray.direction, Color.blue);
        reflectLightsChildren[i].position = hit.point;
        reflectLightsChildren[i].GetComponent<Light>().enabled = true;
    }

    private void UpdateRemainingLength(ref float remainingLength)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
        remainingLength -= Vector3.Distance(ray.origin, hit.point);
    }

    private Vector3 GetRayDirection()
    {
        Quaternion objRotation = transform.rotation;
        Vector3 direction = objRotation * rayDirs[rayDir];
        return direction;
    }

    private Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident)
    {
        surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
        incident.Normalize();

        return (RI1 / RI2 * Vector3.Cross(surfNorm, Vector3.Cross(-surfNorm, incident)) - surfNorm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident) * (RI1 / RI2 * RI1 / RI2), Vector3.Cross(surfNorm, incident)))).normalized;
    }
}

