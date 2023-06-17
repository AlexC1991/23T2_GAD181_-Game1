using UnityEngine;

namespace AlexzanderCowell
{
    public class ShoeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bootsToSpawn; // Prefab of the GameObject that is spawned into the maze which in this case is the rocket boots/shoes.
        [SerializeField] private GameObject[] spawnList; // This is an Array for all the spawn locations in the map that i placed where the boots/shoes could spawn in. 

        private readonly float _maxTimer = 10f; // Max time for how long the player has to wait for another rocket boot/shoe prefab spawns.
        private float _currentTime; // This is the current time and holds the value to whatever the timer is at currently at.
        private bool _sBoots; // This bool keeps the boots from spawning if the character is in the training rooms still.

        private void Awake()
        {
            spawnList = GameObject.FindGameObjectsWithTag("SpawnPoint"); // Grabs all the rocket boot/shoe spawn locations that are tagged with SpawnPoint and puts them in the Array.
        }
<<<<<<< Updated upstream

        private void Start()
        {
            currentTime = maxTimer; // Current Time will always start off being equal to the max time.
=======
        private void OnEnable()
        {
            CharacterMovement.ResetRCurrentMessageEvent += TrainerNeedsTheirBootsCalled;
        }
        private void Start()
        {
            _currentTime = _maxTimer; // Current Time will always start off being equal to the max time.
            _sBoots = false;
>>>>>>> Stashed changes
        }
        private void Update()
        {
            Debug.Log("Spawn Boot Bool " + _sBoots);
            int secretArea = Random.Range(0, spawnList.Length); // A temporary int is to be used to hold the random range value that it spits out when going through all the spawn locations in the Array using the Array name .Length.  
            
            if (_currentTime < 0.2f && _sBoots) // If current time is less then 0.2f and sBoots bool is true then it will allow the Instantiate to proceed and spawn the prefab.
            {
                Instantiate(bootsToSpawn, spawnList[secretArea].transform.position, Quaternion.identity); // Spawns the prefab using the spawnList Array with secretArea being the choice in Array as to which spawn point.
<<<<<<< Updated upstream
                
                currentTime = maxTimer; // Current timer will then re equal the max timer to reset.
            }
            currentTime -= 0.8f * Time.deltaTime; // Current timer is minuses over a 0.8f per frame using Time.delta time.
        } 

        private void OnEnable() // Start of the Action Event.
        {
            CharacterMovement.StartSpawningThemBoots += GetSetAndSpawn; // Listens, waits and picks up the Action Event Character Movement script sent out. This is what tells sBoots to be true or false for spawning the rocket boots/shoes.
        }

        private void OnDisable() // End of the Action Event.
        {
            CharacterMovement.StartSpawningThemBoots -= GetSetAndSpawn; // Stops listening to the Action Event Character Movement script sent out.
        }

        private void GetSetAndSpawn(bool spawnThemBootsSirPlease) // Uses the bool sent out via the Action Event.
        {
            if (spawnThemBootsSirPlease) // If this is true then says sBoots should be true too.
            {
                sBoots = true; // sBoots is now true due to spawnThemBootsSirPlease is true as well.
            }
            else
            {
                sBoots = false; // If spawnThemBootsSirPlease is not true and is false then sBoots are also false.
            }
            
=======
                _currentTime = _maxTimer; // Current timer will then re equal the max timer to reset.
            }
            _currentTime -= 0.8f * Time.deltaTime; // Current timer is minuses over a 0.8f per frame using Time.delta time.
        }
        private void TrainerNeedsTheirBootsCalled(bool _didRelocateToMazeRoom)
        {
            if (_didRelocateToMazeRoom) _sBoots = true;
        }
        private void OnDisable()
        {
            CharacterMovement.ResetRCurrentMessageEvent -= TrainerNeedsTheirBootsCalled;
>>>>>>> Stashed changes
        }
    }
    
   
}
