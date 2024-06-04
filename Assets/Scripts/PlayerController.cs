using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float turnSpeed = 100f;

    private CharacterController _controller;
    private Animator _animator;
    private Vector3 _velocity;
    private bool _isGrounded;

    private float moveX;
    private float moveZ;

    private bool _isRunning;
    
    private static readonly int MOVES_PEED = Animator.StringToHash("MoveSpeed");

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        _isRunning = Input.GetKey(KeyCode.LeftShift);

        if(_isRunning)
            moveZ = Mathf.Lerp(moveZ, 2f, Time.deltaTime * 10f);
        else
            moveZ = Mathf.Lerp(moveZ, Input.GetAxis("Vertical"), Time.deltaTime * 10f);
          
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        transform.Rotate(0, moveX * turnSpeed * Time.deltaTime, 0);

        var move = transform.forward * moveZ;
        _controller.Move(move * (speed * Time.deltaTime));

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        
    }

    private void LateUpdate()
    {
        var moveSpeed = _controller.velocity;
        moveSpeed.y = 0;
        
        _animator.SetFloat(MOVES_PEED, moveZ);
    }
}