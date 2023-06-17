using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class SpawnLocations : MonoBehaviour
    {
        private bool _insideOfRocketRoom; // This Bool gives a yes or no when the character is located in the rocket room or not.
        private bool _insideOfCheckPointRoom; // This Bool gives a yes or no when the character is located in the checkpoint room or not.
        private bool _insideOfTimerRoom;
        private bool _insideOfMainMazeRoom;

        [Header("UI Elements")] 
        [SerializeField] private Text topTrainingText;
        [SerializeField] private Text bottomTrainingText;
        [SerializeField] private GameObject topTrainingTextOnOff;
        [SerializeField] private GameObject bottomTrainingTextOnOff;

        public static event Action<bool> checkPointRoomEvent;
        public static event Action<bool> timerRoomEvent;
        public static event Action<bool> rocketBootRoomEvent;
        public static event Action<bool> mainMazeRoomEvent;
        private void OnEnable()
        {
            TimerScript.RelocateToRocketRoomEvent += RocketTrainingStartsNow;
            CharacterMovement.ResetCCurrentMessageEvent += RelocatedToTheTimeRoom;
        }
        private void Start()
        {
            topTrainingTextOnOff.SetActive(false);
            bottomTrainingTextOnOff.SetActive(false);
        }
        private void Update()
        {
            checkPointRoomEvent?.Invoke(_insideOfCheckPointRoom);
            timerRoomEvent?.Invoke(_insideOfTimerRoom);
            rocketBootRoomEvent?.Invoke(_insideOfRocketRoom);
            mainMazeRoomEvent?.Invoke(_insideOfMainMazeRoom);
        }
        public void TimeSpawnRoom()
        {
            topTrainingTextOnOff.SetActive(true);
            bottomTrainingTextOnOff.SetActive(true);
            _insideOfTimerRoom = true;
            _insideOfRocketRoom = false;
            _insideOfCheckPointRoom = false;
            _insideOfMainMazeRoom = false;
            topTrainingText.text = "This is the Time Training Room";
            bottomTrainingText.text = "Press 'E' to talk to the trainer in the room";
            topTrainingText.GetComponent<Text>().color = Color.cyan;
            bottomTrainingText.GetComponent<Text>().color = Color.green;
        }
        public void MainStartRoomSpawn()
        {
            topTrainingTextOnOff.SetActive(false);
            bottomTrainingTextOnOff.SetActive(false);
            _insideOfRocketRoom = false; // Says you are no longer in the rocket room.
            _insideOfTimerRoom = false;
            _insideOfCheckPointRoom = false;
            _insideOfMainMazeRoom = true;
        }
        public void CheckPointSpawnRoom()
        {
            topTrainingTextOnOff.SetActive(true);
            bottomTrainingTextOnOff.SetActive(true);
            _insideOfCheckPointRoom = true; // Says the character is in the check point room which in this case is yes.
            _insideOfRocketRoom = false;
            _insideOfTimerRoom = false;
            _insideOfMainMazeRoom = false;
            topTrainingText.text = "This is the Checkpoint Training Room";
            bottomTrainingText.text = "Press 'E' to talk to the trainer in the room";
            topTrainingText.GetComponent<Text>().color = Color.white;
            bottomTrainingText.GetComponent<Text>().color = Color.green;
        }
        public void RocketBootRoomSpawnPoint()
        {
            topTrainingTextOnOff.SetActive(true);
            bottomTrainingTextOnOff.SetActive(true);
            _insideOfRocketRoom = true; // Says yes the character is in the rocket training room.
            _insideOfCheckPointRoom = false;
            _insideOfTimerRoom = false;
            _insideOfMainMazeRoom = false;
            topTrainingText.text = "This is the Rocket Boot Training Room";
            bottomTrainingText.text = "Press 'E' to talk to the trainer in the room";
            topTrainingText.GetComponent<Text>().color = Color.red;
            bottomTrainingText.GetComponent<Text>().color = Color.yellow;
        }
        private void RocketTrainingStartsNow(bool didRelocateToRRoom)
        {
            if (didRelocateToRRoom) RocketBootRoomSpawnPoint();
        }
        private void RelocatedToTheTimeRoom(bool didRelocateToTRoom)
        {
            if (didRelocateToTRoom) TimeSpawnRoom();
        }
        private void OnDisable()
        {
            CharacterMovement.ResetTCurrentMessageEvent -= RocketTrainingStartsNow;
            CharacterMovement.ResetCCurrentMessageEvent -= RelocatedToTheTimeRoom;
        }
    }
}
