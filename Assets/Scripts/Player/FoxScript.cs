using Unity.VisualScripting;
using UnityEngine;

public class FoxScript : MonoBehaviour
{
    public Rigidbody2D FoxRb;
    public float Horizontal;
    public bool isJumping = false;
    public bool isGrounded = true;
    [SerializeField] private bool isWalled;
    public bool isPushingOrPulling = false; //state for pushing/pulling
    // private bool IsWallSliding = false;
    private bool IsWallJumping = false;
    // private float WallJumpingDirection;
    public bool IsFacingRight = true;
    public bool InputEnabled = true;
    public AudioManager audioManager;



    [Header("Player Movement")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float reducedSpeed = 2f; //Slower speed when pushing/pulling
    [SerializeField] private float jumpingPower = 7f;


    [Header("Ground/Wall Check")]
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private BoxCollider2D WallCheckBox;
    [SerializeField] private float groundcheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask WallLayer;

    [Header("Push/Pull")]
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask InteractMask;
    private GameObject box;

    [Header("WallJump")]
    [SerializeField] private bool wallJumpBool = false;
    [SerializeField] private int wallJumpCount = 0;
    [SerializeField] private int maxWallJumps = 3;
    [SerializeField] private float WallJumpingTime = 0.2f;
    [SerializeField] private float WallSlidingSpeed = 2f;
    [SerializeField] private float WallJumpCounter = 0;
    [SerializeField] private float WallJumpDuration = 0.4f;
    [SerializeField] private Vector2 WallJumpingPower = new Vector2(8f, 16f);


    private void Awake()
    {
        FoxRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        ProcessInputs();
        // Perform a raycast in the direction the player is facing to check for pushable objects
        Physics2D.queriesStartInColliders = false;
        Vector2 direction = GetRotatedDirection();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, InteractMask);


        // If a pushable object is detected and the player presses "E", attach it to the player using a FixedJoint2D
        if (hit.collider != null && hit.collider.CompareTag("Pushable") && Input.GetKeyDown(KeyCode.E) && InputEnabled)
        {
            isPushingOrPulling = true;
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<BoxPull>().beingPushed = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyUp(KeyCode.E) && hit.collider != null && InputEnabled) // When the player releases "E", detach the object by disabling the FixedJoint2D
        {
            isPushingOrPulling = false;
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;
        }

        // WallSlide();
        // WallJump();

        if (!isGrounded)
        {
            isJumping = true;
        }
        else if (isGrounded)
        {
            isJumping = false;
        }
        if (InputEnabled == false)
        {
            FoxRb.velocity = new Vector2(0f, FoxRb.velocity.y);//berhentiin movement horizontal
        }

        if (WallCheckBox.IsTouchingLayers(WallLayer))
        {
            isWalled = true;
        }
        else
        {
            isWalled = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
        checkGround();
    }



    private void Move()
    {
        if (!IsWallJumping && InputEnabled == true)
        {
            FoxRb.velocity = new Vector2(Horizontal * speed, FoxRb.velocity.y);
        }

        if (isPushingOrPulling == true)
        {
            FoxRb.velocity = new Vector2(Horizontal * reducedSpeed, FoxRb.velocity.y);

        }
        else
        {
            Flip();
        }



    }

    private void ProcessInputs()
    {
        
        if (isGrounded && !isJumping)
        {
            wallJumpCount = 0; // Reset wall jump count when grounded
        }

        if (InputEnabled == true)
        {
            Horizontal = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && !isPushingOrPulling && isGrounded && !isJumping)
            {
                //audioManager.PlaySFX(audioManager.Jump); // volume and audio kinda messed up
                FoxRb.velocity = new Vector2(FoxRb.velocity.x, jumpingPower);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isPushingOrPulling && !isGrounded && isWalled && isJumping)
            {
                if (IsFacingRight)
                {
                    FoxRb.velocity = new Vector2(-WallJumpingPower.x, WallJumpingPower.y);
                    IsWallJumping = true;
                    WallJumpCounter = WallJumpingTime;
                    wallJumpCount++;
                    Flip();
                    Invoke(nameof(StopWallJumping), WallJumpDuration);
                }
                else if (!IsFacingRight)
                {
                    FoxRb.velocity = new Vector2(WallJumpingPower.x, WallJumpingPower.y);
                    IsWallJumping = true;
                    WallJumpCounter = WallJumpingTime;
                    wallJumpCount++;
                    Flip();
                    Invoke(nameof(StopWallJumping), WallJumpDuration);
                }
                audioManager.PlaySFX(audioManager.Jump);
            }
        }
    }
    
    private void StopWallJumping()
    {
        IsWallJumping = false; // Allow movement again
    }

    private void Flip()
    {
        
        // Flip character when changing direction
        if (IsFacingRight && Horizontal < 0f || !IsFacingRight && Horizontal > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    private bool checkGround()
    {
        if (groundCheck.IsTouchingLayers(groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }

    private Vector2 GetRotatedDirection()
    {
        return IsFacingRight ? Vector2.right : Vector2.left;
    }



}
