using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class CheckPointRoom : MonoBehaviour
    {
        [SerializeField] private Text bottomText; // Text that sits at the bottom of the UI Screen that is located on the trainer.
        [SerializeField] private Text topText; // Text that sits at the top of the UI Screen that is located on the trainer.
        [HideInInspector]
        public int currentCMessages; // Current message count for what message is at what and keeps the dialog going. It's used in the Character Movement script as well to allow the player to do stuff at the end.
        public static event Action<int> LetTheCharacterMoveInCheckPointRoom; 
        private bool _nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private readonly int _maxMessages = 13; // Sets a max message limit for the clamp used underneath.
        [HideInInspector] public int _currentCMessages; // Current message count for what message is at what and keeps the dialog going. It's used in the Character Movement script as well to allow the player to do stuff at the end.
        private bool _yesHeIs;
        
        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            _nextMessagePlease = other.CompareTag("Player"); // Player collides with it making the bool true.
            // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            // Player dose not collide with it making the bool false.
        }
        private void OnEnable()
        {
            SpawnLocations.CheckPointRoomEvent += IsHeInsideOfCRoom;
            CharacterMovement.ResetCCurrentMessageEvent += ResetCheckPointMessages;
        }
        private void Start()
        {
            _currentCMessages = 0;
        }
        private void Update()
        {
            if (_nextMessagePlease && _yesHeIs) InsideOfThisRoom();
            LetTheCharacterMoveInCheckPointRoom?.Invoke(_currentCMessages);
        }
        private void IsHeInsideOfCRoom(bool insideOfCheckPointRoom)
        {
            if (insideOfCheckPointRoom) _yesHeIs = true;
        }
        private void InsideOfThisRoom()
        {

            if (_nextMessagePlease && Input.GetKeyDown(KeyCode.E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                _currentCMessages = Mathf.Clamp(_currentCMessages +1, 0, _maxMessages); // Keeps the current C messages from going further then the max messages and only increment by +1 with the min value to start is 0.

            }

            if (_currentCMessages == 1) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Welcome Sir!");
                bottomText.text = ("Here We Have");
            }

            if (_currentCMessages == 2) // If the current C messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("A CheckPoint!");
                bottomText.text = ("It's So Amazing!.");
            }

            if (_currentCMessages == 3) // If the current C messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("... Well You");
                bottomText.text = ("Must Be Wondering");
            }

            if (_currentCMessages == 4) // If the current C messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Why It's So");
                bottomText.text = ("Amazing? Well");
            }
            
            if (_currentCMessages == 5) // If the current C messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("We Have That");
                bottomText.text = ("Clock Up Top");
            }
            
            if (_currentCMessages == 6) // If the current C messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("If It Counts");
                bottomText.text = ("Down To 0");
            }
            
            if (_currentCMessages == 7) // If the current C messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Then.. Your");
                bottomText.text = ("Screwed. But");
            }
            
            if (_currentCMessages == 8) // If the current C messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("With This Thing");
                bottomText.text = ("We Will Always");
            }
            
            if (_currentCMessages == 9) // If the current C messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Be Able To");
                bottomText.text = ("Well Not Be");
            }
            
            if (_currentCMessages == 10) // If the current C messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Screwed.. Haha");
                bottomText.text = ("So Make Sure");
            }
            
            if (_currentCMessages == 11) // If the current C messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("You Always Go");
                bottomText.text = ("Through One In");
            }
            
            if (_currentCMessages == 12) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("The Maze &");
                bottomText.text = ("Try Not To");
            }
            
            if (_currentCMessages == 13) // If the current C messages is equal to 13 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("To Get Lost!.");
                bottomText.text = ("Try It Out.");
            }
        }
        private void ResetCheckPointMessages(bool didRelocateToTRoom)
        {
            if (didRelocateToTRoom) _currentCMessages = 0;
        }
        private void OnDisable()
        {
            SpawnLocations.CheckPointRoomEvent -= IsHeInsideOfCRoom;
            CharacterMovement.ResetCCurrentMessageEvent -= ResetCheckPointMessages;
        }
    }      
}