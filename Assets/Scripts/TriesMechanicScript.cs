using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TriesMechanicScript : MonoBehaviour
    {
        [Header("Scripts")] 
        [SerializeField] private CharacterMovement cMove;
        private int _currentTries;
        private readonly int _maxTries = 5;

        [Header("UI Elements")] 
        [SerializeField] private Text counterText;
        [SerializeField] private GameObject _maxTimesplayerCanPlayScreen;
        private void Start()
        {
            _maxTimesplayerCanPlayScreen.SetActive(false);
            _currentTries = 0;
        }
        private void Update()
        {
            TriesMechanicStart();
            counterText.text = (_currentTries + "/" + _maxTries);
        }
        private void TriesMechanicStart()
        {
            if (cMove.tryAgain)
            {
                _currentTries += 1;
                cMove.tryAgain = false;
            }

            if (_currentTries == _maxTries)
            {
                _maxTimesplayerCanPlayScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
