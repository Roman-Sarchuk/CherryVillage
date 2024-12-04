using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 5f; // Running speed.
    [SerializeField] private float _jumpForce = 5f; // Jump height.

    [SerializeField] private Sprite _jumpSprite; // Sprite that shows up when the character is not on the ground. [OPTIONAL]

    private Rigidbody2D _rb; // Variable for the RigidBody2D component.
    [SerializeField] private SpriteRenderer _sprite; // Variable for the SpriteRenderer component.
    // private Animator animator; // Variable for the Animator component. [OPTIONAL]

    private bool isGrounded; // Variable that will check if character is on the ground.
    [SerializeField] private GameObject _groundCheckPoint; // The object through which the isGrounded check is performed.
    [SerializeField] private float _groundCheckRadius; // isGrounded check radius.
    [SerializeField] private LayerMask _groundLayer; // Layer wich the character can jump on.

    private bool _jumpPressed = false; // Variable that will check is "Space" key is pressed.
    private bool _APressed = false; // Variable that will check is "A" key is pressed.
    private bool _DPressed = false; // Variable that will check is "D" key is pressed.

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); // Setting the RigidBody2D component.
    }

    // Update() is called every frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) _jumpPressed = true; // Checking on "Space" key pressed.
        if (Input.GetKey(KeyCode.A)) _APressed = true; // Checking on "A" key pressed.
        if (Input.GetKey(KeyCode.D)) _DPressed = true; // Checking on "D" key pressed.
    }

    // Update using for physics calculations.
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.transform.position, _groundCheckRadius, _groundLayer); // Checking if character is on the ground.

        // Left/Right movement.
        if (_APressed)
        {
            _rb.velocity = new Vector2(-_runSpeed, _rb.velocity.y); // Move left physics.
            _sprite.flipX = false; // Rotating the character object to the left.
            _APressed = false; // Returning initial value.
        }
        else if (_DPressed)
        {
            _rb.velocity = new Vector2(_runSpeed, _rb.velocity.y); // Move right physics.
            _sprite.flipX = true; // Rotating the character object to the right.
            _DPressed = false; // Returning initial value.
        }
        else _rb.velocity = new Vector2(0, _rb.velocity.y);

        // Jumps.
        if (_jumpPressed && isGrounded)
        {
            _rb.velocity = new Vector2(0, _jumpForce); // Jump physics.
            _jumpPressed = false; // Returning initial value.
        }

        // Setting jump sprite. [OPTIONAL]
        /*if (!isGrounded)
        {
            animator.enabled = false; // Turning off animation.
            sr.sprite = jumpSprite; // Setting the sprite.
        }
        else animator.enabled = true; // Turning on animation.*/
    }
}