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
        private void OnEnable()
        {
            CharacterMovement.ResetRCurrentMessageEvent += TrainerNeedsTheirBootsCalled;
        }
        private void Start()
        {
            _currentTime = _maxTimer; // Current Time will always start off being equal to the max time.
            _sBoots = false;
        }
        private void Update()
        {
            int secretArea = Random.Range(0, spawnList.Length); // A temporary int is to be used to hold the random range value that it spits out when going through all the spawn locations in the Array using the Array name .Length.  
            
            if (_currentTime < 0.2f && _sBoots) // If current time is less then 0.2f and sBoots bool is true then it will allow the Instantiate to proceed and spawn the prefab.
            {
                Instantiate(bootsToSpawn, spawnList[secretArea].transform.position, Quaternion.identity); // Spawns the prefab using the spawnList Array with secretArea being the choice in Array as to which spawn point.

                _currentTime = _maxTimer; // Current timer will then re equal the max timer to reset.
            }
            _currentTime -= 0.8f * Time.deltaTime; // Current timer is minuses over a 0.8f per frame using Time.delta time.
        }
        private void TrainerNeedsTheirBootsCalled(bool didRelocateToMazeRoom)
        {
            if (didRelocateToMazeRoom) _sBoots = true;
        }
        private void OnDisable()
        {
            CharacterMovement.ResetRCurrentMessageEvent -= TrainerNeedsTheirBootsCalled;
        }
    }
    
   
}
