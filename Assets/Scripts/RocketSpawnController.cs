using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class RocketSpawnController : MonoBehaviour
    {
        [Header("Mouse Controls")] 
        private float _mouseXposition;

        [Header("Boots & Character Foot Locations")]
        private GameObject _bootLeft; // Gets the left boot from the rocket boot/shoe prefab.
        private GameObject _bootRight; // Gets the right boot from the rocket boot/shoe prefab.
        [SerializeField] private Transform _footLeft; // This is the left foot of the character for the placement of the left rocket boot/shoe.
        [SerializeField] private Transform _footRight; // This is the right foot of the character for the placement of the right rocket boot/shoe.

        [Header("Boots & Controls For Boots")] 
        private float MaxBootTimer = 5; // This is the max timer for the rocket boots/shoes.
        [HideInInspector] public float _currentBootTimer; // This is the current timer for the rocket boots/shoes.
        private float FinishedBootTimer = 0; // This is the finished timer float for the rocket boot/shoe timer.
        private GameObject _slideTimer; // This is the slider that is used for the rocket boot/shoe timer.
        private bool _rocketBootState;
        public static event Action<bool> resetTheSpeedOfCharacterEvent;
        private bool _doResetScene;
        private void Awake() 
        {
            _slideTimer = GameObject.Find("Slider"); // Finds the Slider in the game hierarchy and assigns it to the slideTimer variable.
            _slideTimer.GetComponent<CanvasGroup>().alpha = 0; // Sets the sliderTimer to be invisible using the CanvasGroup.
            _rocketBootState = false;
            _doResetScene = false;
            _slideTimer.SetActive(false);
            _currentBootTimer = MaxBootTimer;
        }
        private void OnEnable()
        {
            CharacterMovement.ActivateRocketBootStateEvent += StartTimeRBootState;
            CharacterMovement.ResetTCurrentMessageEvent += ResetScene;
        }
        private void Update()
        {
            if (!_rocketBootState)
            {
                _currentBootTimer = MaxBootTimer;
            }
            BootsEnabled();
            BootsDisabled();
            resetTheSpeedOfCharacterEvent?.Invoke(_rocketBootState);
        }
        private void BootsEnabled()
        {
            if (_rocketBootState && _currentBootTimer > 0.2f)
            {   
                _slideTimer.SetActive(true);
                _currentBootTimer -= 0.7f * Time.deltaTime; // Starts the timer for the boots.
                AppearingSlider(); // Brings up the slider in view in the UI
                // If the shoes are on the character the function starts below.
                _bootLeft = GameObject.FindWithTag("LeftRocketShoe"); // This finds the left boot used from the prefab by looking for the tag used by the prefab.
                _bootRight = GameObject.FindWithTag("RightRocketShoe"); // This finds the right boot used from the prefab by looking for the tag used by the prefab.
                _slideTimer.GetComponent<Slider>().value = SliderCountDown(); // this starts the timer with its variables.
                _bootLeft.transform.position = _footLeft.transform.position; // puts the left boot position onto the left foot while using the boot.
                _bootRight.transform.position = _footRight.transform.position; // puts the right boot position onto the right foot while using the boot.
            }
        }
        private void BootsDisabled()
        {
            if (_currentBootTimer < 0.3f)
            {
                _rocketBootState = false;
                _slideTimer.SetActive(false);
                _slideTimer.GetComponent<CanvasGroup>().alpha = 0; // Makes the slider invisible in the UI.
                _currentBootTimer = FinishedBootTimer; // Makes the current boot timer equal to the finished boot timer so does not reset.
                
                if (_doResetScene)
                {
                    SceneManager.LoadScene("Maze 1");
                    _doResetScene = false;
                }
            }
        }
        private float SliderCountDown() {
            
            return (_currentBootTimer / MaxBootTimer); // Gives the variables of the slider of the current and max boot timer of the slider.
        }
        private void AppearingSlider() 
        {
            _slideTimer.GetComponent<CanvasGroup>().alpha = 1; // Makes the slider appear in the UI.
        }
        private void StartTimeRBootState(bool timeToMoveFaster)
        {
            if (timeToMoveFaster) _rocketBootState = true;
        }

        private void ResetScene(bool _didRelocateToRRoom)
        {
            if (_didRelocateToRRoom) _doResetScene = true;
        }
        private void OnDisable()
        {
            CharacterMovement.ActivateRocketBootStateEvent -= StartTimeRBootState;
            CharacterMovement.ResetTCurrentMessageEvent -= ResetScene;
        }
    }
}
