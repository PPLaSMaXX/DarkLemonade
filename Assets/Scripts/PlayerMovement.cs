using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player movement
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 moveInput; // Input vector for movement
    private Animator animator; // Reference to the Animator component
    private bool facingRight = true; // Flag to check if the player is facing right

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed; // Update the Rigidbody2D velocity based on input and speed

        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetFloat("Speed", moveInput.magnitude); // Set the "Speed" parameter in the Animator based on input magnitude

        if (context.canceled)
        {
            animator.SetFloat("Speed", 0); // Reset speed if the input is canceled
        }

        moveInput = context.ReadValue<Vector2>(); // Read the input value from the context

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
