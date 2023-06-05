using UnityEngine;

namespace AlexzanderCowell
{
    public class RocketBootRelocatorScript : MonoBehaviour
    {
        private void Update()
        {
            GetComponent<ParticleSystem>().Play(); // Once this GameObject is spawned in it will play the particle system I have made for the Prefab.
        }

        private void OnEnable() // Start of the Action Event.
        {
            CharacterMovement.DestroyTheBootsEvent += KillBoots; // Grabs the Action Event that is sent out from the Character Movement script to kill the boots/shoes.
        } 
        private void OnDisable() // Finished with the Action Event.
        {
            CharacterMovement.DestroyTheBootsEvent -= KillBoots; // Stops listening out for the Action Event.
        } 

        private void KillBoots(bool shoesOn) // Method used and activated via the Action Event sent out and if shoes are no longer used they are destroyed if it has a Tag of RocketBoots.
        {
            if (!shoesOn!) // If shoes On are false it starts the function below.
            {
                Destroy(GameObject.FindWithTag("RocketBoots")); // Destroys the game Object that holds the Tag RocketBoots.
            }
        }
    }
}
