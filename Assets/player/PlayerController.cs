using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MovementForce = 1;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _lastInput; 
    
    // Start is called before the first frame update
    private void Start()
    {
       
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigidbody2D.AddForce(_lastInput*MovementForce);
    }

    public void ReadInput(InputAction.CallbackContext rawInput)
    {
        _lastInput = rawInput.ReadValue<Vector2>();
    }
}
