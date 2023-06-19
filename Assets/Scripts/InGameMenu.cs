using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private SoundPlusMusicManager soundManager;
        [Header("UI Elements")]
        [SerializeField] private GameObject inGameMenuStart; // Pulls up the in game menu UI.
        [SerializeField] private GameObject soundOnYes; // This shows the button for sounds on.
        [SerializeField] private GameObject soundOnNo; // this shows the button for sounds off.
        [SerializeField] private Text soundIsWhat; // This text shows on the screen that the sound is currently doing if it be on or off.
        private bool _menuDuringGame;
        private bool playSomeMusic;
        [HideInInspector] public bool canSeeMouse;
        [HideInInspector] public bool canLockMouse;

        private void OnEnable()
        {
            CharacterMovement.UsingInGameMenuEvent += InMainGameNow;
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape) && _menuDuringGame)
            {
                OpenInGameMenu();
            }
        }
        private void OpenInGameMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
                
            Time.timeScale = 0;
            _menuDuringGame = true; // Sets the in game menu to not show due to a false bool.
            inGameMenuStart.SetActive(true); // Has the in game menu UI to be turned off.
        }
        public void MainSoundOn()
        {
            soundManager._playMainMusic = true;
            soundOnYes.GetComponent<CanvasGroup>().alpha = 0; // Sets the alpha of the sound on button to be 0 so only sound off button is visible.
            soundOnYes.GetComponent<CanvasGroup>().interactable = false; // Sets the ability for the player to not be able to interact with the button once its been pressed.
            soundOnYes.GetComponent<Button>().interactable = false;
            soundOnYes.GetComponent<CanvasGroup>().blocksRaycasts = false; // Sets the button to block any raycasts onto it so it can not be accessed.
            soundOnNo.GetComponent<CanvasGroup>().alpha = 1; // Sets the alpha of the sound off button to be 1 so the sound on button is not visible.
            soundOnNo.GetComponent<CanvasGroup>().interactable = true; // Sets the ability for the player to be able to interact with the button once its been pressed.
            soundOnNo.GetComponent<Button>().interactable = true;
            soundOnNo.GetComponent<CanvasGroup>().blocksRaycasts = true; // Sets the button to not block any raycasts onto it so it can be accessed.
            soundIsWhat.text = ("Sound Is On"); // Text saying the sound is on in the in game menu.
            soundIsWhat.GetComponent<Text>().color = Color.green; // Gets the color for the text and changes it to green when the sound is on.
        }
        public void MainSoundOff()
        {
            soundManager.mainGameSound.Stop();
            soundOnYes.GetComponent<CanvasGroup>().alpha = 1; // Sets the alpha of the sound on button to be 1 so the sound off button is not visible.
            soundOnYes.GetComponent<CanvasGroup>().interactable = true; // Sets the ability for the player to be able to interact with the button once its been pressed.
            soundOnYes.GetComponent<Button>().interactable = true;
            soundOnYes.GetComponent<CanvasGroup>().blocksRaycasts = true; // Sets the button to not block any raycasts onto it so it can be accessed.
            soundOnNo.GetComponent<CanvasGroup>().alpha = 0; // Sets the alpha of the sound off button to be 0 so only sound on button is visible.
            soundOnNo.GetComponent<CanvasGroup>().interactable = false; // Sets the ability for the player to not be able to interact with the button once its been pressed.
            soundOnNo.GetComponent<Button>().interactable = false;
            soundOnNo.GetComponent<CanvasGroup>().blocksRaycasts = false; // Sets the button to block any raycasts onto it so it can not be accessed.
            soundIsWhat.text = ("Sound Is Off"); // Text saying the sound is off in the in game menu.
            soundIsWhat.GetComponent<Text>().color = Color.red; // Gets the color for the text and changes it to red when the sound is off.
        }
        public void ExitInGameMenu() {
            
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            
            _menuDuringGame = false; // Sets the in game menu to not show due to a false bool.
            Time.timeScale = 1; // Runs the game at a normal speed.
            inGameMenuStart.SetActive(false); // Has the in game menu UI to be turned off.
            _menuDuringGame = true; // Allows the in game menu to be re opened again if pressed ESC.
        }

        public void ResetGame()
        {
            SceneManager.LoadScene("Maze 1");
        }
        private void InMainGameNow(bool canUseInGameMenu)
        {
            if (canUseInGameMenu) _menuDuringGame = true;
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("StartMenu");
        }
        private void OnDisable()
        {
            CharacterMovement.UsingInGameMenuEvent -= InMainGameNow;
        }
    }
}
