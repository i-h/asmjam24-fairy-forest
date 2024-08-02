using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler _input;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _lookTransform;
    [SerializeField] private Transform _spaceIndicator = default;
    [SerializeField] private SpatialField _field;
    [SerializeField] private bool _spaceBendActive = true;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float[] _minMaxAngle = {60,90};
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpForce = 100f;
    Vector2 _lastInput;
    float lastSpaceBend = 0;

    private void Update(){
        _lastInput = _input.GetInput();

        if(_input.GetJump()){
            var velocity = _rb.velocity;
            velocity.y = Mathf.Max(velocity.y, _jumpForce);
            //_rb.velocity = velocity; // No jumping just yet
        }
        if(Input.GetKeyDown(KeyCode.T)) _spaceBendActive = !_spaceBendActive;
    }
    private void FixedUpdate()
    {

        Vector3 forwardMovement = _lookTransform.forward * _lastInput.y;
        Vector3 sidewaysMovement = _lookTransform.right * _lastInput.x;

        if(_spaceBendActive && _field) {
            lastSpaceBend = _field.GetValue(transform.position.x, transform.position.y, transform.position.z);
            if(_spaceIndicator) _spaceIndicator.localScale = Vector3.one * lastSpaceBend;
            _camera.m_Lens.FieldOfView = Mathf.Lerp(_minMaxAngle[1], _minMaxAngle[0], lastSpaceBend);
            transform.localScale = Vector3.one * Mathf.Lerp(1, 0.2f, lastSpaceBend);
        }

        _rb.MovePosition(transform.position + (forwardMovement+sidewaysMovement) * _moveSpeed * Time.deltaTime * GetSpaceBendEffect(lastSpaceBend));
    }

    private float GetSpaceBendEffect(float value) => _spaceBendActive ? 1-0.8f*value : 1;
}
