using System.Collections.Generic;
using UnityEngine;

public class PlaceableCharacterView : PlaceableView
{
    public Transform Model;
    public float _moveSpeed = 2;
    public float _turnSpeed = 200;
    public float _jumpForce = 4;

    public Animator _animator;
    public Rigidbody _rigidBody;
    
    private float _currentV;
    private float _currentH;

    private readonly float _interpolation = 10;
    
    private bool _wasGrounded;
    private Vector3 _currentDirection = Vector3.zero;

    private float _jumpTimeStamp;
    private float _minJumpInterval = 0.25f;
    private bool _jumpInput;

    private bool _isGrounded;

    private List<Collider> _collisions = new List<Collider>();
    
    private static readonly int Land = Animator.StringToHash("Land");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

    protected override void InitializeModel()
    {
    }

    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        _cameraView = (CameraView)_contexts.game.cameraEntity.view.Value;
    }
    
    protected override void Update()
    {

#if UNITY_EDITOR
        if (!_jumpInput && Input.GetKey(KeyCode.Space))
#else
        if (!_jumpInput && _contexts.game.isJump)
#endif
        {
            _jumpInput = true;
            _contexts.game.isJump = false;
        }
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!_collisions.Contains(collision.collider))
                {
                    _collisions.Add(collision.collider);
                }
                _isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            _isGrounded = true;
            if (!_collisions.Contains(collision.collider))
            {
                _collisions.Add(collision.collider);
            }
        }
        else
        {
            if (_collisions.Contains(collision.collider))
            {
                _collisions.Remove(collision.collider);
            }
            if (_collisions.Count == 0) { _isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_collisions.Contains(collision.collider))
        {
            _collisions.Remove(collision.collider);
        }
        if (_collisions.Count == 0) { _isGrounded = false; }
    }
    
    private void FixedUpdate()
    {
        _animator.SetBool(Grounded, _isGrounded);

        MoveUpdate();

        _wasGrounded = _isGrounded;
        _jumpInput = false;
    }
    
    private void MoveUpdate()
    {
#if UNITY_EDITOR 
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
#else
        float v = _contexts.game.movement.Value.y;
        float h = _contexts.game.movement.Value.x;
#endif
        var currentCamera = _cameraView.transform;

        _currentV = Mathf.Lerp(_currentV, v, Time.deltaTime * _interpolation);
        _currentH = Mathf.Lerp(_currentH, h, Time.deltaTime * _interpolation);

        Vector3 direction = currentCamera.forward * _currentV + currentCamera.right * _currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            _currentDirection = Vector3.Slerp(_currentDirection, direction, Time.deltaTime * _interpolation);

            Model.rotation = Quaternion.LookRotation(_currentDirection);
            transform.position += _currentDirection * (_moveSpeed * Time.deltaTime);

            _animator.SetFloat(MoveSpeed, direction.magnitude);
        }

        JumpingAndLanding();
    }
    
    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - _jumpTimeStamp) >= _minJumpInterval;

        if (jumpCooldownOver && _isGrounded && _jumpInput)
        {
            _jumpTimeStamp = Time.time;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        if (!_wasGrounded && _isGrounded)
        {
            _animator.SetTrigger(Land);
        }

        if (!_isGrounded && _wasGrounded)
        {
            _animator.SetTrigger(Jump);
        }
    }

    public override void OnAnyEditmode(GameEntity entity)
    {
        base.OnAnyEditmode(entity);
        if (_entity.hasPlaceablePosition)
        {
            transform.position = _entity.placeablePosition.Value;
        }

        if (_entity.hasPlaceableRotation)
        {
            transform.rotation = Quaternion.Euler(_entity.placeableRotation.Value);
        }
    }
}