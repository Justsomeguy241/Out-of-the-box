using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHold : MonoBehaviour
{
    [Header("Door Hold ID")]
    [SerializeField]
    private int doorHID;
    public int DoorHID => doorHID;
    public ButtonHold buttonHold;
    [Header("Door Position")]
    [SerializeField]
    public Vector3 InitialPos;
    [SerializeField]
    private Vector3 DesiredPos;
    [SerializeField]
    public Vector3 offsetPosHold; // Example offset for open position
    [SerializeField]
    private Vector3 targetposition;
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
        InitialPos = transform.localPosition;
        ButtonHold[] buttons = FindObjectsOfType<ButtonHold>();
        foreach (ButtonHold b in buttons)
        {
            if (b.buttonHID == doorHID)
            {
                buttonHold = b;
                break;
            }
        }
    }

    void Update()
    {
        targetposition = InitialPos + DesiredPos;
        if (buttonHold.isHolding == true)
        {
            DesiredPos = offsetPosHold; // Set the desired position to the offset when holding
            OpenDoorHold();
            if (!audioPlaying) // A bool check to stop triggering the audio multiple time
            {
                audioManager.playMyAudio(audioManager.DoorOpen);
                audioPlaying = true;
            }
        }
        else
        {
            DesiredPos = Vector3.zero; // Reset the desired position when not holding
            targetposition = InitialPos; // Reset target position to initial position
            CloseDoorHold();
            if (audioPlaying)
            {
                audioManager.StopSFX(audioManager.DoorOpen);
                audioPlaying = false;
            }
        }
    }

    public void OpenDoorHold()
    {
        if(buttonHold.isHolding == true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetposition, speed * Time.deltaTime);
        }
    }
    public void CloseDoorHold()
    {
        if(buttonHold.isHolding == false)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetposition, speed * Time.deltaTime);
        }
    }
}
