using UnityEngine;

namespace AlexzanderCowell
{
    public class RocketBootRelocatorScript : MonoBehaviour
    {
        private void Update()
        {
            GetComponent<ParticleSystem>().Play(); // Once this GameObject is spawned in it will play the particle system I have made for the Prefab.
        }
    }
}
