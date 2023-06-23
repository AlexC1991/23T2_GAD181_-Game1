using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour
{

    public AudioSource SFX;

    public void PlaySound()
    {
        SFX.Play();
    }
}
