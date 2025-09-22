using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour
{

    public bool beingPushed;
    [SerializeField] private GameObject EKeyPrompt;
    private Rigidbody2D rb;
    public BoxCollider2D SidetriggerCollider;
    public BoxCollider2D TopTriggerCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (EKeyPrompt != null)
        {
            EKeyPrompt.SetActive(false); // Hide the prompt initially
        }
        else
        {
            Debug.LogError("EKeyPrompt is not assigned in BoxPull script on " + gameObject.name);
        }

        if(!beingPushed)
        {
            // Freeze X movement to keep the box stationary
            // rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            // Unfreeze X movement when being pushed
            // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!beingPushed)
        {
            // Freeze X movement to keep the box stationary
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            // rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            // Unfreeze X movement when being pushed
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && EKeyPrompt != null)
        {
            // Get the contact point
            foreach (ContactPoint2D contact in other.contacts)
            {
                // Calculate the direction from the box to the player
                Vector2 contactNormal = contact.normal;

                // If the player is on the left or right side of the box
                // Left side: normal.x > 0 (player is to the left)
                // Right side: normal.x < 0 (player is to the right)
                if (Mathf.Abs(contactNormal.x) > 0.5f)
                {
                    EKeyPrompt.SetActive(true);
                    return;
                }
            }
        }

        if (other.collider.CompareTag("Crow") && EKeyPrompt != null)
        {
            // Get the contact point
            foreach (ContactPoint2D contact in other.contacts)
            {
                // Calculate the direction from the box to the player
                Vector2 contactNormal = contact.normal;

                // If the player is on the left or right side of the box
                // Left side: normal.x > 0 (player is to the left)
                // Right side: normal.x < 0 (player is to the right)
                if (Mathf.Abs(contactNormal.y) > 0.5f)
                {
                    EKeyPrompt.SetActive(true);
                    return;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("Crow"))
        {
            EKeyPrompt.SetActive(false);
        }
    }
}
