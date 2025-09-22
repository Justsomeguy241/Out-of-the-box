using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHold : MonoBehaviour
{
    [SerializeField]
    private int ButtonHID;
    public int buttonHID => ButtonHID;
    [SerializeField]
    private DoorHold doorHold;
    [SerializeField]
    public bool isHolding = false;

    public Sprite ButtonUnpressed;
    public Sprite ButtonPressed;
    public UnityEvent ButtonOn;
    public UnityEvent ButtonOff;
    public AudioManager audioManager;
    private bool audioPlaying = false;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        DoorHold[] doors = FindObjectsOfType<DoorHold>();
        foreach (DoorHold d in doors)
        {
            if (d.DoorHID == ButtonHID)
            {
                doorHold = d;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // stop putting everything in update >_>
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable") && doorHold != null)
        {

            if (audioManager != null && !audioPlaying)
            {
                audioManager.PlaySFX(audioManager.Buttton);
                audioPlaying = true;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable") && doorHold != null)
        {
            isHolding = true;
            doorHold.isOpen = true;
            GetComponent<SpriteRenderer>().sprite = ButtonPressed;
            //if (audioManager != null)
            //{
            //    audioManager.PlaySFX(audioManager.Buttton);
            //}
            ButtonOn?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable"))
        {
            if (audioManager != null && audioPlaying)
            {
                audioPlaying = false;
            }
            // if (audioManager != null)
            // {
            //     audioManager.PlaySFX(audioManager.Buttton);
            // }
            // if(doorHold.DoorHID == ButtonHID)
            // {
            //     Debug.Log("Player is not holding the button");
            //     isHolding = false;
            //     GetComponent<SpriteRenderer>().sprite = ButtonUnpressed;
            // }
            isHolding = false;
            doorHold.isOpen = false;
            GetComponent<SpriteRenderer>().sprite = ButtonUnpressed;
            ButtonOff?.Invoke();
        }
    }

}
