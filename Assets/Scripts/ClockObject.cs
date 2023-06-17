using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class ClockObject : MonoBehaviour
    {
        private bool _moreTimeAdded; // Bool used to indicate if there was a pickup of the time GameObject and adds more time.
        public static event Action<bool> AddMoreTime; // Sends out an Action Event to the Time Script to indicate that more time needs to be added from the pickup of the GameObject.
        private readonly float _spinningSpeed = 20; // Speed for the time Game Object in the maze to rotate on it's axis. 
        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            if (other.CompareTag("Player")) // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            {
                _moreTimeAdded = true; // When the GameObject with tag called Player collides with the Time GameObject this is attached to it will say that there needs to be more time added with a yes/true.
                MoreTimePlease(); // Activates the Action Event.
                gameObject.SetActive(false); // Destroys the GameObject this script is attached to.
            }
            else
            {
                _moreTimeAdded = false; // Player dose not collide with it making the bool false.
            }
        }

        private void MoreTimePlease() // Start Method for the Action Event.
        {
            AddMoreTime?.Invoke(_moreTimeAdded); // Sends out the Action Event for the time script to catch and use the bool that is turning on/off for adding more time with the collision above.
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * (_spinningSpeed * Time.deltaTime)); // Rotates the GameObject on the Z Axis making it spin in place. 
        }
    }
}
