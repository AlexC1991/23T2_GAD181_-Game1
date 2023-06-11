using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TimerScript : MonoBehaviour
{
    [SerializeField] private Text countDownClockTxT; // Grabs the text used on the clock to display the countdown.
    private float startTime = 10; // Starting clock time and max time.
    private float currentTime; // Current Time that is used inside of the timer. This holds whatever is currently the time is at. 
    private float finishTime = 0; // End time for the clock and used to indicate when it reaches 0 this is the min for the current time.
    private bool timeIsUp; // Used to indicate when the time is finished to be used in the Character Movement when to go back to the checkpoint.
    private bool previousTimeIsUp = false; // Unused currently. It was used for making a mechanic when you go to a checkpoint too many times and give u a lose screen.
    private bool resetLocation;
    private bool goToRocketTraining;
    
    [Header("Scripts")] 
    [SerializeField] private CharacterMovement character; // For using public variables & Functions inside of this script from the Character Movement script.
    [SerializeField] private TimeRoomScript timerRoom; // For using public variables & Functions inside of this script from the Time Room Script script.

    public static event Action<bool> RelocateToRocketRoomEvent;
    public static event Action<bool> _RespawnToCheckPointEvent; 
    private void Awake()
    {
        timeIsUp = false; // Time being up is false before starting the game from not allowing the timer to continue.
        currentTime = startTime; // Makes the current time always equal the start time when starting the scene this script is in.
    }
    private void OnEnable()
    {
        SpawnLocations._RocketBootRoomEvent += TimerStopsMovingRocketRoom;
        SpawnLocations._CheckPointRoomEvent += TimerStopsMovingCheckPointRoom;
        SpawnLocations._MainMazeRoomEvent += TimeContinuesToMoveInMaze;
        TimeRoomScript._LetTheCharacterMoveInTheTimeRoom += TimeIsOnForTesting;
        ClockObject.addMoreTime += AddExtraTime;
    }
    private void OnDisable()
    {
        SpawnLocations._RocketBootRoomEvent -= TimerStopsMovingRocketRoom;
        SpawnLocations._CheckPointRoomEvent -= TimerStopsMovingCheckPointRoom;
        SpawnLocations._MainMazeRoomEvent -= TimeContinuesToMoveInMaze;
        TimeRoomScript._LetTheCharacterMoveInTheTimeRoom -= TimeIsOnForTesting;
        ClockObject.addMoreTime -= AddExtraTime;
    }
    
    private void Update()
    {
        StartCountingDown();
        countDownClockTxT.text = (currentTime).ToString("F0"); // Grabs the clock text and adds in what the current time is. reading but because its a float and not a int we use F zero to bring it to displaying only 2 digits on the clock.
        
        if (currentTime < finishTime) // Asks if the current time is less then the finish time and if so it starts the function.
        {
            resetLocation = true;
            currentTime = startTime; // The clock time will reset back to the start time.
            resetLocation = false;
        }
        _RespawnToCheckPointEvent?.Invoke(resetLocation);
        RelocateToRocketRoomEvent?.Invoke(goToRocketTraining);
    }

    private void AddExtraTime(bool moreTimeAdded)
    {
        if (moreTimeAdded) currentTime = startTime;
    }
    private void StartCountingDown() // Time count down method to execute the count down timer inside of it.
    {
        if (timeIsUp)
        {
            currentTime -= 0.7f * Time.deltaTime; // Current times count down by 0.7f every frame times Time. Delta Time.
        }
    }
    private void TimerStopsMovingRocketRoom(bool insideOfRocketRoom)
    {
        if (insideOfRocketRoom)
        {
            timeIsUp = false;
            currentTime = startTime;
            goToRocketTraining = false;
        }
    }
    private void TimerStopsMovingCheckPointRoom(bool insideOfCheckPointRoom)
    {
        if (insideOfCheckPointRoom) timeIsUp = false;
    }
    private void TimeContinuesToMoveInMaze(bool insideOfMainMazeRoom)
    {
        if (insideOfMainMazeRoom) timeIsUp = true;
    }
    private void TimeIsOnForTesting(int currentTMessages)
    {
        if (currentTMessages == 11)
        {
            timeIsUp = true;
            
            if (currentTime < 5)
            {
                goToRocketTraining = true;
            }
        }
        
    }
    
}
}
