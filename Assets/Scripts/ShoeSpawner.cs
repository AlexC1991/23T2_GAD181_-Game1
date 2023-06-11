using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class ShoeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bootsToSpawn; // Prefab of the GameObject that is spawned into the maze which in this case is the rocket boots/shoes.
        [SerializeField] private GameObject[] spawnList; // This is an Array for all the spawn locations in the map that i placed where the boots/shoes could spawn in. 

        private float maxTimer = 10f; // Max time for how long the player has to wait for another rocket boot/shoe prefab spawns.
        private float currentTime; // This is the current time and holds the value to whatever the timer is at currently at.
        private float spawnTime = 0f; // When the timer gets to 0 it will equal the spawn time and spawn the boots/shoes.
        private bool sBoots; // This bool keeps the boots from spawning if the character is in the training rooms still.

        private void Awake()
        {
            spawnList = GameObject.FindGameObjectsWithTag("SpawnPoint"); // Grabs all the rocket boot/shoe spawn locations that are tagged with SpawnPoint and puts them in the Array.
        }

        private void Start()
        {
            currentTime = maxTimer; // Current Time will always start off being equal to the max time.
            sBoots = false;
        }

        private void OnEnable()
        {
            RocketRoomTrainer._CharactersBootsNeedSpawning += TrainerNeedsTheirBootsCalled;
        }

        private void OnDisable()
        {
            RocketRoomTrainer._CharactersBootsNeedSpawning -= TrainerNeedsTheirBootsCalled;
        }

        private void Update()
        {
            int secretArea = UnityEngine.Random.Range(0, spawnList.Length); // A temporary int is to be used to hold the random range value that it spits out when going through all the spawn locations in the Array using the Array name .Length.  
            
            if (currentTime < 0.2f && sBoots) // If current time is less then 0.2f and sBoots bool is true then it will allow the Instantiate to proceed and spawn the prefab.
            {
                Instantiate(bootsToSpawn, spawnList[secretArea].transform.position, Quaternion.identity); // Spawns the prefab using the spawnList Array with secretArea being the choice in Array as to which spawn point.
                currentTime = maxTimer; // Current timer will then re equal the max timer to reset.
            }
            currentTime -= 0.8f * Time.deltaTime; // Current timer is minuses over a 0.8f per frame using Time.delta time.
        }

        private void TrainerNeedsTheirBootsCalled(bool canRelocateToMaze)
        {
            if (canRelocateToMaze) sBoots = true;
        }
    }
    
   
}
