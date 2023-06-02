using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Player))]
public class Movement : PlayerComponent
{
    private Rigidbody rb;
    private PlayerInput input;
    private Transform orientation;

    [SerializeField] private float drag = 20, airModifier = .3f;

    private float currentSpeed;
    private float targetSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        input = GetComponent<PlayerInput>();
        orientation = input.Orientation;

    }

    private void Start()
    {
        targetSpeed = specs.WalkSpeed;
    }

    private void OnEnable()
    {

        Player.OnStateChange += ChangedState;
    }

    private void OnDisable()
    {
        Player.OnStateChange -= ChangedState;
    }

    private void ChangedState(PlayerState _state)
    {
        switch (_state)
        {
            case PlayerState.Airborne: targetSpeed = currentSpeed * specs.airborneSpeedModifier;  break;
            case PlayerState.Walking: targetSpeed = specs.WalkSpeed; break;
            case PlayerState.Sprinting: targetSpeed = specs.SprintSpeed; break;
        }
    }

    private void FixedUpdate()
    {
        SetSpeed(targetSpeed);
        Move(input.Move);
        SpeedLimit();
        CounterMovement();
        
        UIManager.Instance.UpdateVelocity(rb.velocity.magnitude);
    }

    private void SetSpeed(float _speed) => currentSpeed = _speed;

    public void SetTargetSpeed(float _value) => targetSpeed = _value;

    private void Move(Vector2 _dir)
    {
        player.TryWalking = _dir.magnitude > 0;

        var _moveDir = _dir.x * orientation.forward + _dir.y * orientation.right;

        if (player.State == PlayerState.Airborne)
            rb.AddForce(currentSpeed * drag * airModifier * _moveDir.normalized, ForceMode.Acceleration);
        else
            rb.AddForce(currentSpeed * drag * _moveDir.normalized, ForceMode.Acceleration);
    }

    private void SpeedLimit()
    {
        var _flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (_flatVel.magnitude <= currentSpeed) return;

        Vector3 _limitedVel = _flatVel.normalized * currentSpeed;
        rb.velocity = new Vector3(_limitedVel.x, rb.velocity.y, _limitedVel.z);
    }

    private void CounterMovement()
    {
        if (player.State == PlayerState.Airborne) return;
        var _friction = (currentSpeed * drag) / (targetSpeed + .1f);
        rb.AddForce(-rb.velocity * _friction, ForceMode.Acceleration);
    }

}
