using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TimeRoomScript : MonoBehaviour
    {
        [SerializeField] private ClockTester clockT;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Text _quickTimerText;
        [SerializeField] private Text dialogueText; // Text that sits at the top of the UI Screen that is located on the trainer.

        private bool _nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private readonly int _maxMessages = 13; // Sets a max message limit for the clamp used underneath.
        private int _currentTMessages;
        private bool finishedMessages;
        [SerializeField] private GameObject turnOffChat;
        [SerializeField] float _quickTimer = 10;
        public static event Action<bool> _TimeRoomEvent; 

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
            _currentTMessages = 0;
        }

        private void Update()
        {
            dialogueText.GetComponent<Text>().color = Color.cyan;
            _TimeRoomEvent?.Invoke(finishedMessages);
            _quickTimerText.text = (_quickTimer).ToString("F0");
            transform.LookAt(2 * transform.position - playerPosition.position);

            if (_nextMessagePlease &&
                Input.GetKeyDown(KeyCode
                    .E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                _currentTMessages =
                    Mathf.Clamp(_currentTMessages + 1, 0,
                        _maxMessages); // Keeps the current C messages from going further then the max messages and only increment by +1 with the min value to start is 0.
            }

            if (_currentTMessages == 0) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = (" ' Caution ' " + " You can not move until i say so.... Press E");
            }

            if (_currentTMessages == 1) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Well hello there.. This is the Timer training room!.. Press E");
            }

            if (_currentTMessages == 2) // If the current C messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("So remember in the last room my friend talked about the clock ??? .. Press E");
            }

            if (_currentTMessages == 3) // If the current C messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Well there is the clock in the top right corner.. Cool ayy.. Press E");
            }

            if (_currentTMessages == 4) // If the current C messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("You must of noticed the clock behind me spinning around ? yeah?... Press E");
            }

            if (_currentTMessages == 5) // If the current C messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Well when that timer reaches less then 5 seconds you will want to grab.. Press E");
            }

            if (_currentTMessages == 6) // If the current C messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("And find these clocks through out the maze before that timer gets to 0;... Press E");
            }

            if (_currentTMessages == 7) // If the current C messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Because if it gets to 0 then you will be sent back to the checkpoints. However!... Press E");
            }

            if (_currentTMessages == 8) // If the current C messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Grabbing these clocks behind me can help in you always moving forwards... Press E");
            }

            if (_currentTMessages == 9) // If the current C messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("So right now the timer up top is staying at 10 but in a second... Press E");
            }

            if (_currentTMessages == 10) // If the current C messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("I will stop holding that timer and let it count down to 0. Try to... Press E");
            }

            if (_currentTMessages == 11) // If the current C messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Get that timer before it reaches 0. To proceed to the next tutorial.. Press E");
            }

            if (_currentTMessages ==
                12) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("You must reach that timer behind me and only then you will be able to teleported... Press E");

            }

            if (_currentTMessages ==
                13) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Timer is let go and you can move... Lets Go!.");
                
                finishedMessages = true;
                _quickTimer -= 0.7f * Time.deltaTime; // Current times count down by 0.7f every frame times Time. Delta Time.

                if (clockT.timerGotHit)
                {
                    _quickTimer = 10;
                    clockT.timerGotHit = false;
                }
                
                if (_quickTimer < 6f && !clockT.timerGotHit)
                {
                    turnOffChat.SetActive(false);
                    _quickTimer = 0;
                }

                if (_quickTimer == 0)
                {
                    SceneManager.LoadScene("RocketBootTrainingRoom");
                }

            }
        }
    }
}

     


