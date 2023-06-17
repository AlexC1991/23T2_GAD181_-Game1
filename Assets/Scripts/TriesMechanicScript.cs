using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TriesMechanicScript : MonoBehaviour
    {
        private float _startCount = 1;
        private float _originalStartCount;
        
        private int _currentTries;
        private int _originalTries;
        private readonly int _maxTries = 5;

        [Header("Tries Mechanic Counter Text")] 
        [SerializeField] private Text counterText;
        private void Start()
        {
            _originalTries = _currentTries;
            _originalStartCount = _startCount;
        }
        private void Update()
        {
            TriesMechanicStart();
            counterText.text = (_currentTries + "/" + _maxTries);
        }
        private void TriesMechanicStart()
        {
            if (_currentTries == _maxTries)
            {
                _startCount -= 0.3f * Time.deltaTime;
                
                if (_startCount < 0.2f)
                {
                    _currentTries = _originalTries;
                    _startCount = _originalStartCount;
                }
            }
            
        }
    }
}
