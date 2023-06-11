using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class CheckPointRoom : MonoBehaviour
    {
        [SerializeField] private Text bottomText; // Text that sits at the bottom of the UI Screen that is located on the trainer.
        [SerializeField] private Text topText; // Text that sits at the top of the UI Screen that is located on the trainer.
        private bool nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private int maxMessages = 13; // Sets a max message limit for the clamp used underneath.
        private int currentCMessages; // Current message count for what message is at what and keeps the dialog going. It's used in the Character Movement script as well to allow the player to do stuff at the end.
        private bool yesHeIs;
        public static event Action<int> _LetTheCharacterMoveInCheckPointRoom; 

        private void Start()
        {
            currentCMessages = 0;
        }
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

        private void OnEnable()
        {
            SpawnLocations._CheckPointRoomEvent += IsHeInsideOfCRoom;
            CharacterMovement._ResetCCurrentMessageEvent += ResetCheckPointMessages;
        }

        private void OnDisable()
        {
            SpawnLocations._CheckPointRoomEvent -= IsHeInsideOfCRoom;
            CharacterMovement._ResetCCurrentMessageEvent -= ResetCheckPointMessages;
        }

        private void Update()
        {
            if (nextMessagePlease && yesHeIs) InsideOfThisRoom();
            _LetTheCharacterMoveInCheckPointRoom?.Invoke(currentCMessages);
        }

        private void IsHeInsideOfCRoom(bool insideOfCheckPointRoom)
        {
            if (insideOfCheckPointRoom) yesHeIs = true;
        }
            
        private void InsideOfThisRoom()
        {

            if (nextMessagePlease && Input.GetKeyDown(KeyCode.E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                currentCMessages = Mathf.Clamp(currentCMessages +1, 0, maxMessages); // Keeps the current C messages from going further then the max messages and only increment by +1 with the min value to start is 0.

            }

            if (currentCMessages == 1) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Welcome Sir!").ToString();
                bottomText.text = ("Here We Have").ToString();
            }

            if (currentCMessages == 2) // If the current C messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("A CheckPoint!").ToString();
                bottomText.text = ("It's So Amazing!.").ToString();
            }

            if (currentCMessages == 3) // If the current C messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("... Well You").ToString();
                bottomText.text = ("Must Be Wondering").ToString();
            }

            if (currentCMessages == 4) // If the current C messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Why It's So").ToString();
                bottomText.text = ("Amazing? Well").ToString();
            }
            
            if (currentCMessages == 5) // If the current C messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("We Have That").ToString();
                bottomText.text = ("Clock Up Top").ToString();
            }
            
            if (currentCMessages == 6) // If the current C messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("If It Counts").ToString();
                bottomText.text = ("Down To 0").ToString();
            }
            
            if (currentCMessages == 7) // If the current C messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Then.. Your").ToString();
                bottomText.text = ("Screwed. But").ToString();
            }
            
            if (currentCMessages == 8) // If the current C messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("With This Thing").ToString();
                bottomText.text = ("We Will Always").ToString();
            }
            
            if (currentCMessages == 9) // If the current C messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Be Able To").ToString();
                bottomText.text = ("Well Not Be").ToString();
            }
            
            if (currentCMessages == 10) // If the current C messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Screwed.. Haha").ToString();
                bottomText.text = ("So Make Sure").ToString();
            }
            
            if (currentCMessages == 11) // If the current C messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("You Always Go").ToString();
                bottomText.text = ("Through One In").ToString();
            }
            
            if (currentCMessages == 12) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("The Maze &").ToString();
                bottomText.text = ("Try Not To").ToString();
            }
            
            if (currentCMessages == 13) // If the current C messages is equal to 13 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("To Get Lost!.").ToString();
                bottomText.text = ("Try It Out.").ToString();
            }

        }

        private void ResetCheckPointMessages(bool didRelocateToTRoom)
        {
            if (didRelocateToTRoom) currentCMessages = 0;
        }
    }      
}