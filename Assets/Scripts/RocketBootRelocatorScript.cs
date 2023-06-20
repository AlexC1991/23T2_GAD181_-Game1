using UnityEngine;

namespace AlexzanderCowell
{
    public class RocketBootRelocatorScript : MonoBehaviour
    {
        private GameObject _gameManag;
        private void Start()
        {
            _gameManag = GameObject.FindWithTag("GameManager");
        }

        private void Update()
        {
            GetComponent<ParticleSystem>().Play(); // Once this GameObject is spawned in it will play the particle system I have made for the Prefab.
            
            if (_gameManag.GetComponent<RocketBootController>().currentBootTimer < 0.2f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
