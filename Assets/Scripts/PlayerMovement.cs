using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler _input;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _lookTransform;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpForce = 100f;
    Vector2 _lastInput;

    private void FixedUpdate()
    {
        _lastInput = _input.GetInput();

        Vector3 forwardMovement = _lookTransform.forward * _lastInput.y;
        Vector3 sidewaysMovement = _lookTransform.right * _lastInput.x;

        if(_input.GetJump()){
            var velocity = _rb.velocity;
            velocity.y = Mathf.Max(velocity.y, _jumpForce);
            _rb.velocity = velocity;
        }

        _rb.MovePosition(transform.position + (forwardMovement+sidewaysMovement) * _moveSpeed* Time.deltaTime);
    }
}
