using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    private Light l;

    public float intensity = 5f;
    public float lightningDelay = 4f;
    public float lightningDuration = 1f;

    private float oldTime = 0f;
    private float strikeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!l.enabled)
            return;

        float delay = Random.value * lightningDelay;
        float duration = Random.value * lightningDuration;

        //TODO : add multiple flashes
        flash(delay, duration);
    }

    private void flash(float delay, float duration)
    {
        if (Time.time - oldTime >= delay)
        {
            l.intensity = intensity;
            tag = "Thunder";

            oldTime = Time.time;
            strikeTime = oldTime;
        }

        if (Time.time - strikeTime >= duration)
        {
            l.intensity = 0f;
            tag = "Untagged";

            strikeTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        l.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        l.enabled = false;
    }
}
