using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorScript : MonoBehaviour
{
    public GameObject LevelCompleteUI;
    [SerializeField] private GameObject EKeyPrompt; // UI element to show the player can interact
    private bool playerInTrigger = false; // Flag to check if the player is in the trigger area
    private Collider2D playerCollider; // Store the player's collider for later use

    public PlayerSwitch playerSwitchScript; // Reference to the AttachController script
    public bool isAttached => playerSwitchScript.isAttached; // Property to check if the crow is attached to the fox
    public AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        LevelCompleteUI.SetActive(false); // Hide the level complete UI initially
        EKeyPrompt.SetActive(false); // Hide the prompt initially
        if (EKeyPrompt == null)
        {
            Debug.LogError("EKeyPrompt is not assigned in ExitDoorScript on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && playerCollider != null && isAttached) // If player is in the interact space and presses E
        {
            Debug.Log("Player has exited through the door.");
            // Here you can add logic to handle what happens when the player exits through the door
            // For example, loading a new scene or showing a message
            LevelCompleteUI.SetActive(true); // Show the level complete UI
            Time.timeScale = 0f; // Pause the game
            EKeyPrompt.SetActive(false); // Hide the prompt after interaction
            audioManager.MusicSource.GetComponent<AudioSource>().Stop(); // Stop the main theme
            audioManager.SFXSource.PlayOneShot(audioManager.Victory, 15f); // Play the victory sound
        }
        else if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && playerCollider != null && !isAttached)
        {
            Debug.Log("Cannot exit while the crow is not attached to the fox.");
        }
    }

    private void OnTriggerStay2D(Collider2D other) // OnStay move
    {
        if (other.CompareTag("Player")) // If player is in the interact space
        {
            EKeyPrompt.SetActive(true); // Show the prompt
            playerInTrigger = true; // Set the flag to true
            playerCollider = other; // Store the player's collider for later use
        }
    }
    private void OnTriggerExit2D(Collider2D other) // OnExit hide prompt
    {
        if (other.CompareTag("Player")) // If player exits the interact space
        {
            EKeyPrompt.SetActive(false); // Hide the prompt
            playerInTrigger = false; // Set the flag to false
            playerCollider = null; // Clear the player's collider
        }
    }
}
