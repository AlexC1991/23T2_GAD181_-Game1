using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("SpawnLocations")]
        [SerializeField] private Transform startPosition; // This houses the start location for the character when it starts the main game.
        [SerializeField] private Transform timeSpawnPoint; // This houses the position for the character when it starts in the time tutorial room.
        [SerializeField] private Transform rocketSpawnPoint; // This houses the position for the character when it starts in the rocket boot/shoe tutorial room.
        [SerializeField] private Transform checkSpawnPoint; // This houses the position for the character when it starts in the check point tutorial room.
        [SerializeField] private Transform resetSpawnPosition; // This is the position the character will go when the game resets or the character loses.
        private Vector3 timeSpawnTrainingRoom;
        private Vector3 rocketSpawnTrainingRoom;
        private Vector3 checkPointSpawnTrainingRoom;
        private Vector3 startSpawnPosition;
        private Vector3 resetSpawnPositionPoint;
        
        [Header("Character Run Speed")]
        [SerializeField] private float runSpeed = 8f; // This is the run speed of the character this script is attached to.
        [SerializeField] private float maxRunSpeed = 12; // This is the max run speed which is used for the rocket boots/shoes in game.
        private float originalRunSpeed; // This keeps the original run speed to return to when needed.
        private bool timeToMoveFaster; // This Bool gives a yes/no for when you activate the rocket boots/shoes.

        [Header("Character Jump Height")] 
        [SerializeField] private float jumpHeight = 3; // This is the jump height the character can jump up to.
        private bool playerIsJumping;

        [Header("Character info")]
        [SerializeField] private CharacterController controller; // This gets the character controller to be used inside of this script to move the character.
        [SerializeField] private float mouseSensitivity = 1; // Amplifies the mouse movement to be more sensitive when used to rotate the character.
        [SerializeField] private float characterGravity = 20; // Sets the gravity for the character.

        [Header("Various Variables Used")]
        private float mouseXposition; // Gets the position of the mouse using the float values of its X position in the world.
        private Vector3 newPosition; // This sets a new position when going through the checkpoint system.
        private Vector3 moveDirection; // This gathers the move direction for when the character rotates and adjusts the position the character faces.
        private float moveHorizontal; // Speed of which the character moves in the Horizontal axis.
        private float moveVertical; // Speed of which the character moves in the Vertical axis.
        private bool relocatedToSpawnPoint; // This indicates when you have to go back to the checkpoint when time runs out.
        private bool didRelocateToTRoom;
        private bool didRelocateToRRoom;
        private bool didRelocateToMazeRoom;
        private bool canUseInGameMenu;
        private bool timeToRelocateToTRoom;
        private bool timeToRelocateToRRoom;
        private bool timeToRelocateToMazeRoom;

        public static event Action<bool> _ResetTCurrentMessageEvent; 
        public static event Action<bool> _ResetCCurrentMessageEvent; 
        public static event Action<bool> _ResetRCurrentMessageEvent;
        public static event Action<bool> _ActivateRocketBootStateEvent;
        public static event Action<bool> _PlayerIsCurrentlyJumpingEvent;
        public static event Action<bool> _UsingInGameMenuEvent;

        private void Awake()
        {
             startSpawnPosition = startPosition.position;
             timeSpawnTrainingRoom = timeSpawnPoint.position;
             rocketSpawnTrainingRoom = rocketSpawnPoint.position;
             checkPointSpawnTrainingRoom = checkSpawnPoint.position; 
             resetSpawnPositionPoint = resetSpawnPosition.position;
        }

        private void Start()
        {
            RelocateToCheckPointRoom();
            canUseInGameMenu = false;
            originalRunSpeed = runSpeed;
            runSpeed = 0;
        }
        
        private void OnEnable()
        {
            CheckPointRoom._LetTheCharacterMoveInCheckPointRoom += CheckPointRoomMovementCheck;
            TimeRoomScript._LetTheCharacterMoveInTheTimeRoom += TimeRoomMovementCheck;
            RocketRoomTrainer._LetTheCharacterMoveInTheRocketRoom += RocketRoomMovementCheck;
            TimerScript._RespawnToCheckPointEvent += TimeReachZero;
            RocketRoomTrainer._CharactersBootsNeedSpawning += RelocateToMazeRoom;
            TimerScript.RelocateToRocketRoomEvent += RelocateToRocketBootTrainingRoom;
            TestingCheckP._RelocateToTimeRoomEvent += RelocateToTimeTrainingRoom;
        }

        private void OnDisable()
        {
            CheckPointRoom._LetTheCharacterMoveInCheckPointRoom -= CheckPointRoomMovementCheck;
            TimeRoomScript._LetTheCharacterMoveInTheTimeRoom -= TimeRoomMovementCheck;
            RocketRoomTrainer._LetTheCharacterMoveInTheRocketRoom -= RocketRoomMovementCheck;
            TimerScript._RespawnToCheckPointEvent -= TimeReachZero;
            RocketRoomTrainer._CharactersBootsNeedSpawning -= RelocateToMazeRoom;
            TimerScript.RelocateToRocketRoomEvent -= RelocateToRocketBootTrainingRoom;
            TestingCheckP._RelocateToTimeRoomEvent -= RelocateToTimeTrainingRoom;
        }

        private void FixedUpdate()
        {
            if (timeToRelocateToTRoom)
            {
                Debug.Log("Worked");
                controller.transform.position = timeSpawnTrainingRoom;
                runSpeed = 0;
                didRelocateToTRoom = true;
                timeToRelocateToTRoom = false;
            }

            if (timeToRelocateToRRoom)
            {
                controller.transform.position = rocketSpawnTrainingRoom;
                runSpeed = 0;
                didRelocateToRRoom = true;
                didRelocateToTRoom = false;
                timeToRelocateToRRoom = false;
            }

            if (timeToRelocateToMazeRoom)
            {
                controller.transform.position = startSpawnPosition;
                didRelocateToMazeRoom = true;
                didRelocateToRRoom = false;
                timeToRelocateToMazeRoom = false;
            }
        }

        private void Update()
        {
            GetInputS();
            JumpMovement(); // Controls the jump movement of the character.
            CharacterMovementStart(); // Controls the character movement in game.
            ChangeSpeed();
            
            _ResetTCurrentMessageEvent?.Invoke(didRelocateToRRoom);
            _ResetCCurrentMessageEvent?.Invoke(didRelocateToTRoom);
            _ResetRCurrentMessageEvent?.Invoke(didRelocateToMazeRoom);
            _ActivateRocketBootStateEvent?.Invoke(timeToMoveFaster);
            _PlayerIsCurrentlyJumpingEvent?.Invoke(playerIsJumping);
            _UsingInGameMenuEvent?.Invoke(canUseInGameMenu);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RocketBoots")) timeToMoveFaster = true;
            else timeToMoveFaster = false;
        }
        private void TimeReachZero(bool resetLocation)
        {
            if (resetLocation) { // If the current time on the clock is less then 0.2f from the timer script it will make reset location turn true and starting the function below.
                controller.transform.position = newPosition; // This will transform the position of the player to the new position which is got from the last check point.
                relocatedToSpawnPoint = true; // This is true when the player moves back to the check point due to the time in game hitting 0.
            }
            else relocatedToSpawnPoint = false; // If the players time does not hit 0 the character does not go back to spawn being this bool is saying its false due to not relocating.
        }
        private void GetInputS()
        {
            mouseXposition = mouseSensitivity * Input.GetAxis("Mouse X"); // grabs the mouse X axis every frame for the rotation movement.
            moveHorizontal = Input.GetAxis("Horizontal"); // Gets the horizontal movement of the character.
            moveVertical = Input.GetAxis("Vertical"); // Gets the vertical movement of the character.
        }
        private void CharacterMovementStart()
        {
            Vector3 movement = new Vector3(-moveHorizontal, 0f, -moveVertical); // Allows the character to move forwards and backwards & left & right.
            movement = transform.TransformDirection(movement) * runSpeed; // Gives the character movement speed.
            transform.Rotate(Vector3.up, mouseXposition * 80 * Time.deltaTime); // Gets the mouse input and uses it to rotate the character.
            controller.Move((movement + moveDirection) * Time.deltaTime); // Gets all the movement variables and moves the character.
        }
        private void JumpMovement() 
        {
            if (Input.GetKeyDown(KeyCode.Space) && (controller.isGrounded)) // If player hits the space bar and the character is touching the ground it will allow the character to jump.
            {
                moveDirection.y = Mathf.Sqrt(2f * jumpHeight * characterGravity);
                playerIsJumping = true;
            }

            if (!controller.isGrounded)
            {
                moveDirection.y -= characterGravity * Time.deltaTime;
                playerIsJumping = false;
            }
        }
        private void ChangeSpeed() 
        {
            if (timeToMoveFaster) runSpeed = maxRunSpeed; // This will activate when using the rocket shoes/boots giving max run speed.
        }
        private void ResetSpeed() 
        {
            runSpeed = originalRunSpeed; // When the ride is over inside of the rocket boots/shoes it will go back to the original run speed.
        }
        private void CheckPointRoomMovementCheck(int currentCMessages)
        {
            if (currentCMessages == 12) runSpeed = originalRunSpeed;
        }
        private void TimeRoomMovementCheck(int currentTMessages)
        {
            if (currentTMessages == 11) runSpeed = originalRunSpeed;
        }
        private void RocketRoomMovementCheck(int currentRMessages)
        {
            if (currentRMessages == 11) runSpeed = originalRunSpeed;
        }
        private void RelocateToMazeRoom(bool canRelocateToMaze)
        {
            if (canRelocateToMaze)
            {
                timeToRelocateToMazeRoom = true;
                didRelocateToMazeRoom = true;
                didRelocateToRRoom = false;
                canUseInGameMenu = true;
            }
        }
        private void RelocateToCheckPointRoom()
        {
            controller.transform.position = checkPointSpawnTrainingRoom;
        }
        private void RelocateToTimeTrainingRoom(bool playerTestingIt)
        {
            if (playerTestingIt)
            {
                timeToRelocateToTRoom = true;
                didRelocateToTRoom = true;
            }
        }
        private void RelocateToRocketBootTrainingRoom(bool goToRocketTraining)
        {
            if (goToRocketTraining)
            {
                timeToRelocateToRRoom = true;
            }
        }
        
    } 
}

