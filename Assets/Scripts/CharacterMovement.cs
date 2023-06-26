using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private TimerScript timeS;
        [SerializeField] private NewLocationSpawn newSpawn;
        [SerializeField] private TriesMechanicScript tMechanic;
        [SerializeField] private SoundPlusMusicManager mainSoundPlayer;
        [SerializeField] private RocketBootController rocketCon;

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

        [Header("Character info")]
        [SerializeField] private CharacterController controller; // This gets the character controller to be used inside of this script to move the character.
        [HideInInspector] public float mouseSensitivity = 1; // Amplifies the mouse movement to be more sensitive when used to rotate the character.
        [SerializeField] private float characterGravity = 20; // Sets the gravity for the character.
        [SerializeField] private Text rocketBootTotalCapacity;
        
        [Header("Various Variables Used")]
        private float _mouseXposition; // Gets the position of the mouse using the float values of its X position in the world.
        private Vector3 _newPosition; // This sets a new position when going through the checkpoint system.
        private Vector3 _moveDirection; // This gathers the move direction for when the character rotates and adjusts the position the character faces.
        private float _moveHorizontal; // Speed of which the character moves in the Horizontal axis.
        private float _moveVertical; // Speed of which the character moves in the Vertical axis.
        private bool _alwaysCheckSpeed; // Bool say yes or no for when to change the speed with the characters movement.
        private bool _relocatedToSpawnPoint; // This indicates when you have to go back to the checkpoint when time runs out.
        [SerializeField] private GameObject rocketBootScreenOnOrOff;
        private bool _timeToRelocateToTRoom;
        private bool _canUseInGameMenu;
        private bool _didRelocateToTRoom;
        private bool _didRelocateToRRoom;
        [HideInInspector] public bool didRelocateToMazeRoom;
        [HideInInspector] public bool tryAgain;
        private bool _addMoreBoots;
        private int _rocketBootCapacity;
        private bool _activateSpeedBoots;
        private bool _turnOffRocketBoots;
        private bool _activateOnce;

        [SerializeField] private GameObject textTurnOff;

        public static event Action<bool> ResetTCurrentMessageEvent;
        public static event Action<bool> ResetRCurrentMessageEvent;
        public static event Action<bool> PlayerIsCurrentlyJumpingEvent;
        public static event Action<bool> UsingInGameMenuEvent;

        private void OnEnable()
        {
            TimerScript.RespawnToCheckPointEvent += TimeReachZero;
            TimerScript.RelocateToRocketRoomEvent += RelocateToRocketBootTrainingRoom;
            CheckPoint.SaveHereInstead += SaveNewCheckPointLocation;
        }
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            textTurnOff.SetActive(false);
            rocketBootScreenOnOrOff.SetActive(false);
            _rocketBootCapacity = 0;
            _turnOffRocketBoots = false;
            retryMechanicScreen.SetActive(false);
            mainSoundPlayer._playMainMusic = false;
            StartGameMenuBeforeWeStart();
            Time.timeScale = 0;
            _canUseInGameMenu = false;
            _originalRunSpeed = runSpeed;
            runSpeed = 0;
            _timeToMoveFaster = false;
        }
        public void StartTheGame()
        {
            startScreen.SetActive(false);
            StartInMazeRoom();
        }
        private void StartInMazeRoom()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            newSpawn.StartSpawn();
            retryMechanicScreen.SetActive(true);
            rocketBootScreenOnOrOff.SetActive(true);
            mainSoundPlayer._playMainMusic = true;
            didRelocateToMazeRoom = true;
            _didRelocateToRRoom = false;
            _canUseInGameMenu = true;
            _newPosition = controller.transform.position;
            textTurnOff.SetActive(true);
        }
        private void RelocateToRocketBootTrainingRoom(bool goToRocketTraining)
        {
            if (goToRocketTraining)
            {
                timeS.timeIsUp = false;
                timeS.currentTime = 10;
                timeS.goToRocketTraining = false;
            }
        }
        private void StartGameMenuBeforeWeStart()
        {
            startScreen.SetActive(true);
        }
        private void Update()
        {
            rocketBootTotalCapacity.text = (_rocketBootCapacity).ToString();
            _mouseXposition = mouseSensitivity * Input.GetAxis("Mouse X"); // grabs the mouse X axis every frame for the rotation movement.
            _moveHorizontal = Input.GetAxis("Horizontal"); // Gets the horizontal movement of the character.
            _moveVertical = Input.GetAxis("Vertical"); // Gets the vertical movement of the character.
            
            JumpMovement(); // Controls the jump movement of the character.
            ChangeSpeed();
            RocketSpeedReset();
            
            Vector3 movement = new Vector3(-_moveHorizontal, 0f, -_moveVertical); // Allows the character to move forwards and backwards & left & right.
            movement = transform.TransformDirection(movement) * runSpeed; // Gives the character movement speed.
            transform.Rotate(Vector3.up, _mouseXposition * 80 * Time.deltaTime); // Gets the mouse input and uses it to rotate the character.
            controller.Move((movement + _moveDirection) * Time.deltaTime); // Gets all the movement variables and moves the character.
            
            ResetTCurrentMessageEvent?.Invoke(_didRelocateToRRoom);
            ResetRCurrentMessageEvent?.Invoke(didRelocateToMazeRoom);
            PlayerIsCurrentlyJumpingEvent?.Invoke(_playerIsJumping);
            UsingInGameMenuEvent?.Invoke(_canUseInGameMenu);

            if (Input.GetKeyDown(KeyCode.LeftShift) && (_rocketBootCapacity > 0) || Input.GetKeyDown(KeyCode.RightShift) && (_rocketBootCapacity > 0))
            {
                rocketCon.rocketBootState = true;
                _activateOnce = true;
            }
            if (_turnOffRocketBoots && rocketCon.rocketBootState && _activateOnce)
            {
                _rocketBootCapacity -= 1;
                _activateOnce = false;
            }
            
            if (_addMoreBoots)
            {
                _rocketBootCapacity += 1;
                _addMoreBoots = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RocketBoots"))
            {
                _addMoreBoots = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("RocketBoots")) _addMoreBoots = false;
        }

        private void ChangeSpeed() 
        {
            if (rocketCon.rocketBootState) runSpeed = maxRunSpeed; // This will activate when using the rocket shoes/boots giving max run speed.
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
        private void RocketSpeedReset()
        {
            if (!rocketCon.rocketBootState)
            {
                ResetSpeed();

                if (_rocketBootCapacity > 0)
                {
                    _turnOffRocketBoots = true;
                }
            }
        }
        private void OnDisable()
        {
            TimerScript.RespawnToCheckPointEvent -= TimeReachZero;
            TimerScript.RelocateToRocketRoomEvent -= RelocateToRocketBootTrainingRoom;
            CheckPoint.SaveHereInstead -= SaveNewCheckPointLocation;
        }
    } 
}

