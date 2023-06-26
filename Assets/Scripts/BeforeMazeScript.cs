using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyNamespace
{
    public class BeforeMazeScript : MonoBehaviour
    {
        public void LetsContinueToGame()
        {
            SceneManager.LoadScene("Maze 1");
        }
    }
}
