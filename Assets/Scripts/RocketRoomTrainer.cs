using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class RocketRoomTrainer : MonoBehaviour
    {
        [Header("TimeRoomScript"), SerializeField]
        private TimeRoomScript timerRoomS;
        [SerializeField] private Transform bootSpawnLocationPoint; // Transform Position for where the boots will spawn in the training room.
        [SerializeField] private GameObject spawnBootsPlease; // This is the Prefab of the rocket boots that will spawn and be used by the player.
        [SerializeField] private Text bottomText; // Text that sits at the bottom of the UI Screen that is located on the trainer.
        [SerializeField] private Text topText; // Text that sits at the top of the UI Screen that is located on the trainer.
        private bool _nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private readonly int _maxMessages = 11; // Sets a max message limit for the clamp used underneath.
        private bool _hasSpawned; // Checks to make sure only 1 instance of the spawned boots is sent and not multiple when message 9 is the current message count.
        private bool _yesHeIs;
        [HideInInspector] public bool canRelocateToMaze;
        private float _countDownBegins;
        private float _originalCountDownTimer;
        [HideInInspector]
        public int currentRMessages; // Current message count for what message is at what and keeps the dialog going. It's used in the Character Movement script as well to allow the player to do stuff at the end.
        public static event Action<int> LetTheCharacterMoveInTheRocketRoom; 
        public static event Action<bool> CharactersBootsNeedSpawning;
        private void OnTriggerEnter(Collider other) // Detects when the something named other enters the collider.
        {
            _nextMessagePlease = other.CompareTag("Player"); // Player collides with it making the bool true.
            // Gives the other a name to look for when it collides with it. In this instance it's the GameObject that has the tag in unity called Player.
            // Player dose not collide with it making the bool false.
        }
        private void OnEnable()
        {
            SpawnLocations.RocketBootRoomEvent += IsHeInsideOfRRoom;
        }
        private void Start()
        {
            currentRMessages = 0;
            _countDownBegins = 6;
            _originalCountDownTimer = _countDownBegins;
        }
        private void IsHeInsideOfRRoom(bool insideOfRocketRoom)
        {
            if (insideOfRocketRoom) _yesHeIs = true;
        }
        private void Update()
        {
            Debug.Log("Current Rocket Room Message " + currentRMessages);
            if (_nextMessagePlease && _yesHeIs) InsideOfThisRoom();
            LetTheCharacterMoveInTheRocketRoom?.Invoke(currentRMessages);
            CharactersBootsNeedSpawning?.Invoke(canRelocateToMaze);
        }
        private void InsideOfThisRoom()
        {
            timerRoomS.currentTMessages = 0;
            canRelocateToMaze = false;
            
            if (_nextMessagePlease && Input.GetKeyDown(KeyCode.E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                _countDownBegins = _originalCountDownTimer;
                currentRMessages = Mathf.Clamp(currentRMessages +1, 0, _maxMessages); // Keeps the current R messages from going further then the max messages and only increment by +1 with the min value to start is 0.

            }

            if (currentRMessages == 1) // If the current R messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Rocket Shoes Last");
                bottomText.text = ("5 Seconds.");
            }  

            if (currentRMessages == 2) // If the current R messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("They Will Be");
                bottomText.text = ("Randomly Put Around");
            }  

            if (currentRMessages == 3) // If the current R messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("The Map. Keep");
                bottomText.text = ("An Eye Out");
            }  
            
            if (currentRMessages == 4) // If the current R messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("For Them");
                bottomText.text = ("To Get A");
            }  
            
            if (currentRMessages == 5) // If the current R messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Advantage. Just");
                bottomText.text = ("Walk Up To");
            } 
            
            if (currentRMessages == 6) // If the current R messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Them To Use");
                bottomText.text = ("Them And");
            } 
            
            if (currentRMessages == 7) // If the current R messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Hold On Tight.");
                bottomText.text = ("Now Lets Try");
            }  
            
            if (currentRMessages == 8) // If the current R messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("These Boots On!");
                bottomText.text = ("Poof!. E Please");
            }  
            
            if (currentRMessages == 9) // If the current R messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                if (!_hasSpawned) // If has spawned is false which it is as default. It will allow the Instance to spawn.
                {
                    Instantiate(spawnBootsPlease, bootSpawnLocationPoint.position, Quaternion.identity); // Instantiate the prefab of the boots in the boot location as stated above and to have the item spawn rotated when spawned into the scene.
                    _hasSpawned = true; // Has spawned becomes true and cancels out any more chances of it spawning more.
                }
                topText.text = ("There We Are."); 
                bottomText.text = ("Try Them On.");
            }
            
            if (currentRMessages == 10) // If the current R messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("And Once It");
                bottomText.text = ("Finishes We Can");
            }
            
            if (currentRMessages == 11) // If the current R messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Proceed To The");
                bottomText.text = ("Main Game!..");
                _countDownBegins -= 0.7f * Time.deltaTime;

                if (_countDownBegins < 0.2f)
                {
                    _countDownBegins = 0;
                    _countDownBegins = _originalCountDownTimer;
                    currentRMessages = 0;
                }
            }
        }
        private void OnDisable()
        {
            SpawnLocations.RocketBootRoomEvent -= IsHeInsideOfRRoom;
        }
    }      
}
