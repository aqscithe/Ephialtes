using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]

public class CreateLightCollider : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] bool modifiable = false;
    [SerializeField] float intensity = 400f;

    [Header("Spot Light")]
    [SerializeField] float height = 15f;
    [SerializeField] float innerAngle = 20f;
    [SerializeField] float outerAngle = 30f;

    [Header("Point Light")]
    [SerializeField] float radius = 10f;


    Light light = null;
    SphereCollider sphereCollider = null;
    BoxCollider boxCollider = null;


    private void Start()
    {
        light = gameObject.GetComponent<Light>();
        AddCollider();
        UpdateLightParameters();
    }

    private void AddCollider()
    {
        switch (light.type)
        {
            case LightType.Spot:
                boxCollider = gameObject.AddComponent<BoxCollider>();
                boxCollider.isTrigger = true;
                break;
            case LightType.Point:
                sphereCollider = gameObject.AddComponent<SphereCollider>();
                sphereCollider.isTrigger = true;
                break;
            default:
                Debug.Log("Unsupported Light type - " + light.type.ToString());
                break;
        }
    }

    private void Update()
    {
        if (modifiable)
            UpdateLightParameters();
    }

    private void UpdateLightParameters()
    {
        switch (light.type)
        {
            case LightType.Spot:
                UpdateSpotlight();
                break;
            case LightType.Point:
                UpdatePointlight();
                break;
            default:
                Debug.Log("Unsupported Light type - " + light.type.ToString());
                break;
        }
        light.intensity = intensity;
    }

    private void UpdatePointlight()
    {
        light.range = radius;
        sphereCollider.radius = radius;
    }

    private void UpdateSpotlight()
    {
        light.range = height;
        light.innerSpotAngle = innerAngle;
        light.spotAngle = outerAngle;
        float coneRadius = height * Mathf.Tan(innerAngle * Mathf.PI / 180f);
        boxCollider.size = new Vector3(coneRadius, coneRadius, height);
        boxCollider.center = new Vector3(0f, 0f, height / 2f);
    }
}
