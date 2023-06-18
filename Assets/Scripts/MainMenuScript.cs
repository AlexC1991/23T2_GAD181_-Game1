using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
    public class MainMenuScript : MonoBehaviour
    {
        [SerializeField] private AudioSource mainMenuSound;

        private void Start()
        {
            mainMenuSound.Play();
        }

        public void QuitGame() // Public Method to be called by a button in Unity.
        {
            Application.Quit(); // Quits the application when called.
        }
        
        public void FirstTutorialPlease() // Public Method to be called by a button in Unity.
        {
            SceneManager.LoadScene("BeforeMaze1"); // Loads the scene called BeforeMaze1.
        }

    }
}
