using UnityEngine;

namespace AlexzanderCowell
{
    public class NewLocationSpawn : MonoBehaviour
    {
        private Vector3 _timeSpawnTrainingRoom;
        private Vector3 _rocketSpawnTrainingRoom;
        private Vector3 _checkPointSpawnTrainingRoom;
        private Vector3 _startSpawnPosition;
        /*private Vector3 resetSpawnPositionPoint;*/
        
        [Header("SpawnLocations")]
        [SerializeField] private Transform timeRoomSpawn;
        [SerializeField] private Transform startRoomSpawn;
        [SerializeField] private Transform checkPointRoomSpawn;
        [SerializeField] private Transform rocketRoomSpawn;
        /*[SerializeField] private ScriptableObjectBase resetPointSpawn; // This is the position the character will go when the game resets or the character loses.*/

        [Header("Character Controller")] 
        [SerializeField] private CharacterController characterCon;


        private void Awake()
        {
            _timeSpawnTrainingRoom = timeRoomSpawn.position;
            _rocketSpawnTrainingRoom = rocketRoomSpawn.position;
            _checkPointSpawnTrainingRoom = checkPointRoomSpawn.position;
            _startSpawnPosition = startRoomSpawn.position;
            /*resetSpawnPositionPoint = resetPointSpawn.spawnToThisPosition.position;*/
        }

        private void Start()
        {
            characterCon.enabled = true;
        }


        public void StartSpawn()
        {
            characterCon.transform.position = _startSpawnPosition;
            //characterCon.enabled = true;
        }

        public void RocketSpawnRoom()
        {
            //characterCon.enabled = false;
            characterCon.transform.position = _rocketSpawnTrainingRoom;
            //characterCon.enabled = true;
        }

        public void CheckPointTrainRoomSpawn()
        {
            characterCon.transform.position = _checkPointSpawnTrainingRoom;
            //characterCon.enabled = true;
        }

        public void TimeTrainRoomSpawn()
        {
            //characterCon.enabled = false;
            characterCon.transform.position = _timeSpawnTrainingRoom;
            //characterCon.enabled = true;
        }

        /*public void ResetSpawnPointActivated()
        {
            characterCon.transform.position = resetSpawnPositionPoint;
        }*/

    }
}
