using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    [SerializeField] float frequency = 0.5f;
    float timer = 0f;
    Light light = null;

    private void Start()
    {
        timer = frequency;
        light = GetComponent<Light>();
    }
    private void Update()
    {
        if (timer <= 0f)
        {
            if (light.enabled)
                light.enabled = false;
            else
                light.enabled = true;
            timer = frequency;
        }
        timer -= Time.deltaTime;
    }
}