using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ButtonPressurePlate : MonoBehaviour
{
    private bool pressed = false; //toggle for plate
    public GameObject Sprite; //animation sprite
    private Vector3 originalPos; //saves starting position
    public GameObject door; //GmObj door
    private Vector3 doorOriginalPos; //saves starting position door
    private Doorvariables DV; //bugfixes: asked for door toggle bool

    private void Start() //saves position, yes this consumes memory
    {
        originalPos = Sprite.transform.position;
        doorOriginalPos = door.transform.position;
        DV = door.GetComponent<Doorvariables>(); //I'm too lazy to make a new public
    }

    private void OnTriggerEnter2D(Collider2D collision) //OnEnter we change the sprite loc and toggles
    {
        if (collision.tag == "Player" || collision.tag == "Pushable")
        {
            Debug.Log("touched");
            pressed = true;
            Sprite.transform.position = new Vector3(originalPos.x, originalPos.y - 0.3f, originalPos.z);
            DV.isOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //OnExit we change the sprite loc and toggles
    {
        if (collision.tag == "Player" || collision.tag == "Pushable")
        {
            Debug.Log("untouched");
            pressed = false;
            Sprite.transform.position = originalPos;
            DV.isOpen = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //OnStay we move the door
    {
        if (collision.tag == "Player" || collision.tag == "Pushable")
        {
            opendoor();
        }
    }

    private void opendoor() //this opens the door, yAxis can be changed for speed
    {
        if (door.transform.position.y > doorOriginalPos.y - 4) //stop it from running away
        {
            door.transform.Translate(0, - 0.5f, 0);
        }
    }

    private void FixedUpdate() //keep it updated fixed, can be changed for speed
    {
        
        if (door.transform.position.y < doorOriginalPos.y && !DV.isOpen) //stop it from running away, check toggle
        {
            Debug.Log("up");
            door.transform.Translate(0, 0.01f, 0); //this closes the door, yAxis can be changed for speed
        }
    }
}
