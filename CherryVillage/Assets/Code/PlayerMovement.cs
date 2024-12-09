using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Movement")]
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private float _jumpHeight = 6.5f;

    private bool _isGrounded;
    [SerializeField] private Transform _groundCheckPoint; // The object through which the isGrounded check is performed.
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer; // Layer wich the character can jump on.

    [Header("DepthMovement")]
    [SerializeField] private bool _isDeapthMoving = false;
    [SerializeField] private bool _isAbleMoveInto;
    [SerializeField] private bool _isAbleMoveOut;
    public bool IsAbleMoveInto { set { _isAbleMoveInto = value; } }
    public bool IsAbleMoveOut { set { _isAbleMoveOut = value; } }
    public void DeapthMovingCompleted() { _isDeapthMoving = true; }

    [Header("Other")]
    [SerializeField] private GameObject _sprite; // Variable for the SpriteRenderer component.
    private Rigidbody2D _rb;
    [SerializeField] private Animator _animator; // Variable for the Animator component. [OPTIONAL]

    // TEST ZONE
    public float xVelocity;
    public float yVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        BaseMovement();
        //DepthMovement();
    }

    private void BaseMovement()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        xVelocity = moveInput;
        // *** animation
        _animator.SetFloat("xVelocity", moveInput);
        _animator.SetBool("isIdle", (moveInput >= -0.1 && moveInput <= 0.1));
        // *** animation
        /*if (moveInput > 0)
            _sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (moveInput < 0)
            _sprite.transform.rotation = Quaternion.Euler(0, 180, 0);*/

        transform.position += new Vector3(moveInput * _runSpeed * Time.deltaTime, 0, 0);

        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer); // Checking if character is on the ground.

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float jumpVelocity = _jumpHeight;
        while (jumpVelocity > 0)
        {
            transform.position += new Vector3(0, jumpVelocity * Time.deltaTime, 0);
            jumpVelocity -= Time.deltaTime * 10f; // Gravity effect
            yield return null;
        }
    }

    private void DepthMovement()
    {
        if (_isAbleMoveInto && Input.GetKeyDown(KeyCode.W))
        {
            _isDeapthMoving = true;
            GameController.singleton.TransferUp();
        }
        else if (_isAbleMoveOut && Input.GetKeyDown(KeyCode.S))
        {
            _isDeapthMoving = true;
            GameController.singleton.TransferDown();
        }
    }
}
