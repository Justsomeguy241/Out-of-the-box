using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowTutorial : MonoBehaviour
{
    [Header("Triggers")]
    public BoxCollider2D CrowTutorialTrigger;

    [Header("Tutorial Panels")]
    public GameObject CrowTutorialPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all tutorial GameObjects are initially inactive
        if (CrowTutorialPanel != null) CrowTutorialPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Crow"))
        {
            CrowTutorialPanel.SetActive(true); // Show the movement tutorial when the player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //fox movement tutorial
        if (other.CompareTag("Crow"))
        {
            CrowTutorialPanel.SetActive(false); // Hide the movement tutorial when the player exits the trigger
        }
    }
}
