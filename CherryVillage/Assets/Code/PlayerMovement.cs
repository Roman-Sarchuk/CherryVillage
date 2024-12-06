using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Movement")]
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    private bool isGrounded;
    [SerializeField] private GameObject _groundCheckPoint; // The object through which the isGrounded check is performed.
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer; // Layer wich the character can jump on.

    [Header("Keys")]
    [SerializeField] private bool _jumpPressed = false; // Variable that will check is "Space" key is pressed.
    [SerializeField] private bool _APressed = false; // Variable that will check is "A" key is pressed.
    [SerializeField] private bool _DPressed = false; // Variable that will check is "D" key is pressed.
    [SerializeField] private bool _intoPressed = false; // Variable that will check is "W" key is pressed.
    [SerializeField] private bool _outPressed = false; // Variable that will check is "S" key is pressed.

    [Header("DepthMovement")]
    [Range(0f, 100f)]
    [SerializeField] private float _minScalePercent = 50f;
    [Range(100f, 200f)]
    [SerializeField] private float _maxScalePercent = 100f;
    [SerializeField] private float _scaleCoef = 0.1f;
    [SerializeField] private Vector3 _scaleVector3;
    [SerializeField] private Vector3 _initialScale;
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Vector3 _minScale;
    [SerializeField] private bool _isDeapthMoving = false;
    [SerializeField] private bool _isAbleMoveInto;
    [SerializeField] private bool _isAbleMoveOut;
    [SerializeField] private LayerMask _upZoneLayer;
    [SerializeField] private LayerMask _downZoneLayer;


    [Header("Other")]
    [SerializeField] private SpriteRenderer _sprite; // Variable for the SpriteRenderer component.
    [SerializeField] private Sprite _jumpSprite; // Sprite that shows up when the character is not on the ground. [OPTIONAL]
    private Rigidbody2D _rb;
    // private Animator animator; // Variable for the Animator component. [OPTIONAL]

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _initialScale = transform.localScale;
        _maxScale = _initialScale * _maxScalePercent;
        _minScale = _initialScale * _minScalePercent;

        _scaleVector3 = Vector3.zero * _scaleCoef;
    }

    private void Update()
    {
        _jumpPressed = Input.GetKeyDown(KeyCode.Space);
        _APressed = Input.GetKey(KeyCode.A);
        _DPressed = Input.GetKey(KeyCode.D);
        _intoPressed = Input.GetKey(KeyCode.W);
        _outPressed = Input.GetKey(KeyCode.S);
    }

    private void FixedUpdate()
    {
        if (!_isDeapthMoving)
            BaseMovement();
        DepthMovement();
    }

    private void BaseMovement()
    {
        isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.transform.position, _groundCheckRadius, _groundLayer); // Checking if character is on the ground.

        // Left/Right movement
        if (_APressed)
        {
            _rb.velocity = new Vector2(-_runSpeed, _rb.velocity.y); // Move left physics.
            _sprite.flipX = false; // Rotating the character object to the left.
            _APressed = false; // Returning initial value.
        }
        else if (_DPressed)
        {
            _rb.velocity = new Vector2(_runSpeed, _rb.velocity.y);
            _sprite.flipX = true; // Rotating the character object to the right.
            _DPressed = false;
        }
        else _rb.velocity = new Vector2(0, _rb.velocity.y);

        // Jumps.
        if (_jumpPressed && isGrounded)
        {
            _rb.velocity = new Vector2(0, _jumpForce); // Jump physics.
            _jumpPressed = false;
        }

        // Setting jump sprite. [OPTIONAL]
        /*if (!isGrounded)
        {
            animator.enabled = false; // Turning off animation.
            sr.sprite = jumpSprite; // Setting the sprite.
        }
        else animator.enabled = true; // Turning on animation.*/
    }

    private void DepthMovement()
    {
        _isAbleMoveInto = Physics2D.OverlapCircle(_groundCheckPoint.transform.position, _groundCheckRadius, _upZoneLayer);
        _isAbleMoveOut = Physics2D.OverlapCircle(_groundCheckPoint.transform.position, _groundCheckRadius, _downZoneLayer);

        // Into/Out movement
        if (_isAbleMoveInto && _intoPressed)
        {
            transform.localScale = transform.localScale + _scaleVector3;
            //_rb.velocity = new Vector2(-_runSpeed, _rb.velocity.y); // Move left physics.
            //_sprite.flipX = false; // Rotating the character object to the left.
            _isDeapthMoving = true;
        }
        else if (_isAbleMoveOut && _outPressed)
        {
            transform.localScale -= _scaleVector3;
            //_rb.velocity = new Vector2(_runSpeed, _rb.velocity.y);
            //_sprite.flipX = true; // Rotating the character object to the right.
            _isDeapthMoving = true;
        }
    }
}
