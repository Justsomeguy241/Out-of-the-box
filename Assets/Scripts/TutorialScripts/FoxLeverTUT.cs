using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxLeverTUT : MonoBehaviour
{
    [Header("Triggers")]
    public BoxCollider2D FoxLeverTrigger;

    [Header("Tutorial Panels")]
    public GameObject FoxLeverTutorial;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all tutorial GameObjects are initially inactive
        if (FoxLeverTutorial != null) FoxLeverTutorial.SetActive(false);
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
            FoxLeverTutorial.SetActive(true); // Show the movement tutorial when the player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Player"))
        {
            FoxLeverTutorial.SetActive(false); // Hide the movement tutorial when the player exits the trigger
        }
    }
}
