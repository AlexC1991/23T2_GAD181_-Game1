using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TriesMechanicScript : MonoBehaviour
    {
        private float startCount = 1;
        private float originalStartCount;
        
        private int currentTries;
        private int originalTries;
        private int maxTries = 5;
       
        private bool resetOnMap;

        [Header("Tries Mechanic Counter Text")] 
        [SerializeField] private Text counterText;
        

        private void Start()
        {
            originalTries = currentTries;
            originalStartCount = startCount;
            resetOnMap = false;
        }

        private void Update()
        {
            TriesMechanicStart();
            counterText.text = (currentTries + "/" + maxTries).ToString();
        }


        private void TriesMechanicStart()
        {
            if (currentTries == maxTries)
            {
                startCount -= 0.3f * Time.deltaTime;
                
                if (startCount < 0.2f)
                {
                    resetOnMap = true;
                    resetOnMap = false;
                    currentTries = originalTries;
                    startCount = originalStartCount;
                }
            }
            
        }
    }
}
