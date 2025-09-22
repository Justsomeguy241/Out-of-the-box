using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPopup : MonoBehaviour
{
    [Header("Door Popup")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer;
    [SerializeField] private Collider2D doorCollider;


    [Header("Door Toggle")]
    [SerializeField] public int doorPopupID;
    
    [Header("Door State")]
    public bool Appear = false;

    void Start()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Appear)
        {
            //Debug.Log("Door appeared");
            doorSpriteRenderer.enabled = true;
            doorCollider.enabled = true;
        }
        else
        {
            //Debug.Log("Door disappeared");
            doorSpriteRenderer.enabled = false;
            doorCollider.enabled = false;
        }
    }

    public void ToggleDoor()
    {
        Debug.Log("Toggle Door");
        if(!Appear)
        {
            Appear = true;
        }
        else
        {
            Appear = false;
        }
    }
}
