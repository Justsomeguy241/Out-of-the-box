using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxJumpTUT : MonoBehaviour
{
    [Header("Triggers")]
    public BoxCollider2D FoxJumpTrigger;

    [Header("Tutorial Panels")]
    public GameObject FoxJumpTutorial;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all tutorial GameObjects are initially inactive
        if (FoxJumpTutorial != null) FoxJumpTutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Player"))
        {
            FoxJumpTutorial.SetActive(true); // Show the movement tutorial when the player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Player"))
        {
            FoxJumpTutorial.SetActive(false); // Hide the movement tutorial when the player exits the trigger
        }
    }
}
