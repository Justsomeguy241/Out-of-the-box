using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorToggle : MonoBehaviour
{
    [Header("Door Toggle")]
    [SerializeField] private int DoorTID;
    [SerializeField] private Rigidbody2D Rigidbody2D;
    public int doorTID => DoorTID;
    private ButtonToggle buttonToggle;
    [Header("Door Position")]
    [SerializeField]
    public Vector3 initialPos;
    [SerializeField]
    private Vector3 desiredPos;
    [SerializeField]
    public Vector3 offsetPos; // Example offset for open position
    [Header("Door Speed")]
    public float speed = 2f;
    [Header("Door State")]
    public bool isOpen = false;
    public bool isSideWay;
    public AudioManager audioManager;
    private bool audioPlaying = false;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        initialPos = transform.localPosition;
       
        // closedposToggle = transform.position;
        // openPosToggle = closedposToggle + new Vector3(0, 3f, 0); // Example offset for open position
    }

    void Update()
    {
        Vector3 targetPosition = initialPos + desiredPos;
        
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);

        // Check if the door is moving
        if (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            isOpen = true;
            if (!audioPlaying) // A bool check to stop triggering the audio multiple time
            {
                audioManager.playMyAudio(audioManager.DoorOpen);
                audioPlaying = true;
            }
            
        }
        else
        {
            isOpen = false;
            if (audioPlaying)
            {
                audioManager.StopSFX(audioManager.DoorOpen);
                audioPlaying = false;
            }
            
        }
    }
    [ContextMenu("OpenDoor")]
    public void OpenDoor()
    {
        if (audioManager != null)
        {  
            audioManager.PlaySFX(audioManager.Lever);
        }
        Debug.Log("Open Door");
        desiredPos = offsetPos;
    }
    [ContextMenu("CloseDoor")]
    public void CloseDoor()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.Lever);
        }
        Debug.Log("Close Door");
        desiredPos = Vector3.zero;
    }
}

