using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
    public class NewLocationSpawn : MonoBehaviour
    {
        private Vector3 _startSpawnPosition;
        [SerializeField] private Transform startRoomSpawn;
        [Header("Character Controller")] [SerializeField]
        private CharacterController characterCon;
        
        private void Awake()
        {
            _startSpawnPosition = startRoomSpawn.position;
        }
        private void Start()
        {
            characterCon.enabled = true;
        }
        public void StartSpawn()
        {
            characterCon.transform.position = _startSpawnPosition;
        }

        public void TrainingStart()
        {
            SceneManager.LoadScene("CheckPointTrainingRoom");
        }
    }
}
