using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f; // Ensure the game is unpaused when loading a new scene
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; // Ensure the game is unpaused when returning to the main menu
    }
}
