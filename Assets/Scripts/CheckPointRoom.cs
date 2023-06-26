using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class CheckPointRoom : MonoBehaviour
    {
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Text dialogueText; // Text that sits at the top of the UI Screen that is located on the trainer.
        private bool _nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private readonly int _maxMessages = 13; // Sets a max message limit for the clamp used underneath.
        private int _currentCMessages;
        private bool finishedMessages;
        [SerializeField] private GameObject turnOffChat;
        private float _quickTimer = 1f;

        public static event Action<bool> _CheckPointRoomEvent; 

        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            _nextMessagePlease = other.CompareTag("Player"); // Player collides with it making the bool true.
            // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            // Player dose not collide with it making the bool false.
        }
        private void Start()
        {
            turnOffChat.SetActive(true);
            finishedMessages = false;
            _currentCMessages = 0;
            Time.timeScale = 1;
        }
        
        private void Update()
        {
            dialogueText.GetComponent<Text>().color = Color.white;
            _CheckPointRoomEvent?.Invoke(finishedMessages);
            transform.LookAt(2* transform.position - playerPosition.position);

            if (_nextMessagePlease && Input.GetKeyDown(KeyCode.E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                _currentCMessages = Mathf.Clamp(_currentCMessages +1, 0, _maxMessages); // Keeps the current C messages from going further then the max messages and only increment by +1 with the min value to start is 0.

            }
            
            if (_currentCMessages == 0) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = (" ' Caution ' " + " You can not move until i say so.... Press E");
            }

            if (_currentCMessages == 1) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Welcome Sir! here we have a check point!.. Press E");
            }
            if (_currentCMessages == 2) // If the current C messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("It's so amazing!. Well you must be wondering .. Press E");
            }

            if (_currentCMessages == 3) // If the current C messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Why it's so amazing? well we have a clock in the top.. Press E");
            }

            if (_currentCMessages == 4) // If the current C messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Right hand corner of your screen and it counts down from 10 to 0... Press E");
            }
            
            if (_currentCMessages == 5) // If the current C messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("If it counts down to 0 then your going to have a bad time... Press E");
            }
            
            if (_currentCMessages == 6) // If the current C messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("However with this thing we call a check point, you will always have... Press E");
            }
            
            if (_currentCMessages == 7) // If the current C messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("A new saved location to spawn into which will give you that... Press E");
            }
            
            if (_currentCMessages == 8) // If the current C messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Edge to inch closer to the trophy in the maze!. However if you... Press E");
            }
            
            if (_currentCMessages == 9) // If the current C messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Get stuck you will have 5 retries available. However if you... Press E");
            }
            
            if (_currentCMessages == 10) // If the current C messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Get stuck stuck then after 5 tries you will have the option to reset... Press E");
            }
            
            if (_currentCMessages == 11) // If the current C messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("The game again and start all over again. You have now completed the.. Press E");
            }
            
            if (_currentCMessages == 12) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Training you are now free to move around.Go through to another tutorial!.. Press E");
                
            }
            if (_currentCMessages == 13) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Go through one of the check points to proceed.. You can walk now.");
                finishedMessages = true;
                _quickTimer -= 0.3f * Time.deltaTime;
                if (_quickTimer < 0.2f)
                {
                    turnOffChat.SetActive(false);
                    _quickTimer = 0;
                }
                
            }
        }
        
    }      
}