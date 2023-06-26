using System;
using UnityEngine;

public class TrainingShoes : MonoBehaviour
{
    private bool yeaHeGotTouched;
    public static event Action<bool> _TouchedItEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            yeaHeGotTouched = true;
        }
    }
    private void Update()
    {
        _TouchedItEvent?.Invoke(yeaHeGotTouched);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            yeaHeGotTouched = false;
        }
    }
}
