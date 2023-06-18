using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
    public class MainMenuScript : MonoBehaviour
    {
        public void StartGame() // Public Method to be called by a button in Unity.
        {
            SceneManager.LoadScene("Maze 1"); // Loads the scene called Maze 1.
        }

        public void QuitGame() // Public Method to be called by a button in Unity.
        {
            Application.Quit(); // Quits the application when called.
        }
        
        public void FirstTutorialPlease() // Public Method to be called by a button in Unity.
        {
            SceneManager.LoadScene("BeforeMaze1"); // Loads the scene called BeforeMaze1.
        }

        public void MainMenu() // Public Method to be called by a button in Unity.
        {
            SceneManager.LoadScene("StartMenu"); // Loads the scene called StartMenu.
        }
    }
}
