using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
    
    public class TrophyWin : MonoBehaviour
    {
        private readonly float _spinningSpeed = 10; // Spinning speed of the trophy GameObject.
        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            if (other.CompareTag("Player")) // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            {
                SceneManager.LoadScene("You Won!"); // Loads the scene called You Won!.
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * (_spinningSpeed * Time.deltaTime)); // Rotates the GameObject on the Z Axis making it spin in place. 
        }
    }
}