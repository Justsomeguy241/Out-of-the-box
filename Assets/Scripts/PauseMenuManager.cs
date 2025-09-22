using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject transparentBackground;
    public GameObject MainMenuBackground;
    private bool escapeKeyPressed = false;
    public bool isPaused = false;
    // Start is called before the first frame update
    public AudioManager audioManager;
    void Start()
    {
        MainMenuBackground.SetActive(false);
        transparentBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escapeKeyPressed)
        {
            
            escapeKeyPressed = true;
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escapeKeyPressed = false;
        }
    }

    public void PauseGame()
    {
        transparentBackground.SetActive(true);
        // audioManager.PlaySFX(audioManager.Pause);
        Time.timeScale = 0f;
        // MainMenuPanel.SetActive(true);
        MainMenuBackground.SetActive(true);
        isPaused = true;
        audioManager.PlaySFX(audioManager.Pause);
    }

    public void ResumeGame()
    {
        transparentBackground.SetActive(false);
        // audioManager.PlaySFX(audioManager.ClickOnPause);
        Time.timeScale = 1f;
        // MainMenuPanel.SetActive(false);
        MainMenuBackground.SetActive(false);
        isPaused = false;
        audioManager.PlaySFX(audioManager.MouseClick);
    }
    
    public void BackToMainMenu()
    {
        audioManager.PlaySFX(audioManager.MouseClick);
        SceneManager.LoadScene("MainMenu");
        // audioManager.PlaySFX(audioManager.ClickOnPause);
        Time.timeScale = 1f;
    }
}
