using UnityEngine;

namespace AlexzanderCowell
{
    public class FallScript2 : MonoBehaviour
    {
        [SerializeField] private CharacterController playerController;
        [SerializeField] private Transform fallRespawnPoint2;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerController.transform.position = fallRespawnPoint2.position;
            }
        }
    }
}
