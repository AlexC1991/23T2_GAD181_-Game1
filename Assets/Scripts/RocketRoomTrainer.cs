using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class RocketRoomTrainer : MonoBehaviour
    {
        
        [Header("Boots & Controls For Boots")]
        private readonly float _maxBootTimer = 5; // This is the max timer for the rocket boots/shoes.

        [HideInInspector] public float currentBootTimer; // This is the current timer for the rocket boots/shoes.

        private readonly float _finishedBootTimer = 0; // This is the finished timer float for the rocket boot/shoe timer.

       [SerializeField] private GameObject _slideTimer; // This is the slider that is used for the rocket boot/shoe timer.
        [HideInInspector] public bool rocketBootState;
        private bool _doResetScene;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Transform spawnBootsHere;
        [SerializeField] private GameObject rocketBoots;

        [SerializeField] private RocketRoomMovement tMove;

        [SerializeField]
        private Text dialogueText; // Text that sits at the top of the UI Screen that is located on the trainer.\
        private bool _nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private readonly int _maxMessages = 14; // Sets a max message limit for the clamp used underneath.
        private int _currentRMessages;
        private bool _finishedMessages;
        private bool _oneBootOnly;
        [SerializeField] private GameObject turnOffChat;
        [SerializeField] private GameObject rocketScreen;
        private int rocketCounter;
        [SerializeField] private Text daRocketCapacitySir;
        private bool areCollected;
        private float _quickTimer = 1f;

        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            _nextMessagePlease = other.CompareTag("Player"); // Player collides with it making the bool true.
            // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            // Player dose not collide with it making the bool false.
        }
        
        private void Start()
        {
            turnOffChat.SetActive(true);
            _finishedMessages = false;
            _currentRMessages = 0;
            rocketScreen.SetActive(false);
            _oneBootOnly = false;
            areCollected = false;
            rocketBootState = false;
        }

        private void Update()
        {
            Debug.Log(tMove.runSpeed);

            BootsEnabled();
            BootsDisabled();
           
            rocketCounter = tMove._rocketBootCapacity;

            if (rocketCounter > 0)
            {
                rocketScreen.SetActive(true);
            }

            if (_currentRMessages != _maxMessages)
            {
                tMove.runSpeed = 0;
            }
          
            daRocketCapacitySir.text = (rocketCounter).ToString();

            dialogueText.GetComponent<Text>().color = Color.yellow;
            transform.LookAt(2 * transform.position - playerPosition.position);
            
            if (_nextMessagePlease &&
                Input.GetKeyDown(KeyCode
                    .E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                _currentRMessages =
                    Mathf.Clamp(_currentRMessages + 1, 0,
                        _maxMessages); // Keeps the current C messages from going further then the max messages and only increment by +1 with the min value to start is 0.
            }

            if (_currentRMessages == 0) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = (" ' Caution ' " + " You can not move until i say so.... Press E");
            }

            if (_currentRMessages == 1) // If the current C messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Well hello there.. This is the Rocket Boot Training Room!.. Press E");
            }

            if (_currentRMessages == 2) // If the current C messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("So do you like to go fast?? Well sure you do ! hahaha... Press E");
            }

            if (_currentRMessages == 3) // If the current C messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Well I can show how to just remember to use it okay. So.. Press E");
            }

            if (_currentRMessages == 4) // If the current C messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("When you go into the maze there will be checkpoints &.. Press E");
            }

            if (_currentRMessages == 5) // If the current C messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Clocks! but what the other 2 didn't tell you is they don't come.. Press E");
            }

            if (_currentRMessages == 6) // If the current C messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Back and if your suck your going to need some speed to help!.. Press E");
            }

            if (_currentRMessages == 7) // If the current C messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("So every so often inside of the maze you might see a nice shiny.. Press E");
            }

            if (_currentRMessages == 8) // If the current C messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("Pair of rocket boots!.. Well there is a bit more to them then you... Press E");
            }

            if (_currentRMessages == 9) // If the current C messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text =
                    ("Would think??.. Well If you manage to collect some of these boots you will.. Press E");
            }

            if (_currentRMessages == 10) // If the current C messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("See this counter spawn in the bottom right corner.. Hahaha.. Press E");
            }

            if (_currentRMessages == 11) // If the current C messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text =
                    ("Poof! and there it is.. So make sure that counter is there and ready to.. Press E");
                rocketScreen.SetActive(true);
            }

            if (_currentRMessages == 12) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text =
                    ("Go.. Well there is goes haha.. U know what how about u collect these boots.. Press E");
                rocketScreen.SetActive(false);
                _oneBootOnly = true;
            }

            if (_currentRMessages == 13) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text =
                    ("Poof! haha like magic. Remember when you collect them press shift and hold... Press E");
                if (_oneBootOnly)
                {
                    Instantiate(rocketBoots, spawnBootsHere.position, Quaternion.identity);
                    _oneBootOnly = false;
                }
            }
            if (_currentRMessages == 14) // If the current C messages is equal to 12 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                dialogueText.text = ("On tight! Once you collect them go towards the portal and have fun! haha.. U can move now");
                tMove.runSpeed = tMove._originalRunSpeed;
                _quickTimer -= 0.3f * Time.deltaTime;
                if (_quickTimer < 0.2f)
                {
                    turnOffChat.SetActive(false);
                    _quickTimer = 0;
                }
            }
        }
        
        private void BootsEnabled()
        {
            if (rocketBootState) // If the shoes are on the character the function starts below.
            {
                rocketScreen.SetActive(true);
                tMove.runSpeed = tMove.maxRunSpeed;
                _slideTimer.SetActive(true);
                currentBootTimer -= 0.7f * Time.deltaTime; // Starts the timer for the boots.
                AppearingSlider(); // Brings up the slider in view in the UI
                _slideTimer.GetComponent<Slider>().value = SliderCountDown(); // this starts the timer with its variables.
            }
        }

        private void BootsDisabled()
        {
            if (currentBootTimer < 0.3f)
            {
                tMove.runSpeed = tMove._originalRunSpeed;
                rocketBootState = false;
                _slideTimer.SetActive(false);
                _slideTimer.GetComponent<CanvasGroup>().alpha = 0; // Makes the slider invisible in the UI.
                currentBootTimer = _finishedBootTimer; // This resets it back to 0.
            }
        }
        
        private float SliderCountDown() {
            
            return (currentBootTimer / _maxBootTimer); // Gives the variables of the slider of the current and max boot timer of the slider.
        }
        private void AppearingSlider() 
        {
            _slideTimer.GetComponent<CanvasGroup>().alpha = 1; // Makes the slider appear in the UI.
        }
    }
}
