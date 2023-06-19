using UnityEngine;

namespace AlexzanderCowell
{
    public class ClockTester : MonoBehaviour
    {
        private readonly float _spinningSpeed = 28; // Speed for the time Game Object in the maze to rotate on it's axis. 
        [HideInInspector] public bool timerGotHit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                timerGotHit = true;
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * (_spinningSpeed * Time.deltaTime)); // Rotates the GameObject on the Z Axis making it spin in place. 

            if (timerGotHit)
            {
                gameObject.SetActive(false);
            }
        }
    }
}