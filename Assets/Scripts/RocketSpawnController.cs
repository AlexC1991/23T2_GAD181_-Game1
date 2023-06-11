using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class RocketSpawnController : MonoBehaviour
    {
        [Header("Mouse Controls")] 
        private float mouseXposition;
        private float mouseSensitivity = 1;
        
        [Header("Boots & Character Foot Locations")]
        private GameObject bootLeft; // Gets the left boot from the rocket boot/shoe prefab.
        private GameObject bootRight; // Gets the right boot from the rocket boot/shoe prefab.
        private Vector3 footLeft; // This is the left foot of the character for the placement of the left rocket boot/shoe.
        private Vector3 footRight; // This is the right foot of the character for the placement of the right rocket boot/shoe.
        private bool shoesOn; // This Bool states if the rocket boots/shoes are on or not.

        [Header("Boots & Controls For Boots")]
        private float maxBootTimer = 5; // This is the max timer for the rocket boots/shoes.
        private float currentBootTimer; // This is the current timer for the rocket boots/shoes.
        private float finishedBootTimer = 0; // This is the finished timer float for the rocket boot/shoe timer.
        private GameObject slideTimer; // This is the slider that is used for the rocket boot/shoe timer.
        private bool shouldTeleport = false; // This is a Bool used to indicate if the player should be teleported when interacting with the check point with the trainer.
        private bool spawnThemBootsSirPlease = false; // This asks yes or no to spawning the boots in the trainer room.
        private bool insideOfTimeRoom = false; // Indicates if your inside of the time room or not.
        private bool rocketBootState;
        
        private void Awake() {
            
            footLeft = GameObject.Find("RocketShoeLeftLocation").transform.position; // Finds the game objects transform position and assigns it to foot left. This being the left foot of the character.
            footRight = GameObject.Find("RocketShoeRightLocation").transform.position; // Finds the game objects transform position and assigns it to foot right. This being the right foot of the character.
            slideTimer = GameObject.Find("Slider"); // Finds the Slider in the game hierarchy and assigns it to the slideTimer variable.
            slideTimer.GetComponent<CanvasGroup>().alpha = 0; // Sets the sliderTimer to be invisible using the CanvasGroup.
        }

        private void OnEnable()
        {
            CharacterMovement._ActivateRocketBootStateEvent += StartTimeRBootState;
        }

        private void OnDisable()
        {
            CharacterMovement._ActivateRocketBootStateEvent -= StartTimeRBootState;
        }

        private void Update()
        {
            mouseXposition = mouseSensitivity * Input.GetAxis("Mouse X"); // grabs the mouse X axis every frame for the rotation movement.
            transform.Rotate(Vector3.up, mouseXposition * 80 * Time.deltaTime); // Gets the mouse input and uses it to rotate the boots with the character.
            BootsEnabled();
            BootsDisabled();
        }
        
        private void BootsEnabled() {

            if (rocketBootState)
            {
                // If the shoes are on the character the function starts below.
                bootLeft = GameObject.FindWithTag(
                    "LeftRocketShoe"); // This finds the left boot used from the prefab by looking for the tag used by the prefab.
                bootRight = GameObject.FindWithTag(
                    "RightRocketShoe"); // This finds the right boot used from the prefab by looking for the tag used by the prefab.
                footLeft = GameObject.Find("RocketShoeLeftLocation").transform
                    .position; // This finds the left foot of the character by name in the Hierarchy and its position. 
                footRight = GameObject.Find("RocketShoeRightLocation").transform
                    .position; // This finds the right foot of the character by name in the Hierarchy and its position.
                slideTimer.GetComponent<Slider>().value =
                    SliderCountDown(); // this starts the timer with its variables.
                transform.Rotate(Vector3.up,
                    mouseXposition * 120 *
                    Time.deltaTime); // Gets the tracking of the mouse so the boots/shoes turn with the mouse rotation.
                bootLeft.transform.position =
                    footLeft; // puts the left boot position onto the left foot while using the boot.
                bootRight.transform.position =
                    footRight; // puts the right boot position onto the right foot while using the boot.
                currentBootTimer -= 0.8f * Time.deltaTime; // Starts the timer for the boots.
                AppearingSlider(); // Brings up the slider in view in the UI
            }
        }

        private void BootsDisabled()
        {
            if (!(currentBootTimer < 0.1f) || rocketBootState) // If the timer has finished for using the boots in game.
            {
                shoesOn = false; // Shoes are no longer on so it is false.
                slideTimer.GetComponent<CanvasGroup>().alpha = 0; // Makes the slider invisible in the UI.
                currentBootTimer = finishedBootTimer; // Makes the current boot timer equal to the finished boot timer so does not reset.
                rocketBootState = false;
            }
        }

        private float SliderCountDown() {
            
            return (currentBootTimer / maxBootTimer); // Gives the variables of the slider of the current and max boot timer of the slider.
        }
        private void AppearingSlider() {
            
            slideTimer.GetComponent<CanvasGroup>().alpha = 1; // Makes the slider appear in the UI.
        }

        private void StartTimeRBootState(bool timeToMoveFaster)
        {
            if (timeToMoveFaster) rocketBootState = true;
        }
    }
}
