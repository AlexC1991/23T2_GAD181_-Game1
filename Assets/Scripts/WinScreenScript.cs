using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitGameApplication()
    {
        Application.Quit();
    }
}
