using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMechanism : MonoBehaviour
{
    [SerializeField] int code = 1;
    [Range(0.001f, 0.025f)]
    [SerializeField] float dissolveStep = .01f;
    [SerializeField] Color baseColor = new Color(141f, 0f, 255f);

    private string DissolveShaderInput = "Vector1_598F448E";
    private string DissolveShaderBaseColor = "Color_88986260";

    private PressurePlate plate = null;
    private BoxCollider boxCollider = null;
    private MeshRenderer meshRenderer = null;
    private bool active = false;

    private void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.SetColor(DissolveShaderBaseColor, baseColor);
        PressurePlate[] plates = FindObjectsOfType<PressurePlate>();
        for (int i = 0; i < plates.Length; ++i)
        {
            if (plates[i].code == code)
                plate = plates[i];
        }
    }

    private void Update()
    {
        PollPressurePlateTrigger();
        if (active && meshRenderer.material.GetFloat(DissolveShaderInput) < 1f)
            StartCoroutine(OpenMecanism());  
        else if(meshRenderer.material.GetFloat(DissolveShaderInput) > -1f)
            StartCoroutine(CloseMecanism());
    }

    IEnumerator OpenMecanism()
    {
        meshRenderer.material.SetFloat(DissolveShaderInput, meshRenderer.material.GetFloat(DissolveShaderInput) + dissolveStep);
        yield return new WaitUntil(() => meshRenderer.material.GetFloat(DissolveShaderInput) >= 1f);
        boxCollider.enabled = false;
    }

    IEnumerator CloseMecanism()
    {
        meshRenderer.material.SetFloat(DissolveShaderInput, meshRenderer.material.GetFloat(DissolveShaderInput) - dissolveStep);
        yield return new WaitUntil(() => meshRenderer.material.GetFloat(DissolveShaderInput) <= -1f);
        boxCollider.enabled = true;
    }

    private void PollPressurePlateTrigger()
    {
        if (plate.GetTriggeredState() && !active)
            active = true;
        else if (!plate.GetTriggeredState() && active)
            active = false;
    }
}
