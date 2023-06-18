using UnityEngine;

namespace AlexzanderCowell
{
    public class FallScript1 : MonoBehaviour
    {
        [SerializeField] private CharacterController playerController;
        [SerializeField] private Transform fallRespawnPoint;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerController.transform.position = fallRespawnPoint.position;
            }
        }
    }
}
