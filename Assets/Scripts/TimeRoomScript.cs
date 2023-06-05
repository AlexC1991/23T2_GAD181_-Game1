using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TimeRoomScript : MonoBehaviour
    {
        [SerializeField] private Text bottomText; // Text that sits at the bottom of the UI Screen that is located on the trainer.
        [SerializeField] private Text topText; // Text that sits at the top of the UI Screen that is located on the trainer.
        private bool nextMessagePlease; // Transfers a bool to allow the KeyCode E to be pressed from the collider.
        private int maxMessages = 11; // Sets a max message limit for the clamp used underneath.
        [HideInInspector]
        public int currentTMessages; // Current message count for what message is at what and keeps the dialog going. It's used in the Character Movement script as well to allow the player to do stuff at the end.


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

            if (nextMessagePlease && Input.GetKeyDown(KeyCode.E)) // Will only activate if I am in the collider and the next message please is set true as well as me pressing the E on the keyboard.
            {
                currentTMessages = Mathf.Clamp(currentTMessages +1, 0, maxMessages); // Keeps the current T messages from going further then the max messages and only increment by +1 with the min value to start is 0.

            }

            if (currentTMessages == 1) // If the current T messages is equal to 1 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Hello! This is").ToString();
                bottomText.text = ("The Time Room.").ToString();
            }

            if (currentTMessages == 2) // If the current T messages is equal to 2 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Here We Have").ToString();
                bottomText.text = ("A Clock..").ToString();
            }

            if (currentTMessages == 3) // If the current T messages is equal to 3 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("This Keeps Your").ToString();
                bottomText.text = ("Time Always").ToString();
            }

            if (currentTMessages == 4) // If the current T messages is equal to 4 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("At 10 Seconds.").ToString();
                bottomText.text = ("This Keeps You").ToString();
            }
            
            if (currentTMessages == 5) // If the current T messages is equal to 5 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("Moving Through").ToString();
                bottomText.text = ("The Maze.").ToString();
            }
            
            if (currentTMessages == 6) // If the current T messages is equal to 6 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("If You Collect").ToString();
                bottomText.text = ("This Clock In").ToString();
            }
            
            if (currentTMessages == 7) // If the current T messages is equal to 7 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("The Maze, It").ToString();
                bottomText.text = ("Will Go Away.").ToString();
            }
            
            if (currentTMessages == 8) // If the current T messages is equal to 8 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("So Be Strategic").ToString();
                bottomText.text = ("Around the Maze").ToString();
            }
            
            if (currentTMessages == 9) // If the current T messages is equal to 9 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("And Don't Get").ToString();
                bottomText.text = ("Lost!. See").ToString();
            }
            
            if (currentTMessages == 10) // If the current T messages is equal to 10 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("That Clock There?").ToString();
                bottomText.text = ("Go & Collect It").ToString();
            }
            
            if (currentTMessages == 11) // If the current T messages is equal to 11 it will play the text provided in the top & bottom text followed by a to string at the end to send it as a string which is a text format
            {
                topText.text = ("And See What").ToString();
                bottomText.text = ("Happens & Be Ready!").ToString();
            }

        }
    }      
}

