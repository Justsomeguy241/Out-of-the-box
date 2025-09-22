using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchTUT : MonoBehaviour
{
    [Header("Triggers")]
    public BoxCollider2D PlayerSwitchTrigger;

    [Header("Tutorial Panels")]
    public GameObject PlayerSwitchTutorial;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all tutorial GameObjects are initially inactive
        if (PlayerSwitchTutorial != null) PlayerSwitchTutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Player") || other.CompareTag("Crow"))
        {
            PlayerSwitchTutorial.SetActive(true); // Show the movement tutorial when the player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Player") || other.CompareTag("Crow"))
        {
            PlayerSwitchTutorial.SetActive(false); // Hide the movement tutorial when the player exits the trigger
        }
    }
}
