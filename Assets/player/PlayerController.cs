using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MovementForce = 1;
    public float raycastOffset;
    public LayerMask groundLayers;
    public float JumpingForce = 15f;
    public float JumpingCooldwon = 0.1f;
    public float CoyoteTime = 0.2f;
    public PhysicalAnimator physicalAnimator;

    private float CoyoteTimeCounter;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Vector2 _lastInput; 
    private bool WaitingForInput = true;
    private bool DidIJump = true;
    private float _lastInputTime;
    private float _lastTimeOnPlatform;
    
    // Start is called before the first frame update
    private void Start()
    {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var isOnTheFloor = IsOnFloor();
        var hasHorizontalInput = Mathf.Abs(_lastInput.x) > Mathf.Epsilon;
        var hasVerticalSpeed = Mathf.Abs(_rigidbody2D.velocity.y) > Mathf.Epsilon;
        UpdateAnimator(hasHorizontalInput);
        
        if (hasHorizontalInput)
        {
            _lastInputTime = Time.time;
            WaitingForInput = false;
            DidIJump = false;
            _rigidbody2D.AddForce(_lastInput*MovementForce);   
        }
        else
        {
            if (Time.time - _lastInputTime >= JumpingCooldwon)
            {
                WaitingForInput = true;
            }
        }
        
        if (isOnTheFloor)
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }
        if (WaitingForInput && !DidIJump && CoyoteTimeCounter>0f && !hasVerticalSpeed )
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity+(Vector2.up*JumpingForce);
            DidIJump = true;
            CoyoteTimeCounter = 0f;
        }
    }
    
    private bool IsOnFloor()
    {
        Vector2 position2D = _transform.position;
        var startPosition = position2D + Vector2.up * raycastOffset;

        bool isOnTheFloor = Physics2D.Raycast(startPosition, Vector2.down, 0.1f, groundLayers);
        var endPostion = startPosition + Vector2.down * 0.1f;
        Debug.DrawLine(startPosition, endPostion, Color.red);
        return isOnTheFloor;
    }

    private void UpdateAnimator(bool hasMovement)
    {
        physicalAnimator.isMoving = hasMovement;
        physicalAnimator.facingLeft = _lastInput.x < -0.1f;
    }
    
    public void ReadInput(InputAction.CallbackContext rawInput)
    {
        _lastInput = rawInput.ReadValue<Vector2>();
    }
}


