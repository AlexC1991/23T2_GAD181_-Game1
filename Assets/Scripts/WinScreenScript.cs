using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitGameApplication()
    {
        Application.Quit();
    }
}
