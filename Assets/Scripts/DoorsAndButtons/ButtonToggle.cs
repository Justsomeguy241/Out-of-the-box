using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ButtonToggle : MonoBehaviour
{
    [SerializeField]
    private int buttonTID;
    [SerializeField]
    private DoorToggle door;
    [SerializeField]
    public Sprite LeverOnSprite;
    [SerializeField]
    public Sprite LeverOffSprite;
    public UnityEvent LeverOn;
    public UnityEvent LeverOff;
    public bool isOpen = false;
    void Start()
    {
        DoorToggle[] doors = FindObjectsOfType<DoorToggle>();
        foreach (DoorToggle d in doors)
        {
            if (d.doorTID == buttonTID)
            {
                door = d;
                break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crow") && door != null || collision.CompareTag("Player") && door != null)
        {
            if (isOpen == false)
            {
                GetComponent<SpriteRenderer>().sprite = LeverOnSprite;

                LeverOn?.Invoke();
                isOpen = true;
                door.isOpen = true;
            }
            else if (isOpen == true)
            {
                GetComponent<SpriteRenderer>().sprite = LeverOffSprite;
                LeverOff?.Invoke();
                isOpen = false;
                door.isOpen = false;
            }

        }
    }
}
