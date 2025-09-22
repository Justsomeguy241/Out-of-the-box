
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CrowScript : MonoBehaviour
{
    public Rigidbody2D CrowRb;
    [SerializeField]
    public bool isFlying = false;
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private bool isGrabItem = false;
    public bool CrowInputEnabled = false;

    [Header("crow Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] public float flyStrength = 5f;

    [Header("Ground Check")]
    [SerializeField]
    public BoxCollider2D GroundCheckGameObject;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.01f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Push/Pull")]
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask InteractMask;
    private GameObject box;
    private bool isPushingOrPulling = false; //state for pushing/pulling

    private float horizontalInput = 0f;



    [Header("Player Switch")]
    public PlayerSwitch playerSwitchScript;

    // Start is called before the first frame update
    void Awake()
    {
        CrowRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrowInputEnabled)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            if (!isGrounded && isFlying)
            {
                CrowRb.velocity = new Vector2(horizontalInput * speed, CrowRb.velocity.y);
            }
            else if (isGrounded && !isFlying)
            {
                CrowRb.velocity = new Vector2(0f, CrowRb.velocity.y);
            }

            // Flip the sprite based on the direction the player is moving (VISUAL)
            if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f); // Facing right
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f); // Facing left
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CrowRb.velocity = Vector2.up * flyStrength;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                CrowRb.gravityScale = 0.1f;
            }
            else
            {
                CrowRb.gravityScale = 1f;
            }

            if (playerSwitchScript.isAttached)
            {
                CrowRb.velocity = new Vector2(0f, 0f); // Disable crow input when attached to the fox
            }

        }

        // Perform a raycast below the player to check for pushable objects
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, InteractMask);

        // If a pushable object is detected and the player presses "E", attach it to the player using a FixedJoint2D
        if (hit.collider != null && hit.collider.CompareTag("Pushable") && Input.GetKeyDown(KeyCode.E) && CrowInputEnabled)
        {
            isPushingOrPulling = true;
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<BoxPull>().beingPushed = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();

        }
        else if (Input.GetKeyUp(KeyCode.E) && CrowInputEnabled && hit.collider != null) // When the player releases "E", detach the object by disabling the FixedJoint2D
        {
            isPushingOrPulling = false;
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;
        }

        if (isGrounded)
        {
            isFlying = false; // Reset flying state when grounded
        }
        else
        {
            isFlying = true; // Set flying state when not grounded
        }

        if (isGrounded && CrowInputEnabled == false)
        {
            CrowRb.velocity = new Vector2(0f, 0f); // Disable crow input when grounded and not attached to the fox
        }
    }


    private void FixedUpdate()
    {
        if (GroundCheckGameObject.IsTouchingLayers(groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }

    public void OnDetachBoost()
    {
        // Detach the crow from the fox and apply a boost
        CrowRb.velocity = new Vector2(CrowRb.velocity.x, flyStrength * 2.5f);
    }
}

