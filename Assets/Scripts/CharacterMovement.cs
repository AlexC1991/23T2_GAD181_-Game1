using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Scripts")] 
        [SerializeField] private TimerScript timeScript; // Uses the TimerScript script in this script under timeScript.
        [SerializeField] private RocketRoomTrainer roomRScript; // Uses the RocketRoomTrainer script in this script under roomRScript.
        [SerializeField] private TimeRoomScript timeRoomS; // Uses the TimeRoomScript script in this script under timeRoomS.
        [SerializeField] private CheckPointRoom checkRoomPoint; // Uses the CheckPointRoom script in this script under checkRoomPoint.
        [SerializeField] private TestingCheckP testCheck; // Uses the TestingCheckP script in this script under testCheck.
        
        [Header("Character Run Speed")]
        [HideInInspector] public float runSpeed = 8f; // This is the run speed of the character this script is attached to.
        [SerializeField] private float maxRunSpeed = 12; // This is the max run speed which is used for the rocket boots/shoes in game.
        private float originalRunSpeed; // This keeps the original run speed to return to when needed.
        private bool timeToMoveFaster; // This Bool gives a yes/no for when you activate the rocket boots/shoes.

        [Header("Character Jump Height")] 
        [SerializeField] private float jumpHeight = 3; // This is the jump height the character can jump up to.

        [Header("SpawnLocations")]
        [SerializeField] private Transform startPosition; // This houses the start location for the character when it starts the main game.
        [SerializeField] public Transform timeSpawnPoint; // This houses the position for the character when it starts in the time tutorial room.
        [SerializeField] private Transform rocketSpawnPoint; // This houses the position for the character when it starts in the rocket boot/shoe tutorial room.
        [SerializeField] private Transform checkSpawnPoint; // This houses the position for the character when it starts in the check point tutorial room.
        [HideInInspector] public bool insideOfRocketRoom; // This Bool gives a yes or no when the character is located in the rocket room or not.
        [HideInInspector] public bool insideOfCheckPointRoom; // This Bool gives a yes or no when the character is located in the checkpoint room or not.
        
        [Header("Character info")]
        [SerializeField] private CharacterController controller; // This gets the character controller to be used inside of this script to move the character.
        [SerializeField] private float mouseSensitivity = 5; // Amplifies the mouse movement to be more sensitive when used to rotate the character.
        [SerializeField] private float characterGravity = 20; // Sets the gravity for the character.

        [Header("Various Variables Used")]
        [HideInInspector] public bool moreTime; // This gives a yes or no for if the player gets to have more time when collecting the clocks. This is public to be used in the Time Script.
        [HideInInspector] public bool respawnStart = false; // This is unused atm due to a bug issue preventing the character to reset correctly. I also have to add in a reset for the whole map.
        [HideInInspector] public int currentTries; // This is currently a bug as well which is supposed to count how many times you go back to a check point but unable to have the counter to work correctly in my development time frame.
        private float mouseXposition; // Gets the position of the mouse using the float values of its X position in the world.
        private Vector3 newPosition; // This sets a new position when going through the checkpoint system.
        private Vector3 moveDirection; // This gathers the move direction for when the character rotates and adjusts the position the character faces.
        private float moveHorizontal; // Speed of which the character moves in the Horizontal axis.
        private float moveVertical; // Speed of which the character moves in the Vertical axis.
        private bool alwaysCheckSpeed; // Bool say yes or no for when to change the speed with the characters movement.
        private bool beforeGameStarts = false; // This is for a image to come up on the screen or not which happens before you start playing the main game.
        private bool menuDuringGame = false; // This is for the in game menu to pop up or not and only activates when pressing ESC in the main game.
        private int maxTries = 5; // This is a bug which goes with the current tries and have issues getting the counter to work with it.
        private int startingTries = 0; // This is part of the bug with the check point system being counted.
        private float startCount = 1; // This count down is for the bug that is supposed to reset back to the main game but currently is in a work in progress to make work.
        private bool relocatedToSpawnPoint; // This indicates when you have to go back to the checkpoint when time runs out.

        [Header("UI Elements")] 
        [SerializeField] private Text counterText; // Text for the bug checkpoint counter. 
        [SerializeField] private GameObject beforeItStarts; // This is the before it starts UI Image when you finish the tutorials and go onto the main menu.
        [SerializeField] private GameObject inGameMenuStart; // Pulls up the in game menu UI.
        [SerializeField] private GameObject soundOnYes; // This shows the button for sounds on.
        [SerializeField] private GameObject soundOnNo; // this shows the button for sounds off.
        [SerializeField] private Text soundIsWhat; // This text shows on the screen that the sound is currently doing if it be on or off.
        
        [Header("Boots")] 
        private GameObject bootLeft; // Gets the left boot from the rocket boot/shoe prefab.
        private GameObject bootRight; // Gets the right boot from the rocket boot/shoe prefab.
        private Vector3 footLeft; // This is the left foot of the character for the placement of the left rocket boot/shoe.
        private Vector3 footRight; // This is the right foot of the character for the placement of the right rocket boot/shoe.
        private bool shoesOn; // This Bool states if the rocket boots/shoes are on or not.
        private float maxBootTimer = 5; // This is the max timer for the rocket boots/shoes.
        private float currentBootTimer; // This is the current timer for the rocket boots/shoes.
        private float finishedBootTimer = 0; // This is the finished timer float for the rocket boot/shoe timer.
        private GameObject slideTimer; // This is the slider that is used for the rocket boot/shoe timer.
        private bool shouldTeleport = false; // This is a Bool used to indicate if the player should be teleported when interacting with the check point with the trainer.
        private bool spawnThemBootsSirPlease = false; // This asks yes or no to spawning the boots in the trainer room.
        private bool insideOfTimeRoom = false; // Indicates if your inside of the time room or not.
        private float quickGetThem = 4; // This is the timer for getting the rocket boots/shoes in the rocket boot/shoe training room. This is not shown in game however.
        
        [Header("Sounds")] 
        [SerializeField] private AudioSource _mainGameSound; // Main game music that is played during the main game play.
        [SerializeField] private AudioSource _jumpSounds; // Jump sounds for when the character jumps.
        [SerializeField] private AudioSource _timeSounds; // Clock sound for when you collide with the clock in game.
        [SerializeField] private AudioSource _rocketTimeSounds; // This sound is used while you are using the rocket boots/shoes as an effect.
        
        public static event Action<bool> DestroyTheBootsEvent; // Action Event sending out a bool using the event name DestroyTheBootsEvent. This sends out to destroy the boots.
        public static event Action<bool> StartSpawningThemBoots; // Action Event sending out a bool using the event name StartSpawningThemBoots. This sends out when to start spawning the boots.

        private void Awake() {
            
            footLeft = GameObject.Find("RocketShoeLeftLocation").transform.position; // Finds the game objects transform position and assigns it to foot left. This being the left foot of the character.
            footRight = GameObject.Find("RocketShoeRightLocation").transform.position; // Finds the game objects transform position and assigns it to foot right. This being the right foot of the character.
            slideTimer = GameObject.Find("Slider"); // Finds the Slider in the game hierarchy and assigns it to the slideTimer variable.
            slideTimer.GetComponent<CanvasGroup>().alpha = 0; // Sets the sliderTimer to be invisible using the CanvasGroup.
        }
        private void Start() {
            
            roomRScript.currentRMessages = 0; // Defaults the current R messages to be 0;
            checkRoomPoint.currentCMessages = 0; // Defaults the current C messages to be 0;
            timeRoomS.currentTMessages = 0; // Defaults the current T messages to be 0;
            Time.timeScale = 1; // Sets the game to run at normal speed.
            currentTries = startingTries; // This is for the bug that was mentioned above. This variable isn't currently used.
            alwaysCheckSpeed = true; // This makes sure the run speed is always normal when true.
            originalRunSpeed = runSpeed; // Original run speed always holds the run speed value.
            runSpeed = 0; // runSpeed will be 0 to start with for the tutorials.
            menuDuringGame = false; // This will make sure the in game menu can't be used until true.
            CheckPointSpawnRoom(); // Starts the CheckPointSpawnRoom Method which will have the player spawn in there first.
        }

        public void MainSoundOn() {
           
            _mainGameSound.Play(); // Plays the main game sound when the sound is turned on.
            soundOnYes.GetComponent<CanvasGroup>().alpha = 0; // Sets the alpha of the sound on button to be 0 so only sound off button is visible.
            soundOnYes.GetComponent<CanvasGroup>().interactable = false; // Sets the ability for the player to not be able to interact with the button once its been pressed.
            soundOnYes.GetComponent<CanvasGroup>().blocksRaycasts = false; // Sets the button to block any raycasts onto it so it can not be accessed.
            
            soundOnNo.GetComponent<CanvasGroup>().alpha = 1; // Sets the alpha of the sound off button to be 1 so the sound on button is not visible.
            soundOnNo.GetComponent<CanvasGroup>().interactable = true; // Sets the ability for the player to be able to interact with the button once its been pressed.
            soundOnNo.GetComponent<CanvasGroup>().blocksRaycasts = true; // Sets the button to not block any raycasts onto it so it can be accessed.

            soundIsWhat.text = ("Sound Is On").ToString(); // Text saying the sound is on in the in game menu.
            soundIsWhat.GetComponent<Text>().color = Color.green; // Gets the color for the text and changes it to green when the sound is on.
        }

        public void MainSoundOff() {
            
            _mainGameSound.Stop();
            soundOnYes.GetComponent<CanvasGroup>().alpha = 1; // Sets the alpha of the sound on button to be 1 so the sound off button is not visible.
            soundOnYes.GetComponent<CanvasGroup>().interactable = true; // Sets the ability for the player to be able to interact with the button once its been pressed.
            soundOnYes.GetComponent<CanvasGroup>().blocksRaycasts = true; // Sets the button to not block any raycasts onto it so it can be accessed.
            
            soundOnNo.GetComponent<CanvasGroup>().alpha = 0; // Sets the alpha of the sound off button to be 0 so only sound on button is visible.
            soundOnNo.GetComponent<CanvasGroup>().interactable = false; // Sets the ability for the player to not be able to interact with the button once its been pressed.
            soundOnNo.GetComponent<CanvasGroup>().blocksRaycasts = false; // Sets the button to block any raycasts onto it so it can not be accessed.
            
            soundIsWhat.text = ("Sound Is Off").ToString(); // Text saying the sound is off in the in game menu.
            soundIsWhat.GetComponent<Text>().color = Color.red; // Gets the color for the text and changes it to red when the sound is off.
        }

        private void TimeSpawnRoom() {
            
            controller.transform.position = timeSpawnPoint.position; // Uses the character controllers position and moves the character to the time spawn point for the time room tutorial to start.
            runSpeed = 0; // Keeps the run speed to 0 so the player can not run around until the trainer is finished talking.
            insideOfTimeRoom = true; // This Bool is to indicate that yes the character is in the Time training Room.
            timeScript.currentTime = 10; // Makes sure the current Time is always at 10 seconds and does not move from 10.
            shouldTeleport = false; // Makes teleporting false so there is no error or bugs.
        }
        
        public void MainStartRoomSpawn() {
           
            inGameMenuStart.SetActive(false); // Sets the In game menu to be turned off when not in use.
            beforeGameStarts = true; // Shows the UI as its the start of the main start room.
            Time.timeScale = 0; // Sets the game to be paused while u have the before game starts UI image.
            spawnThemBootsSirPlease = true; // Allows the boots to spawn as normal in the maze.
            timeScript.currentTime = 10; // Resets the current time to 10 for starting to play the game.
            roomRScript.currentRMessages = 0; // Sets the current rocket room messages to be 0 so the game does not keep the messages at 11.
            insideOfRocketRoom = false; // Says you are no longer in the rocket room.
            SpawnSomeBootsEvent(); // Starts the method that starts spawning boots event so the boots in the maze can start spawning.
        }

        private void CheckPointSpawnRoom() { 
            
            controller.transform.position = checkSpawnPoint.position; // Transforms position of the character to the check point spawn room for training.
            insideOfCheckPointRoom = true; // Says the character is in the check point room which in this case is yes.
        }

        private void RocketBootRoomSpawnPoint() {
            
            checkRoomPoint.currentCMessages = 0; // Resets the check point training room messages to 0.
            timeRoomS.currentTMessages = 0; // Resets the Time training room to be 0 as well.
            insideOfRocketRoom = true; // Says yes the character is in the rocket training room.
            insideOfTimeRoom = false; // Says no the character is not in the time training room.
            runSpeed = 0; // Sets the player run speed to be 0 so the player can not run around.
            controller.transform.position = rocketSpawnPoint.position; // Sends the player to the rocket boot/shoe training room spawn point location.

        }
        private void OnEnable() {
            
            CheckPoint.SaveHereInstead += SavePointUpdated; // Subscribes to the Action Event and listens that the character has gone through a checkpoint.
            ClockObject.addMoreTime += IncreasedTimeSir; // Subscribes to the Action Event and listens that the character has hit a clock on the map and wants more time.
        }
        
        private void OnDisable() {
            
            CheckPoint.SaveHereInstead -= SavePointUpdated; // Stops listening for the player going through the check point.
            ClockObject.addMoreTime -= IncreasedTimeSir; // Stops listening about the player wanting more time.
        }

        private void FixedUpdate() {
            
            if (roomRScript.currentRMessages == 11) { // If the messages in the rocket room hit 11 it will allow the character to move around.
                
                runSpeed = originalRunSpeed;
            }
            
            if (timeRoomS.currentTMessages == 11 && insideOfTimeRoom) { // If the messages in the time room hit 11 it will allow the character to move around.
                
                insideOfCheckPointRoom = false; // This is false to allow the time to count down from 10.
                runSpeed = originalRunSpeed;
            }
            
            if (checkRoomPoint.currentCMessages == 13) { // If the messages in the check point room hit 13 it will allow the character to move around.
                
                runSpeed = originalRunSpeed;
            }
            
            if (testCheck.playerTestingIt) { // Detects when the player hits the check point in the check point room to teleport the character to the next room.
                
                shouldTeleport = true;
            }

            if (shouldTeleport) { // This activates to be true if the player hits the check point int he check point room.
                
                TimeSpawnRoom(); // This will move the player to the Time Training room. 
                testCheck.playerTestingIt = false; // Once the player has hit the check point it will now become false so it does not keep trying to transport the character to the Time room.
                checkRoomPoint.currentCMessages = 0; // Sets the check point messages back to 0.
            }
            
            if (timeScript.currentTime < 5 && insideOfTimeRoom) {  // If the current time is less then 5 and the character is inside of the Time room it will execute the function below.
                
                RocketBootRoomSpawnPoint(); // This will spawn the player into the rocket boot/shoe training room.
                timeScript.currentTime = 10; // Sets the current time back to 10.
                timeRoomS.currentTMessages = 0; // Resets the time room messages back to 0.
            }

            if (!shoesOn! && roomRScript.currentRMessages == 11) { // If the shoes are not on which is false in this case & the rocket room messages equal to 11 it will start the function.
                
                if (quickGetThem < 0.2f) { // If this float is less then 0.2f being that its a timer it will start the function below.
                    MainStartRoomSpawn(); // Spawns into the main game.
                    controller.transform.position = startPosition.position; // Transforms the player to the main game spawn point.
                    quickGetThem = 5; // the timer is reset to a value of 5.
                    
                }
            }
        }

        public void PlayTheGame() {
            
            Time.timeScale = 1; // Sets the game to run at normal speed.
            beforeItStarts.SetActive(false); // sets the before it starts UI image to not show and be inactive.
            timeRoomS.currentTMessages = 11; // Sets the current Time room messages to be 11 as a condition in the Time Script.
            MainSoundOn(); // Sets the main sound to play.
            beforeGameStarts = false; // Sets the bool for the UI image before the main game starts to be false.
            menuDuringGame = true; // Allows the in game menu to open and be used.
        }

        public void ExitInGameMenu() {
            
            menuDuringGame = false; // Sets the in game menu to not show due to a false bool.
            Time.timeScale = 1; // Runs the game at a normal speed.
            inGameMenuStart.SetActive(false); // Has the in game menu UI to be turned off.
            menuDuringGame = true; // Allows the in game menu to be re opened again if pressed ESC.

        }

        private void Update() {
            
            counterText.text = (currentTries + "/" + maxTries).ToString(); // This is for the bug of the counter for the checkpoint tries.
            BootsDisabled(); // Runs the rocket boots/shoes disabled method.
            BootsEnabled(); // Runs the rocket boots/shoes enabled method.
            JumpMovement(); // Allows the jump movement to be used every frame.
            mouseXposition = mouseSensitivity * Input.GetAxis("Mouse X"); // grabs the mouse X axis every frame for the rotation movement.
            moveHorizontal = Input.GetAxis("Horizontal"); // Gets the horizontal movement of the character.
            moveVertical = Input.GetAxis("Vertical"); // Gets the vertical movement of the character.
            Vector3 movement = new Vector3(-moveHorizontal, 0f, -moveVertical); // Allows the character to move forwards and backwards & left & right.
            movement = transform.TransformDirection(movement) * runSpeed; // Gives the character movement speed.
            transform.Rotate(Vector3.up, mouseXposition * 120 * Time.deltaTime); // Gets the mouse input and uses it to rotate the character.
            controller.Move((movement + moveDirection) * Time.deltaTime); // Gets all the movement variables and moves the character.
            
            if (Input.GetKeyDown(KeyCode.Escape) && menuDuringGame) { // If you press the ESC button and if menu during the game is true it will allow the in game menu to be opened.
                
                Time.timeScale = 0; // Sets the game speed to be 0.
                inGameMenuStart.SetActive(true); // Sets the in game menu to show being its now active and true.
            }
            
            if (Time.timeScale == 0 && beforeGameStarts) { // If the speed of the game is 0 and the before game starts UI is allowed to appear it will start the function below.
               
                beforeItStarts.SetActive(true); // The UI for the before game shows so it can educate the player before the main game. 
            }
            if (roomRScript.currentRMessages == 11) { // If the rocket boot/shoe room messages are at 11 then the timer starts counting down below in the function.
                
                quickGetThem -= 0.4f * Time.deltaTime;
            }

            //if (currentTries == maxTries) // This is the bug issue currently with the tries mechanic for the checkpoint and surviving the maze. Does not work currently.
            //{
                //startCount -= 0.3f * Time.deltaTime;
                //if (startCount < 0.2f)
                //{
                    //SceneManager.LoadScene("Maze 1");
                //}
                
            //}
            
            // Calculate the movement vector
            
            // Apply gravity 
            
            if (!controller.isGrounded) { // If the character is in the air the grounded bool will be false and below the function forces the player back down to the ground being the gravity.
                
                moveDirection.y -= characterGravity * Time.deltaTime;
            }
            
            if (timeScript.currentTime < 0.2f) { // If the current time on the clock is less then 0.2f it will start the function below.
                controller.transform.position = newPosition; // This will transform the position of the player to the new position which is got from the last check point.
                relocatedToSpawnPoint = true; // This is true when the player moves back to the check point due to the time in game hitting 0.
            }
            else relocatedToSpawnPoint = false; // If the players time does not hit 0 the character does not go back to spawn being this bool is saying its false due to not relocating.
            
        }
        private void JumpMovement() {
            
            if (Input.GetKeyDown(KeyCode.Space) && (controller.isGrounded)) { // If player hits the space bar and the character is touching the ground it will allow the character to jump.
                
                _jumpSounds.Play(); // Plays the jump sounds for the character.
                moveDirection.y = Mathf.Sqrt(2f * jumpHeight * characterGravity); // Allows the character to jump in game which is calculated based off the jump height and weighed down by gravity in game.
            }
        }
        private void OnTriggerEnter(Collider other) { 
            
            if (!other.CompareTag("RocketBoots")) return; // If the character comes into a collision with any Game Object with the tag Rocket Boots it will start the function below.
            shoesOn = true; // This is saying yes the shoes are on the character.
            _rocketTimeSounds.Play(); // Plays the rocket boots/shoes sound effect.
            currentBootTimer = maxBootTimer; // Starts the timer from the start.
        }
        private void SavePointUpdated(bool newSavePoint) {
            
            if (newSavePoint) { // If new save point bool is yes it will allow the new position variable to have the current transform position when this collision happened.
                
                newPosition = controller.transform.position;
            }
        }
        private void ChangeSpeed() {
            
            runSpeed = maxRunSpeed; // This will activate when using the rocket shoes/boots giving max run speed.
        }

        private void ResetSpeed() {
           
            runSpeed = originalRunSpeed; // When the ride is over inside of the rocket boots/shoes it will go back to the original run speed.
        }

        private void IncreasedTimeSir(bool newSavePoint) {
            
            if (newSavePoint) {
                
                moreTime = true; // If the character hits the clock in game it will allow the character to have more time.
                _timeSounds.Play(); // This will make the hitting the clock sound effects.
            }
            else moreTime = false;
        }
        
        private void BootsEnabled() {
            
            if (!shoesOn) return; // If the shoes are on the character the function starts below.
            bootLeft = GameObject.FindWithTag("LeftRocketShoe"); // This finds the left boot used from the prefab by looking for the tag used by the prefab.
            bootRight = GameObject.FindWithTag("RightRocketShoe"); // This finds the right boot used from the prefab by looking for the tag used by the prefab.
            footLeft = GameObject.Find("RocketShoeLeftLocation").transform.position; // This finds the left foot of the character by name in the Hierarchy and its position. 
            footRight = GameObject.Find("RocketShoeRightLocation").transform.position; // This finds the right foot of the character by name in the Hierarchy and its position.
            ChangeSpeed(); // Changes the speed of the characters run speed.
            slideTimer.GetComponent<Slider>().value = SliderCountDown(); // this starts the timer with its variables.
            transform.Rotate(Vector3.up, mouseXposition * 120 * Time.deltaTime); // Gets the tracking of the mouse so the boots/shoes turn with the mouse rotation.
            bootLeft.transform.position = footLeft; // puts the left boot position onto the left foot while using the boot.
            bootRight.transform.position = footRight; // puts the right boot position onto the right foot while using the boot.
            currentBootTimer -= 0.8f * Time.deltaTime; // Starts the timer for the boots.
            AppearingSlider(); // Brings up the slider in view in the UI
        }

        private void BootsDisabled()
        {
            if (!(currentBootTimer < 0.1f) || !shoesOn) return; // If the timer has finished for using the boots in game.
            ResetSpeed(); // Changes the speed back to the original speed.
            shoesOn = false; // Shoes are no longer on so it is false.
            BootsWillBeDestroyedEvent(); // This will start the event to be sent out to destroy the boots.
            slideTimer.GetComponent<CanvasGroup>().alpha = 0; // Makes the slider invisible in the UI.
            currentBootTimer = finishedBootTimer; // Makes the current boot timer equal to the finished boot timer so does not reset.
        }

        private float SliderCountDown() {
            
            return (currentBootTimer / maxBootTimer); // Gives the variables of the slider of the current and max boot timer of the slider.
        }
        private void AppearingSlider() {
            
            slideTimer.GetComponent<CanvasGroup>().alpha = 1; // Makes the slider appear in the UI.
        }

        private void BootsWillBeDestroyedEvent() {
            
            DestroyTheBootsEvent?.Invoke(shoesOn); // Sent out an Action Event to the rocket boot script to destroy them.
        }

        private void SpawnSomeBootsEvent() {
            
            StartSpawningThemBoots?.Invoke(spawnThemBootsSirPlease); // Sends out an Action Event to spawn more boots into the maze.
        }
    } 
}

