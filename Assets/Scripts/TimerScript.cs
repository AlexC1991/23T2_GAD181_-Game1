using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TimerScript : MonoBehaviour
    {
        [SerializeField] private Text countDownClockTxT; // Grabs the text used on the clock to display the countdown.
        private readonly float _startTime = 10; // Starting clock time and max time.
        [HideInInspector] // Hides the variable underneath from being displayed in the inspector in Unity.
        public float currentTime; // Current Time that is used inside of the timer. This holds whatever is currently the time is at. 
        private readonly float finishTime = 0; // End time for the clock and used to indicate when it reaches 0 this is the min for the current time.
        [HideInInspector] // Hides function underneath from being displayed in the inspector in Unity.
        public bool timeIsUp; // Used to indicate when the time is finished to be used in the Character Movement when to go back to the checkpoint.

        [Header("Scripts")] [SerializeField]
        private CharacterMovement character; // For using public variables & Functions inside of this script from the Character Movement script.
        private TimeRoomScript _timerRoom;
        private float _finishTime;
        private bool _resetLocation;
        [HideInInspector] public bool goToRocketTraining;
        public static event Action<bool> RelocateToRocketRoomEvent;
        public static event Action<bool> RespawnToCheckPointEvent;

            private void Awake()
            {
                timeIsUp = false; // Time being up is false before starting the game from not allowing the timer to continue.
                currentTime =_startTime; // Makes the current time always equal the start time when starting the scene this script is in.
            }

            private void OnEnable()
            {
                ClockObject.AddMoreTime += AddExtraTime;
            }

            private void Update()
            {
                /*if (_timerRoom.currentTMessages == 11 && character.insideOfRocketRoom == false && character.insideOfCheckPointRoom == false) // Conditions are for the training rooms to make sure they don't start the count down while in the training rooms only in the time room it allows but when it counts to 11 messages in total first.
                {*/
                    StartCountingDown(); // Starts the timer method.
                /*}*/

                countDownClockTxT.text =
                    (currentTime)
                    .ToString("F0"); // Grabs the clock text and adds in what the current time is. reading but because its a float and not a int we use F zero to bring it to displaying only 2 digits on the clock.

                if (currentTime < finishTime) // Asks if the current time is less then the finish time and if so it starts the function.
                {
                    timeIsUp = true; // The clock has hit 0 so the time is up will be true.
                    currentTime = _startTime; // The clock time will reset back to the start time.
                }

                if (currentTime < 0.2f) // Asks if the current time is less then the finish time and if so it starts the function.
                {
                    _resetLocation = true;
                    currentTime = _startTime; // The clock time will reset back to the start time.
                }

                RespawnToCheckPointEvent?.Invoke(_resetLocation);
                RelocateToRocketRoomEvent?.Invoke(goToRocketTraining);

                if (currentTime == _startTime)
                {
                    _resetLocation = false;
                }
            }
            private void AddExtraTime(bool moreTimeAdded)
                {
                    if (moreTimeAdded) currentTime = _startTime;
                }

                private void StartCountingDown() // Time count down method to execute the count down timer inside of it.
                {
                    if (timeIsUp && character.resetRocketRoomCounter && character.resetCheckPointRoomCounter)
                    {
                        currentTime -= 0.7f * Time.deltaTime; // Current times count down by 0.7f every frame times Time. Delta Time.
                    }
                }

                private void OnDisable()
                {
                   
                    ClockObject.AddMoreTime -= AddExtraTime;
                }
            
        
    }
}
