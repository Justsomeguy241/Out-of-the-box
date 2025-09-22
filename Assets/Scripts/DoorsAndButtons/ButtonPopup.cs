using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPopup : MonoBehaviour
{
    [SerializeField] public int buttonPopupID;
    public DoorPopup doorPopup;
    [SerializeField]
    public Sprite LeverOn;
    [SerializeField]
    public Sprite LeverOff;
    public AudioManager audioManager;
    private bool audioPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        DoorPopup[] doors = FindObjectsOfType<DoorPopup>();
        foreach (DoorPopup d in doors)
        {
            if (d.doorPopupID == buttonPopupID)
            {
                doorPopup = d;
                break;
            }
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Crow"))
        {
            doorPopup.ToggleDoor(); //buka pintu
            if (audioManager != null && !audioPlaying)
            {
                audioManager.PlaySFX(audioManager.Lever);
                audioPlaying = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (doorPopup.doorPopupID == buttonPopupID && doorPopup.Appear == true)
        {
            // Change the button color to indicate it's pressed
            GetComponent<SpriteRenderer>().sprite = LeverOn;
        }
        else
        {
            // Reset the button color to its original state
            GetComponent<SpriteRenderer>().sprite = LeverOff;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Crow"))
        {
            if (audioManager != null && audioPlaying)
            {
                audioPlaying = false;
            }
        }
    }
}
