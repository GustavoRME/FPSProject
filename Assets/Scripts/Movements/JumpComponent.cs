using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 100f;    
    [SerializeField] private float _gravityForce = 9.8f;
        
    private float _verticalForce;    

    private bool _isGrounded = true;
    private bool _isJumping = false;    
    
    private CharacterController _controller;

    private void Start() => _controller = GetComponent<CharacterController>();        
    private void Update()
    {
        if(_isJumping)
        {            
            //While is jumping, clamp the values between gravityForce and JumpForce, always the current verticalForce with the gravityForce
            //Doing this we have a gradual decceleration on fall
            _verticalForce = Mathf.Clamp(_verticalForce - _gravityForce, -_gravityForce, _jumpForce);

            //Make the movement and store the Collision flag from the movement we already did.
            CollisionFlags collisionFlags = _controller.Move(transform.up * _verticalForce * Time.deltaTime);

            //Check if we touch ground
            _isGrounded = (collisionFlags & CollisionFlags.Below) != 0;
           
            //wheather we touch ground, so we're not jumping more. 
            _isJumping = !_isGrounded;            
        }        
    }

    public void SetJump()
    {
        if(!_isJumping)
        {
            //When starting jumping the vertical force start with the value from jumpForce
            _isJumping = true;            
            _verticalForce = _jumpForce;             
        }
    }
}
