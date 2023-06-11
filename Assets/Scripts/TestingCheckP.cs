using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class TestingCheckP : MonoBehaviour
    {
        [HideInInspector] public bool playerTestingIt; // Bool being used in Character Movement script to know when the player interacts it. Says player testing due to being the tutorial room.

        public static event Action<bool> _RelocateToTimeRoomEvent; 
        
        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            if (other.CompareTag("Player")) // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            {
                playerTestingIt = true; // Player collides with it making the bool true.
                _RelocateToTimeRoomEvent?.Invoke(playerTestingIt);
            }
        }
        
    }
}
