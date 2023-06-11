using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class RocketRoomTrainer : MonoBehaviour
    {
        [SerializeField] private Transform bootSpawnLocationPoint; // Transform Position for where the boots will spawn in the training room.
        [SerializeField] private GameObject spawnBootsPlease; // This is the Prefab of the rocket boots that will spawn and be used by the player.
        [SerializeField] private Text bottomText; // Text that sits at the bottom of the UI Screen that is located on the trainer.
        [SerializeField] private Text topText; // Text that sits at the top of the UI Screen that is located on the trainer.
        private bool nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private int maxMessages = 11; // Sets a max message limit for the clamp used underneath.
        private bool hasSpawned = false; // Checks to make sure only 1 instance of the spawned boots is sent and not multiple when message 9 is the current message count.
        [HideInInspector]
        public int currentRMessages; // Current message count for what message is at what and keeps the dialog going. It's used in the Character Movement script as well to allow the player to do stuff at the end.
        private bool yesHeIs;
        private bool canRelocateToMaze;
        
        public static event Action<int> _LetTheCharacterMoveInTheRocketRoom; 
        public static event Action<bool> _CharactersBootsNeedSpawning;
        private void Start()
        {
            currentRMessages = 0;
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
            SpawnLocations._RocketBootRoomEvent += IsHeInsideOfRRoom;
            CharacterMovement._ResetRCurrentMessageEvent += ResetRMessages;
        }

        private void OnDisable()
        {
            SpawnLocations._RocketBootRoomEvent -= IsHeInsideOfRRoom;
            CharacterMovement._ResetRCurrentMessageEvent -= ResetRMessages;
        }
        private void IsHeInsideOfRRoom(bool insideOfRocketRoom)
        {
            if (insideOfRocketRoom) yesHeIs = true;
        }
        private void Update()
        {
            if (nextMessagePlease && yesHeIs) InsideOfThisRoom();
            _LetTheCharacterMoveInTheRocketRoom?.Invoke(currentRMessages);
            _CharactersBootsNeedSpawning?.Invoke(canRelocateToMaze);
        }
        private void InsideOfThisRoom()
        {
            canRelocateToMaze = false;
            
            if (nextMessagePlease && Input.GetKeyDown(KeyCode.E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                currentRMessages = Mathf.Clamp(currentRMessages +1, 0, maxMessages); // Keeps the current R messages from going further then the max messages and only increment by +1 with the min value to start is 0.

            }

            if (currentRMessages == 1) // If the current R messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Rocket Shoes Last").ToString();
                bottomText.text = ("5 Seconds.").ToString();
            }  

            if (currentRMessages == 2) // If the current R messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("They Will Be").ToString();
                bottomText.text = ("Randomly Put Around").ToString();
            }  

            if (currentRMessages == 3) // If the current R messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("The Map. Keep").ToString();
                bottomText.text = ("An Eye Out").ToString();
            }  
            
            if (currentRMessages == 4) // If the current R messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("For Them").ToString();
                bottomText.text = ("To Get A").ToString();
            }  
            
            if (currentRMessages == 5) // If the current R messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Advantage. Just").ToString();
                bottomText.text = ("Walk Up To").ToString();
            } 
            
            if (currentRMessages == 6) // If the current R messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Them To Use").ToString();
                bottomText.text = ("Them And").ToString();
            } 
            
            if (currentRMessages == 7) // If the current R messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("Hold On Tight.").ToString();
                bottomText.text = ("Now Lets Try").ToString();
            }  
            
            if (currentRMessages == 8) // If the current R messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                topText.text = ("These Boots On!").ToString();
                bottomText.text = ("Poof!. E Please").ToString();
            }  
            
            if (currentRMessages == 9) // If the current R messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format.
            {
                if (!hasSpawned) // If has spawned is false which it is as default. It will allow the Instance to spawn.
                {
                    Instantiate(spawnBootsPlease, bootSpawnLocationPoint.position, Quaternion.identity); // Instantiate the prefab of the boots in the boot location as stated above and to have the item spawn rotated when spawned into the scene.
                    hasSpawned = true; // Has spawned becomes true and cancels out any more chances of it spawning more.
                }
                topText.text = ("There We Are.").ToString(); 
                bottomText.text = ("Try Them On.").ToString();
            }
            
            if (currentRMessages == 10) // If the current R messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("And Once It").ToString();
                bottomText.text = ("Finishes We Can").ToString();
            }
            
            if (currentRMessages == 11) // If the current R messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Proceed To The").ToString();
                bottomText.text = ("Main Game!..").ToString();
                float countDownBegins = 7;
                countDownBegins -= 0.7f * Time.deltaTime;

                if (countDownBegins < 0.2f)
                {
                    canRelocateToMaze = true;
                }

            }

        }

        private void ResetRMessages(bool didRelocateToMazeRoom)
        {
            if (didRelocateToMazeRoom) currentRMessages = 0;
        }
    }      
}
