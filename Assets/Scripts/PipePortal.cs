using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePortal : MonoBehaviour
{
    [SerializeField]
    private GameObject PortalLinked;
    [SerializeField]
    private GameObject FkeyPrompt; //UI element to show the player can interact
    private bool playerInTrigger = false;
    private Collider2D playerCollider;
    public PlayerSwitch playerSwitchScript; // Reference to the AttachController script
    public bool isAttached => playerSwitchScript.isAttached; // Property to check if the crow is attached to the fox
    public AudioManager audioManager;


    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        FkeyPrompt.SetActive(false);
        if (PortalLinked == null)
        {
            Debug.LogError("PortalLinked is not assigned in PipePortal script on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F) && playerCollider != null && !isAttached) //If player is in the interact space and presses F
        {
            playerCollider.transform.position = PortalLinked.transform.position;
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.pipe);
            }
        }
        else if (playerInTrigger && Input.GetKeyDown(KeyCode.F) && playerCollider != null && isAttached)
        {
            Debug.Log("Cannot teleport while the crow is attached to the fox.");
        }
    }

    private void OnTriggerStay2D(Collider2D other) //OnStay move
    {
        if (other.CompareTag("Player")) //If player is in the interact space
        {
            FkeyPrompt.SetActive(true); //Show the prompt
            playerInTrigger = true; //Set the flag to true
            playerCollider = other; //Store the player's collider for later use
        }
    }

    private void OnTriggerExit2D(Collider2D other) //OnExit hide prompt
    {
        if (other.CompareTag("Player"))
        {
            FkeyPrompt.SetActive(false); //Hide the prompt
            playerInTrigger = false; //Set the flag to false
            playerCollider = null; //Clear the player's collider
        }
    }

    
}
