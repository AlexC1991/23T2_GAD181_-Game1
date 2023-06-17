using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class CheckPoint : MonoBehaviour
    {
        private bool _newSavePoint; // This bool detects when a player goes through the collider and sets off a true to allow the event to send off to the character movement that its okay to save here.

        public static event Action<bool> SaveHereInstead; // Sends an Action Event out called SaveHereInstead. This is caught by Character Movement script to use to save the new position.

        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
            {
                if (other.CompareTag("Player")) // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
                {
                    _newSavePoint = true; // Collision with the GameObject with the tag called Player will make this bool turn true.
                    NewSaveEvent(); // Starts the Method below to activate the Action Event.
                    gameObject.SetActive(false); // Destroys the GameObject this script is attached to.
                }
                else
                {
                    _newSavePoint = false; // If there is no collision the new save point bool will always be false.
                }
            }

            private void NewSaveEvent() // Start Method for the Action Event.
            {
                SaveHereInstead?.Invoke(_newSavePoint); // The Action Event sends out the bool for the Character Movement script to catch it and use the bool that is used in this script.
            }

    }
}
