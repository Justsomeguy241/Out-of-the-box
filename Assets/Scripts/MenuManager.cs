using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject levels;

    private GameObject[] menus;
    public AudioManager audioManager;

    private void Start()
    {
        ShowOnly(mainMenu);
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    private void Awake()
    {
        // Initialize the array of all menu objects
        menus = new GameObject[] { mainMenu, settings, levels };
    }

    // Method to show only one menu and hide all others
    private void ShowOnly(GameObject menuToShow)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(menu == menuToShow);
        }
    }

    // Public methods for specific menus
    public void OpenSettings()
    {
        ShowOnly(settings);
        audioManager.PlaySFX(audioManager.MouseClick);   
    }
    public void OpenMainMenu()
    {
        ShowOnly(mainMenu);
        audioManager.PlaySFX(audioManager.MouseClick); 
    }
    public void OpenLevels()
    {
        ShowOnly(levels);
        Time.timeScale = 1f;
        audioManager.PlaySFX(audioManager.MouseClick); 
    }

    public void Quit()
    {
        Application.Quit();
        audioManager.PlaySFX(audioManager.MouseClick); 
    }
}