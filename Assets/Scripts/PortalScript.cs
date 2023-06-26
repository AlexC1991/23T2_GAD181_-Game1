using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
    public class PortalScript : MonoBehaviour
    {
        private bool spawnNow;
        [SerializeField] private RocketRoomTrainer rTrainer;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene("Maze 1");
            }
        }

        private void Start()
        {
            gameObject.SetActive(true);
            spawnNow = false;
        }

        private void Update()
        {
            if (rTrainer.rocketBootState)
            {
                spawnNow = true;
            }
            if (spawnNow)
            {
                gameObject.SetActive(true);
            }
        }

    }
}
