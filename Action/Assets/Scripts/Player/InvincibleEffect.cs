using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]

public class InvincibleEffect : MonoBehaviour
{
    [SerializeField] float blinkFrequency = 0.05f;
    float timer = 0f;
    
    HealthManager healthManager = null;
    SkinnedMeshRenderer skinRenderer = null;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        timer = blinkFrequency;
        skinRenderer = transform.GetChild(1).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        if (healthManager.invincible && healthManager.health > 0)
            ActivateInvincibleEffect();
        else
        {
            timer = blinkFrequency;
            skinRenderer.enabled = true;
        }
            

    }

    private void ActivateInvincibleEffect()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ToggleMeshRenderer();
            timer = blinkFrequency;
        }
    }

    private void ToggleMeshRenderer()
    {
        if (skinRenderer.enabled)
            skinRenderer.enabled = false;
        else
            skinRenderer.enabled = true;
    }
}
