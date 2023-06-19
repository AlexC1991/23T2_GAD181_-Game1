using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace AlexzanderCowell
{
    public class TestingCheckP : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            if (other.CompareTag("Player")) // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            {
                SceneManager.LoadScene("TimeTrainingRoom");
            }
        }
        
    }
}
