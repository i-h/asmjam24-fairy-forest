using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public enum MoveMode {None, Regular, OtherSide}
    [Header("References")]
    [SerializeField] private InputHandler _input;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _lookTransform;
    [SerializeField] private Transform _spaceIndicator = default;
    [SerializeField] private SpatialField _field;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [Header("Options")]
    [SerializeField] private MoveMode _mode = MoveMode.Regular;
    [SerializeField] private float[] _minMaxAngle = {60,90};
    [SerializeField] private bool _spaceBendActive = true;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpCooldown = 0.2f;
    [SerializeField] private float _dashCooldown = 0.8f;
    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private float _nextJump = -1;
    [SerializeField] private float _dashEnd = -1;
    [SerializeField] private float _dashDuration = 1.2f;
    [SerializeField] private float _nextDash = -1;
    [SerializeField] private float _changeVelocity = 20f;
    Vector2 _lastInput;
    float lastSpaceBend = 0;
    float spaceBendEffect = 1;

    private void Start(){
        OtherSideManager.WorldChanged += OnWorldChanged;
        SetSpaceBend(0);
    }
    private void OnDestroy(){
        OtherSideManager.WorldChanged -= OnWorldChanged;
    }
    private void OnWorldChanged(OtherSideManager.World world){
        _spaceBendActive = world == OtherSideManager.World.OtherSide;
        var velocity = _rb.velocity;

        switch(world){
            case OtherSideManager.World.Normal:
                _mode = MoveMode.Regular;
                SetSpaceBend(0);
                velocity.y = Mathf.Max(velocity.y, _changeVelocity);
            break;
            case OtherSideManager.World.OtherSide:
                _mode = MoveMode.OtherSide;
                velocity.y = Mathf.Max(velocity.y, -_changeVelocity);
            break;
        }
                _rb.velocity = velocity;
    }
    private void Update(){
        _lastInput = _input.GetInput();

        if(_input.GetJump()) {
            switch(_mode){
                case MoveMode.Regular:
                Jump();
                break;
                case MoveMode.OtherSide:
                Dash();
                break;
            }
        }

        if(Input.GetKeyDown(KeyCode.T)) _spaceBendActive = !_spaceBendActive;
    }
    private void FixedUpdate()
    {
        Vector3 forwardMovement = _lookTransform.forward * _lastInput.y;
        Vector3 sidewaysMovement = _lookTransform.right * _lastInput.x;

        if(_mode == MoveMode.OtherSide){
            if(_spaceBendActive && _field) {
                lastSpaceBend = _field.GetValue(transform.position.x, transform.position.y, transform.position.z);
                SetSpaceBend(lastSpaceBend);
            }
        } else {
            forwardMovement.y = 0;
        }

        _rb.MovePosition(transform.position + (forwardMovement+sidewaysMovement) * _moveSpeed * Time.deltaTime * spaceBendEffect);
    }

    private void SetSpaceBend(float bend){
        if(_spaceIndicator) _spaceIndicator.localScale = Vector3.one * bend;
        _camera.m_Lens.FieldOfView = Mathf.Lerp(_minMaxAngle[1], _minMaxAngle[0], bend);
        transform.localScale = Vector3.one * Mathf.Lerp(1, 0.2f, bend);
        spaceBendEffect = GetSpaceBendEffect(bend);
    }

    private void Jump(){
        if(Time.unscaledTime > _nextJump && IsGrounded()){
        _nextJump = Time.unscaledTime + _jumpCooldown;
            var velocity = _rb.velocity;
            velocity.y = Mathf.Max(velocity.y, _jumpForce);
            _rb.velocity = velocity;
        }
    }
    private void Dash(){
        if(Time.unscaledTime > _nextDash){
            StartCoroutine(DashCoroutine());
        }
    }
    private IEnumerator DashCoroutine(){        
        float dashProgress;
        float dashStart = Time.unscaledTime;
        _dashEnd = dashStart + _dashDuration;
        Debug.Log("Dash Start");
        while(Time.unscaledTime < _dashEnd){
            dashProgress = (Time.unscaledTime - dashStart) / _dashDuration;            
            yield return new WaitForEndOfFrame();
        }
        _nextDash = Time.unscaledTime + _dashCooldown;
        Debug.Log("Dash Done");

    }
    private bool IsGrounded(){
        return Physics.Linecast(transform.position + Vector3.up, transform.position + Vector3.down * 0.2f, ~LayerMask.GetMask("Player"));
    }
    private float GetSpaceBendEffect(float value) => _spaceBendActive ? 1-0.8f*value : 1;
}
