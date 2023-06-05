using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Text countDownClockTxT; // Grabs the text used on the clock to display the countdown.
    private float startTime = 10; // Starting clock time and max time.
    [HideInInspector] // Hides the variable underneath from being displayed in the inspector in Unity.
    public float currentTime; // Current Time that is used inside of the timer. This holds whatever is currently the time is at. 
    private float finishTime = 0; // End time for the clock and used to indicate when it reaches 0 this is the min for the current time.
    [HideInInspector] // Hides function underneath from being displayed in the inspector in Unity.
    public bool timeIsUp; // Used to indicate when the time is finished to be used in the Character Movement when to go back to the checkpoint.
    private float previousTime; // Keeps the current time safe for any issues including debugging.
    private bool previousTimeIsUp = false; // Unused currently. It was used for making a mechanic when you go to a checkpoint too many times and give u a lose screen.
    
    [Header("Scripts")] 
    [SerializeField] private CharacterMovement character; // For using public variables & Functions inside of this script from the Character Movement script.
    [SerializeField] private TimeRoomScript timerRoom; // For using public variables & Functions inside of this script from the Time Room Script script.
    

    private void Awake()
    {
        timeIsUp = false; // Time being up is false before starting the game from not allowing the timer to continue.
        currentTime = startTime; // Makes the current time always equal the start time when starting the scene this script is in.
    }

    private void Start()
    {
        previousTime = currentTime; // Previous time will always be the backup time of the current time so will equal the current time from the start.
    }
    private void Update()
    {
        //if (previousTime > finishTime && currentTime == finishTime) //////////////// Unused currently as it was a checkpoint counter to reset back to start with a lose screen. Had to move on from as it was causing issues and unable to make it work in the time frame.
        //{
           //if (!previousTimeIsUp) // Check if timeIsUp transitioned from false to true
            //{
                //character.currentTries += 1;
                //character.currentTries = Mathf.Clamp(character.currentTries, 0, 5);

            //}
            //previousTimeIsUp = true;
        //}
        //else
        //{
            //previousTimeIsUp = false;
        //} 

        if (timerRoom.currentTMessages == 11 && character.insideOfRocketRoom == false && character.insideOfCheckPointRoom == false) // Conditions are for the training rooms to make sure they don't start the count down while in the training rooms only in the time room it allows but when it counts to 11 messages in total first.
        {
            StartCountingDown(); // Starts the timer method.
        }
        
        countDownClockTxT.text = (currentTime).ToString("F0"); // Grabs the clock text and adds in what the current time is. reading but because its a float and not a int we use F zero to bring it to displaying only 2 digits on the clock.

        if (currentTime < finishTime) // Asks if the current time is less then the finish time and if so it starts the function.
        {
            timeIsUp = true; // The clock has hit 0 so the time is up will be true.
            currentTime = startTime; // The clock time will reset back to the start time.
            previousTime = currentTime; // Previous time will equal whatever the currentTime is.
        }
        
        if (character.moreTime == true) // Checks if the Character Movement script more time bool is true. If so it will give the clock in game more time. 
        {
            currentTime = startTime; // Whatever the current time is at it will make the current time be back to the start time.
            previousTime = currentTime; // Previous time will reset to whatever the current time is again.
            character.moreTime = false; // The more time bool will turn back to false preventing any issues with communication between the scripts.
        }
        
        previousTime = currentTime; // previous time continues to be updated via the Update method.
    }

    private void StartCountingDown() // Time count down method to execute the count down timer inside of it.
    {
        currentTime -= 0.7f * Time.deltaTime; // Current times count down by 0.7f every frame times Time. Delta Time.
    }
    
}
}
