using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHoldPopup : MonoBehaviour
{
    [SerializeField]
    private int ButtonID;
    public int buttonID => ButtonID;

    [SerializeField]
    private DoorPopup doorPopup;

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
        DoorPopup[] doors = FindObjectsOfType<DoorPopup>();
        foreach (DoorPopup d in doors)
        {
            if (d.doorPopupID == ButtonID)
            {
                doorPopup = d;
                break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("Pushable")) && doorPopup != null)
        {
            if (!isHolding)
            {
                isHolding = true;
                doorPopup.Appear = true;
                GetComponent<SpriteRenderer>().sprite = ButtonPressed;
                ButtonOn?.Invoke();

                if (audioManager != null && !audioPlaying)
                {
                    audioManager.PlaySFX(audioManager.Buttton);
                    audioPlaying = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("Pushable")) && doorPopup != null)
        {
            isHolding = false;
            doorPopup.Appear = false;
            GetComponent<SpriteRenderer>().sprite = ButtonUnpressed;
            ButtonOff?.Invoke();

            if (audioManager != null && audioPlaying)
            {
                audioPlaying = false;
            }
        }
    }
}
