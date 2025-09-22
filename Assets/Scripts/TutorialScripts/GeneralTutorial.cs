using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTutorial : MonoBehaviour
{
    [Header("Triggers")]
    public BoxCollider2D TutorialTrigger;

    [Header("Tutorial Panels")]
    public GameObject tutorialpanel;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all tutorial GameObjects are initially inactive
        if (tutorialpanel != null) tutorialpanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Crow" ) || other.CompareTag("Player"))
        {
            tutorialpanel.SetActive(true); // Show the movement tutorial when the player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Crow") || other.CompareTag("Player"))
        {
            tutorialpanel.SetActive(false); // Hide the movement tutorial when the player exits the trigger
        }
    }
}
