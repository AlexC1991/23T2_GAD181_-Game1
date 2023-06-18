using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lights : MonoBehaviour
{
    //variables here:
    
    public bool isFlickering = false;
    public float timeDelay;
    private Light lightComponent;
    private float originalIntensity;
    private Coroutine flickeringCoroutine;

    //Method to create flickering lights.
    //Method to stop flickering lights.
    //Method to change intensity to brightest intensity after flickering stops.
    
    private void Start()
    {
        lightComponent = GetComponent<Light>();
        originalIntensity = lightComponent.intensity;
    }

    private void Update()
    {
        if (!isFlickering && flickeringCoroutine == null)
        {
            flickeringCoroutine = StartCoroutine(FlickeringLight());
        }
    }

    private IEnumerator FlickeringLight()
    {
        isFlickering = true;
        lightComponent.enabled = false;
        timeDelay = Random.Range(0.01f, 0.3f);
        yield return new WaitForSeconds(timeDelay);
        lightComponent.enabled = true;
        lightComponent.intensity = originalIntensity * Random.Range(2f, 10f);
        timeDelay = Random.Range(0.3f, 0.8f);
        yield return new WaitForSeconds(timeDelay);
        lightComponent.intensity = originalIntensity;
        isFlickering = false;
        flickeringCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (flickeringCoroutine != null)
            {
                StopCoroutine(flickeringCoroutine);
                flickeringCoroutine = null;
            }

            float targetIntensity = originalIntensity * 8f;
            float intensityChangeSpeed = 10f;

            StartCoroutine(IncreaseIntensity(targetIntensity, intensityChangeSpeed));
            
            Debug.Log("trigger working");
        }
    }

    private IEnumerator IncreaseIntensity(float targetIntensity, float intensityChangeSpeed)
    {
        while (lightComponent.intensity < targetIntensity)
        {
            lightComponent.intensity += intensityChangeSpeed * Time.deltaTime;
            yield return null;
        }

        lightComponent.intensity = targetIntensity;
    }
}