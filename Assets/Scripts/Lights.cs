using System.Collections;
using UnityEngine;

public class Lights : MonoBehaviour
{
    //variables here:

    private bool isFlickering;
    private float timeDelay;
    private Light lightComponent;
    private float originalIntensity;
    private float targetIntensity;
    private float intensityChangeSpeed;

    private void Start()
    {
        lightComponent = GetComponent<Light>();
        originalIntensity = lightComponent.intensity;
        isFlickering = true;
        targetIntensity = 10;
    }

    private void Update()
    {
        Debug.Log("Is Flicking Bool " + isFlickering);
        
        if (isFlickering)
        {
            timeDelay = 0.5f * Time.deltaTime + Random.Range(0.2f, 4.0f);
            lightComponent.enabled = true;
            lightComponent.intensity = timeDelay += 0.1f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Detected");
            intensityChangeSpeed = Mathf.Clamp(intensityChangeSpeed, originalIntensity, 10);
            isFlickering = false;
            lightComponent.intensity = targetIntensity += intensityChangeSpeed * Time.deltaTime;
        }
    }
}