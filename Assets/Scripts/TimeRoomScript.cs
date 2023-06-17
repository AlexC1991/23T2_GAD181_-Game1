using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TimeRoomScript : MonoBehaviour
    {
        [Header("Check Point Room Script"), SerializeField] 
        private CheckPointRoom checkRoom;
        [SerializeField] private Text bottomText; // Text that sits at the bottom of the UI Screen that is located on the trainer.
        [SerializeField] private Text topText; // Text that sits at the top of the UI Screen that is located on the trainer.
        private bool _nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private readonly int _maxMessages = 11; // Sets a max message limit for the clamp used underneath.
        [HideInInspector]
        public int currentTMessages; // Current message count for what message is at what and keeps the dialog going. It's used in the Character Movement script as well to allow the player to do stuff at the end.
<<<<<<< Updated upstream


        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            if (other.CompareTag("Player")) // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            {
                nextMessagePlease = true; // Player collides with it making the bool true.
            }
            else
            {
                nextMessagePlease = false; // Player dose not collide with it making the bool false.
            }
        }
        private void Update()
        {
=======
        private bool _yesHeIs;
        public static event Action<int> letTheCharacterMoveInTheTimeRoom ;
        
        private void OnEnable()
        {
            SpawnLocations.timerRoomEvent += IsHeInsideOfTRoom;
            CharacterMovement.ResetTCurrentMessageEvent += ResetTMessages;
        }
        private void Start()
        {
            currentTMessages = 0;
        }
        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            _nextMessagePlease = other.CompareTag("Player"); // Player collides with it making the bool true.
            // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            // Player dose not collide with it making the bool false.
        }
        private void IsHeInsideOfTRoom(bool insideOfTimerRoom)
        {
            if (insideOfTimerRoom) _yesHeIs = true;
        }
        private void Update()
        {
            Debug.Log("Current Timer Room Message " + currentTMessages);
            if (_nextMessagePlease && _yesHeIs) InsideOfThisRoom();
            letTheCharacterMoveInTheTimeRoom?.Invoke(currentTMessages);
        }
        
        
        private void InsideOfThisRoom()
        {
            checkRoom._currentCMessages = 0;
>>>>>>> Stashed changes

            if (_nextMessagePlease && Input.GetKeyDown(KeyCode.E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                currentTMessages = Mathf.Clamp(currentTMessages +1, 0, _maxMessages); // Keeps the current T messages from going further then the max messages and only increment by +1 with the min value to start is 0.

            }

            if (currentTMessages == 1) // If the current T messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Hello! This is");
                bottomText.text = ("The Time Room.");
            }

            if (currentTMessages == 2) // If the current T messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Here We Have");
                bottomText.text = ("A Clock..");
            }

            if (currentTMessages == 3) // If the current T messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("This Keeps Your");
                bottomText.text = ("Time Always");
            }

            if (currentTMessages == 4) // If the current T messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("At 10 Seconds.");
                bottomText.text = ("This Keeps You");
            }
            
            if (currentTMessages == 5) // If the current T messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Moving Through");
                bottomText.text = ("The Maze.");
            }
            
            if (currentTMessages == 6) // If the current T messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("If You Collect");
                bottomText.text = ("This Clock In");
            }
            
            if (currentTMessages == 7) // If the current T messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("The Maze, It");
                bottomText.text = ("Will Go Away.");
            }
            
            if (currentTMessages == 8) // If the current T messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("So Be Strategic");
                bottomText.text = ("Around the Maze");
            }
            
            if (currentTMessages == 9) // If the current T messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("And Don't Get");
                bottomText.text = ("Lost!. See");
            }
            
            if (currentTMessages == 10) // If the current T messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("That Clock There?");
                bottomText.text = ("Go & Collect It");
            }
            
            if (currentTMessages == 11) // If the current T messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("And See What");
                bottomText.text = ("Happens & Be Ready!");
            }

        }
<<<<<<< Updated upstream
=======

        private void ResetTMessages(bool didRelocateToRRoom)
        {
            if (didRelocateToRRoom) currentTMessages = 0;
        }
        private void OnDisable()
        {
            SpawnLocations.timerRoomEvent -= IsHeInsideOfTRoom;
            CharacterMovement.ResetTCurrentMessageEvent -= ResetTMessages;
        }
>>>>>>> Stashed changes
    }      
}

