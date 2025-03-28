using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    [Header("Jump Parameters")]

    [SerializeField] private float jumpForce = 10f;

    [Header("Script Refrences")]
    public GemManager gm;

    private Rigidbody2D rb;
    private PlayerInputHandler playerInputHandler;
    private float horizontalInput;
    private bool isGrounded;
    private bool shouldJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerInputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        horizontalInput = playerInputHandler.MoveInput.x;
        shouldJump = playerInputHandler.JumpTriggered && isGrounded;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        if (shouldJump)
        {
            Debug.Log("Applying jump");
            ApplyJump();
        }
    }

    void ApplyMovement()
    {
        float speed = moveSpeed * (playerInputHandler.SprintValue > 0 ? sprintMultiplier : 1f);
        rb.linearVelocity = new Vector2 (horizontalInput * speed, rb.linearVelocity.y);
    }

    void ApplyJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        shouldJump = false;
        Debug.Log("Jump has been applied");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("I am touching the ground");
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            gm.gemCount++;
        }
    }
}
