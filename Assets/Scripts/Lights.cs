using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    //variables here.

    public bool isFlickering = false;
    public float timeDelay;

    //Coroutine to start a method to make the lights flicker.
    //IEnumerator for the flickering method.
    //Collision to end the coroutine and stop lights from flickering. Lights remain on to mark places player has already been.

    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine (FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.01f, 0.3f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.3f, 0.8f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
