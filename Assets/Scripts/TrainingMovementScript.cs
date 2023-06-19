using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class TrainingMovementScript : MonoBehaviour
    {
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
        [SerializeField] private float mouseSensitivity = 1; // Amplifies the mouse movement to be more sensitive when used to rotate the character.
        [SerializeField] private float characterGravity = 20; // Sets the gravity for the character.

        [Header("Various Variables Used")]
        private float _mouseXposition; // Gets the position of the mouse using the float values of its X position in the world.
        private Vector3 _newPosition; // This sets a new position when going through the checkpoint system.
        private Vector3 _moveDirection; // This gathers the move direction for when the character rotates and adjusts the position the character faces.
        private float _moveHorizontal; // Speed of which the character moves in the Horizontal axis.
        private float _moveVertical; // Speed of which the character moves in the Vertical axis.

        private void OnEnable()
        {
            TimeRoomScript._TimeRoomEvent += TimerRoomChecker;
            CheckPointRoom._CheckPointRoomEvent += CheckPR;
        }

        private void Start()
        {
            _originalRunSpeed = runSpeed;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            runSpeed = 0;
        }
        
        private void Update()
        {
            _mouseXposition = mouseSensitivity * Input.GetAxis("Mouse X"); // grabs the mouse X axis every frame for the rotation movement.
            _moveHorizontal = Input.GetAxis("Horizontal"); // Gets the horizontal movement of the character.
            _moveVertical = Input.GetAxis("Vertical"); // Gets the vertical movement of the character.
            Vector3 movement = new Vector3(-_moveHorizontal, 0f, -_moveVertical); // Allows the character to move forwards and backwards & left & right.
            movement = transform.TransformDirection(movement) * runSpeed; // Gives the character movement speed.
            transform.Rotate(Vector3.up, _mouseXposition * 80 * Time.deltaTime); // Gets the mouse input and uses it to rotate the character.
            controller.Move((movement + _moveDirection) * Time.deltaTime); // Gets all the movement variables and moves the character.
        }

        private void JumpMovement()
        {
            if (Input.GetKeyDown(KeyCode.Space) &&
                (controller
                    .isGrounded)) // If player hits the space bar and the character is touching the ground it will allow the character to jump.
            {
                _moveDirection.y = Mathf.Sqrt(2f * jumpHeight * characterGravity);
                _playerIsJumping = true;
            }
            if (controller.isGrounded) return;
            _moveDirection.y -= characterGravity * Time.deltaTime;
            _playerIsJumping = false;
        }

        private void TimerRoomChecker(bool finishedMessages)
        {
            if (finishedMessages) TutorialFinished();
        }

        private void CheckPR(bool finishedMessages)
        {
            if (finishedMessages) TutorialFinished();
        }

        private void TutorialFinished()
        {
            runSpeed = _originalRunSpeed;
            JumpMovement(); // Controls the jump movement of the character.
        }

        private void OnDisable()
        {
            TimeRoomScript._TimeRoomEvent -= TimerRoomChecker;
            CheckPointRoom._CheckPointRoomEvent -= CheckPR;
        }
    }
}

