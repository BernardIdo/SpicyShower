using player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MovementForce = 1;
    public float raycastOffset;
    public LayerMask groundLayers;
    public float JumpingForce = 15f;
    public float CoyoteTime = 0.2f;
    public PhysicalAnimator physicalAnimator;

    private bool shouldMove;
    private float _timeRemainingForCoyoteState;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Vector2 _lastInput; 
    private bool _hadInputSinceLanded;
    private PlayerStates _currentState;
    private Vector3 _initialPosition;
    private bool _moving;
    
    private void Start()
    {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentState = PlayerStates.Grounded;
        _initialPosition = _transform.position;
        PhoneInput.instance.onDrag -= HandleDrag;
        PhoneInput.instance.onDrag += HandleDrag;
        PhoneInput.instance.onRelease -= HandleRelease;
        PhoneInput.instance.onRelease += HandleRelease;
    }

    private void HandleDrag(bool isLeft)
    {
        _lastInput.x = isLeft ? -1 : 1;
        _moving = true;
    }

    private void HandleRelease()
    {
        _moving = false;
        _lastInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!shouldMove)
        {
            return;
        }
        UpdateGroundedState();
        TryExitCoyoteFall();
        MoveHorizontally();
        UpdateAnimator(_moving);
        TryJump();
    }
    
    private void TryExitCoyoteFall()
    {
        if (_currentState == PlayerStates.FallingCoyote)
        {
            _timeRemainingForCoyoteState -= Time.deltaTime;
            if (_timeRemainingForCoyoteState <= 0)
            {
                _currentState = PlayerStates.Falling;
            }
        }
    }

    private void TryJump()
    {
        if (_moving && _currentState == PlayerStates.Grounded)
        {
            _hadInputSinceLanded = true;
        }
        
        var canJump = (!_moving && _hadInputSinceLanded) &&
                      _currentState is PlayerStates.Grounded or PlayerStates.FallingCoyote;
        if (canJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = _rigidbody2D.velocity+(Vector2.up*JumpingForce);
        _currentState = PlayerStates.Jumped;
        GameManager.instance.TryStartGame();
    }

    private void UpdateGroundedState()
    {
        Vector2 position2D = _transform.position;
        var startPosition = position2D + Vector2.up * raycastOffset;

        bool isOnTheFloor = Physics2D.Raycast(startPosition, Vector2.down, 0.1f, groundLayers);
        var endPosition = startPosition + Vector2.down * 0.1f;
        Debug.DrawLine(startPosition, endPosition, Color.red);

        if (isOnTheFloor)
        {
            var hasVerticalSpeed = Mathf.Abs(_rigidbody2D.velocity.y) > Mathf.Epsilon;
            if (_currentState is PlayerStates.Falling or PlayerStates.FallingCoyote ||
                _currentState == PlayerStates.Jumped && !hasVerticalSpeed)
            {
                Land();
            }
        }
        else if (_currentState == PlayerStates.Grounded)
        {
            _timeRemainingForCoyoteState = CoyoteTime;
            _currentState = PlayerStates.FallingCoyote;
        }
    }

    private void Land()
    {
        _currentState = PlayerStates.Grounded;
        _hadInputSinceLanded = false;
    }

    private void UpdateAnimator(bool hasMovement)
    {
        physicalAnimator.isMoving = hasMovement;
        physicalAnimator.facingLeft = _rigidbody2D.velocity.x < -0.1f;
    }

    private bool MoveHorizontally()
    {
        var hasHorizontalInput = Mathf.Abs(_lastInput.x) > Mathf.Epsilon;
        if (hasHorizontalInput)
        {
            _rigidbody2D.AddForce(_lastInput*MovementForce);    
        }
        
        return hasHorizontalInput;
    }
    
    public void ReadInput(InputAction.CallbackContext rawInput)
    {
        _lastInput = rawInput.ReadValue<Vector2>();
    }

    public void StopMoving()
    {
        shouldMove = false;
    }
    public void StartMoving()
    {
        shouldMove = true;
    }

    public void PositionForNewGame()
    {
        _transform.position = _initialPosition;
    }
}


