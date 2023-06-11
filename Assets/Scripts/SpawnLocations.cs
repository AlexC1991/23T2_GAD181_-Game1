using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class SpawnLocations : MonoBehaviour
    {
        private bool insideOfRocketRoom; // This Bool gives a yes or no when the character is located in the rocket room or not.
        private bool insideOfCheckPointRoom; // This Bool gives a yes or no when the character is located in the checkpoint room or not.
        private bool insideOfTimerRoom;
        private bool insideOfMainMazeRoom;

        [Header("UI Elements")] 
        [SerializeField] private Text topTrainingText;
        [SerializeField] private Text bottomTrainingText;

        public static event Action<bool> _CheckPointRoomEvent;
        public static event Action<bool> _TimerRoomEvent;
        public static event Action<bool> _RocketBootRoomEvent;
        public static event Action<bool> _MainMazeRoomEvent;

        private void Start()
        {
            CheckPointSpawnRoom();
        }

        private void OnEnable()
        {
            RocketRoomTrainer._CharactersBootsNeedSpawning += RelocateToMazeRoomInstead;
            TimerScript.RelocateToRocketRoomEvent += RocketTrainingStartsNow;
            CharacterMovement._ResetCCurrentMessageEvent += RelocatedToTheTimeRoom;
        }

        private void OnDisable()
        {
            RocketRoomTrainer._CharactersBootsNeedSpawning -= RelocateToMazeRoomInstead;
            CharacterMovement._ResetTCurrentMessageEvent -= RocketTrainingStartsNow;
            CharacterMovement._ResetCCurrentMessageEvent -= RelocatedToTheTimeRoom;
        }

        private void TimeSpawnRoom()
        {
            Debug.Log("Inside of Time Room");
            insideOfTimerRoom = true;
            insideOfRocketRoom = false;
            insideOfCheckPointRoom = false;
            insideOfMainMazeRoom = false;
            topTrainingText.text = "This is the Time Training Room".ToString();
            bottomTrainingText.text = "Press 'E' to talk to the trainer in the room".ToString();
            topTrainingText.GetComponent<Text>().color = Color.cyan;
            bottomTrainingText.GetComponent<Text>().color = Color.green;
            _CheckPointRoomEvent?.Invoke(insideOfCheckPointRoom);
            _TimerRoomEvent?.Invoke(insideOfTimerRoom);
            _RocketBootRoomEvent?.Invoke(insideOfRocketRoom);
            _MainMazeRoomEvent?.Invoke(insideOfMainMazeRoom);
        }

        private void MainStartRoomSpawn()
        {
            Time.timeScale = 0; // Sets the game to be paused while u have the before game starts UI image.
            insideOfRocketRoom = false; // Says you are no longer in the rocket room.
            insideOfTimerRoom = false;
            insideOfCheckPointRoom = false;
            insideOfMainMazeRoom = true;
            _TimerRoomEvent?.Invoke(insideOfTimerRoom);
            _CheckPointRoomEvent?.Invoke(insideOfCheckPointRoom);
            _RocketBootRoomEvent?.Invoke(insideOfRocketRoom);
            _MainMazeRoomEvent?.Invoke(insideOfMainMazeRoom);
        }

        private void CheckPointSpawnRoom()
        {
            Debug.Log("Inside of Checkpoint Room");
            insideOfCheckPointRoom = true; // Says the character is in the check point room which in this case is yes.
            insideOfRocketRoom = false;
            insideOfTimerRoom = false;
            insideOfMainMazeRoom = false;
            topTrainingText.text = "This is the Checkpoint Training Room".ToString();
            bottomTrainingText.text = "Press 'E' to talk to the trainer in the room".ToString();
            topTrainingText.GetComponent<Text>().color = Color.white;
            bottomTrainingText.GetComponent<Text>().color = Color.green;
            _TimerRoomEvent?.Invoke(insideOfTimerRoom);
            _CheckPointRoomEvent?.Invoke(insideOfCheckPointRoom);
            _RocketBootRoomEvent?.Invoke(insideOfRocketRoom);
            _MainMazeRoomEvent?.Invoke(insideOfMainMazeRoom);
        }

        private void RocketBootRoomSpawnPoint()
        {
            Debug.Log("Inside of Rocket Boot Room");
            insideOfRocketRoom = true; // Says yes the character is in the rocket training room.
            insideOfCheckPointRoom = false;
            insideOfTimerRoom = false;
            insideOfMainMazeRoom = false;
            topTrainingText.text = "This is the Rocket Boot Training Room".ToString();
            bottomTrainingText.text = "Press 'E' to talk to the trainer in the room".ToString();
            topTrainingText.GetComponent<Text>().color = Color.red;
            bottomTrainingText.GetComponent<Text>().color = Color.yellow;
            _TimerRoomEvent?.Invoke(insideOfTimerRoom);
            _CheckPointRoomEvent?.Invoke(insideOfCheckPointRoom);
            _RocketBootRoomEvent?.Invoke(insideOfRocketRoom);
            _MainMazeRoomEvent?.Invoke(insideOfMainMazeRoom);
        }

        private void RelocateToMazeRoomInstead(bool canRelocateToMaze)
        {
            if (canRelocateToMaze) MainStartRoomSpawn();
        }
        private void RocketTrainingStartsNow(bool didRelocateToRRoom)
        {
            if (didRelocateToRRoom) RocketBootRoomSpawnPoint();
        }

        private void RelocatedToTheTimeRoom(bool didRelocateToTRoom)
        {
            if (didRelocateToTRoom) TimeSpawnRoom();
        }
    }
}
