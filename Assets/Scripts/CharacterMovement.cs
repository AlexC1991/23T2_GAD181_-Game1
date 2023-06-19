using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private TimerScript timeS;
        [SerializeField] private SpawnLocations spawnLScript;
        [SerializeField] private NewLocationSpawn newSpawn;
        [SerializeField] private TriesMechanicScript tMechanic;
        [SerializeField] private SoundPlusMusicManager mainSoundPlayer;

        [Header("Rocket Room Script"), SerializeField]
        private RocketRoomTrainer rRoomScript;

        [Header("StartUI")] 
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject retryMechanicScreen;

        [Header("Character Run Speed")]
        [HideInInspector] public float runSpeed = 8f; // This is the run speed of the character this script is attached to.
        [SerializeField] private float maxRunSpeed = 12; // This is the max run speed which is used for the rocket boots/shoes in game.
        private float _originalRunSpeed; // This keeps the original run speed to return to when needed.
        private bool _timeToMoveFaster; // This Bool gives a yes/no for when you activate the rocket boots/shoes.

        [Header("Character Jump Height")] 
        [SerializeField] private float jumpHeight = 3; // This is the jump height the character can jump up to.
        private bool _playerIsJumping;

        [Header("SpawnLocations")]
        [HideInInspector] public bool insideOfRocketRoom; // This Bool gives a yes or no when the character is located in the rocket room or not.
        [HideInInspector] public bool insideOfCheckPointRoom; // This Bool gives a yes or no when the character is located in the checkpoint room or not.
        
        [Header("Character info")]
        [SerializeField] private CharacterController controller; // This gets the character controller to be used inside of this script to move the character.
        [SerializeField] private float mouseSensitivity = 1; // Amplifies the mouse movement to be more sensitive when used to rotate the character.
        [SerializeField] private float characterGravity = 20; // Sets the gravity for the character.

        [Header("Various Variables Used")]
        private float _mouseXposition; // Gets the position of the mouse using the float values of its X position in the world.
        private Vector3 _newPosition; // This sets a new position when going through the checkpoint system.
        private Vector3 _moveDirection; // This gathers the move direction for when the character rotates and adjusts the position the character faces.
        private float _moveHorizontal; // Speed of which the character moves in the Horizontal axis.
        private float _moveVertical; // Speed of which the character moves in the Vertical axis.
        private bool _alwaysCheckSpeed; // Bool say yes or no for when to change the speed with the characters movement.
        private bool _relocatedToSpawnPoint; // This indicates when you have to go back to the checkpoint when time runs out.

        private bool _timeToRelocateToTRoom;
        private bool _canUseInGameMenu;
        private bool _didRelocateToTRoom;
        private bool _timeToRelocateToRRoom;
        private bool _didRelocateToRRoom;
        [HideInInspector] public bool resetRocketRoomCounter;
        [HideInInspector] public bool resetCheckPointRoomCounter;
        private bool _didRelocateToMazeRoom;

        [HideInInspector] public bool tryAgain;

        public static event Action<bool> ResetTCurrentMessageEvent;
        public static event Action<bool> ResetCCurrentMessageEvent;
        public static event Action<bool> ResetRCurrentMessageEvent;
        public static event Action<bool> ActivateRocketBootStateEvent;
        public static event Action<bool> PlayerIsCurrentlyJumpingEvent;
        public static event Action<bool> UsingInGameMenuEvent;

        private void OnEnable()
        {
            TimerScript.RespawnToCheckPointEvent += TimeReachZero;
            TimerScript.RelocateToRocketRoomEvent += RelocateToRocketBootTrainingRoom;
            RocketSpawnController.ResetTheSpeedOfCharacterEvent += RocketSpeedReset;
            CheckPoint.SaveHereInstead += SaveNewCheckPointLocation;
        }
        private void Start()
        {
            retryMechanicScreen.SetActive(false);
            mainSoundPlayer._playMainMusic = false;
            StartGameMenuBeforeWeStart();
            Time.timeScale = 0;
            _canUseInGameMenu = false;
            _originalRunSpeed = runSpeed;
            runSpeed = 0;
            _timeToMoveFaster = false;
        }
        public void StartTrainingRoom()
        {
            startScreen.SetActive(false);
            RelocateToCheckPointRoom();
        }
        public void StartTheGame()
        {
            startScreen.SetActive(false);
            StartInMazeRoom();
        }
        private void FixedUpdate()
        {
            if (_timeToRelocateToTRoom)
            {
                newSpawn.TimeTrainRoomSpawn();
                runSpeed = 0;
                spawnLScript.TimeSpawnRoom();
                _didRelocateToTRoom = true;
                _timeToRelocateToTRoom = false;
                resetRocketRoomCounter = true;
                resetCheckPointRoomCounter = true;
            }

            if (_timeToRelocateToRRoom)
            {
                newSpawn.RocketSpawnRoom();
                runSpeed = 0;
                spawnLScript.RocketBootRoomSpawnPoint();
                _didRelocateToRRoom = true;
                _didRelocateToTRoom = false;
                resetRocketRoomCounter = false;
                resetCheckPointRoomCounter = true;
                _timeToRelocateToRRoom = false;
            }
        }
        private void RelocateToCheckPointRoom()
        {
            Time.timeScale = 1;
            newSpawn.CheckPointTrainRoomSpawn();
            spawnLScript.CheckPointSpawnRoom();
            resetRocketRoomCounter = true;
            resetCheckPointRoomCounter = false;
            runSpeed = 0;
        }
        private void StartInMazeRoom()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            newSpawn.StartSpawn();
            spawnLScript.MainStartRoomSpawn();
            retryMechanicScreen.SetActive(true);
            mainSoundPlayer._playMainMusic = true;
            rRoomScript.currentRMessages = 0;
            _didRelocateToMazeRoom = true;
            _didRelocateToRRoom = false;
            resetRocketRoomCounter = true;
            resetCheckPointRoomCounter = true;
            _canUseInGameMenu = true;
            _newPosition = controller.transform.position;
        }
        private void RelocateToTimeTrainingRoom(bool playerTestingIt)
        {
            if (playerTestingIt)
            {
                _timeToRelocateToTRoom = true;
                _didRelocateToTRoom = true;
            }
        }
        private void RelocateToRocketBootTrainingRoom(bool goToRocketTraining)
        {
            if (goToRocketTraining)
            {
                timeS.timeIsUp = false;
                timeS.currentTime = 10;
                timeS.goToRocketTraining = false;
                _timeToRelocateToRRoom = true;
            }
        }
        private void StartGameMenuBeforeWeStart()
        {
            startScreen.SetActive(true);
        }
        private void Update()
        {
            _mouseXposition = mouseSensitivity * Input.GetAxis("Mouse X"); // grabs the mouse X axis every frame for the rotation movement.
            _moveHorizontal = Input.GetAxis("Horizontal"); // Gets the horizontal movement of the character.
            _moveVertical = Input.GetAxis("Vertical"); // Gets the vertical movement of the character.
            
            JumpMovement(); // Controls the jump movement of the character.
            ChangeSpeed();
            
            Vector3 movement = new Vector3(-_moveHorizontal, 0f, -_moveVertical); // Allows the character to move forwards and backwards & left & right.
            movement = transform.TransformDirection(movement) * runSpeed; // Gives the character movement speed.
            transform.Rotate(Vector3.up, _mouseXposition * 80 * Time.deltaTime); // Gets the mouse input and uses it to rotate the character.
            controller.Move((movement + _moveDirection) * Time.deltaTime); // Gets all the movement variables and moves the character.
            
            ResetTCurrentMessageEvent?.Invoke(_didRelocateToRRoom);
            ResetCCurrentMessageEvent?.Invoke(_didRelocateToTRoom);
            ResetRCurrentMessageEvent?.Invoke(_didRelocateToMazeRoom);
            ActivateRocketBootStateEvent?.Invoke(_timeToMoveFaster);
            PlayerIsCurrentlyJumpingEvent?.Invoke(_playerIsJumping);
            UsingInGameMenuEvent?.Invoke(_canUseInGameMenu);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RocketBoots")) _timeToMoveFaster = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("RocketBoots")) _timeToMoveFaster = false;
        }

        private void ChangeSpeed() 
        {
            if (_timeToMoveFaster) runSpeed = maxRunSpeed; // This will activate when using the rocket shoes/boots giving max run speed.
        }
        private void TimeReachZero(bool resetLocation)
        {
            if (resetLocation) { // If the current time on the clock is less then 0.2f from the timer script it will make reset location turn true and starting the function below.
                controller.transform.position = _newPosition; // This will transform the position of the player to the new position which is got from the last check point.
                tryAgain = true;
            }
        }
        private void JumpMovement() 
        {
            if (Input.GetKeyDown(KeyCode.Space) && (controller.isGrounded)) // If player hits the space bar and the character is touching the ground it will allow the character to jump.
            {
                _moveDirection.y = Mathf.Sqrt(2f * jumpHeight * characterGravity);
                _playerIsJumping = true;
            }

            if (controller.isGrounded) return;
            _moveDirection.y -= characterGravity * Time.deltaTime;
            _playerIsJumping = false;
        }
        private void ResetSpeed() 
        {
            runSpeed = _originalRunSpeed; // When the ride is over inside of the rocket boots/shoes it will go back to the original run speed.
        }
        private void SaveNewCheckPointLocation(bool newSavePoint)
        {
            if (newSavePoint)
            {
                _newPosition = controller.transform.position;
                tMechanic._currentTries = 0;
            }
        }
        private void CheckPointRoomMovementCheck(int currentCMessages)
        {
            if (currentCMessages == 12) runSpeed = _originalRunSpeed;
        }
        private void TimeRoomMovementCheck(int currentTMessages)
        {
            if (currentTMessages == 11) runSpeed = _originalRunSpeed;
        }
        private void RocketRoomMovementCheck(int currentRMessages)
        {
            if (currentRMessages == 11) runSpeed = _originalRunSpeed;
        }
        private void RocketSpeedReset(bool rocketBootState)
        {
            if (!rocketBootState) ResetSpeed();
        }
        private void OnDisable()
        {
            TimerScript.RespawnToCheckPointEvent -= TimeReachZero;
            TimerScript.RelocateToRocketRoomEvent -= RelocateToRocketBootTrainingRoom;
            RocketSpawnController.ResetTheSpeedOfCharacterEvent -= RocketSpeedReset;
            CheckPoint.SaveHereInstead -= SaveNewCheckPointLocation;
        }
    } 
}

