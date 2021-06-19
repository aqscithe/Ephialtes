using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLights : MonoBehaviour
{
    Light[] lights = null;

    private void Awake()
    {
        lights = FindObjectsOfType<Light>();
        for (int i = 0; i < lights.Length; ++i)
        {
            Light light = lights[i];
            if (!light.CompareTag("Player_Light") && light.type != LightType.Directional )
            {
                if (light.gameObject.GetComponent<CreateLightCollider>() == null)
                    light.gameObject.AddComponent<CreateLightCollider>();
            }
                
        }
    }
}
