using UnityEngine;

public class Movement : PlayerComponent
{
    [SerializeField] private float drag = 20, airModifier = .3f;

    private float currentSpeed;
    private float targetSpeed;


    private void Start()
    {
        targetSpeed = player.Specs.WalkSpeed;
    }

    private void OnEnable()
    {
        PlayerInput.OnMoveInput += Move;
        Player.OnStateChange += ChangedState;
    }

    private void OnDisable()
    {
        PlayerInput.OnMoveInput -= Move;
        Player.OnStateChange -= ChangedState;
    }

    private void ChangedState(PlayerState _state)
    {
        switch (_state)
        {
            case PlayerState.Airborne: targetSpeed = currentSpeed * player.Specs.airborneSpeedModifier; break;
            case PlayerState.Walking: targetSpeed = player.Specs.WalkSpeed; break;
            case PlayerState.Sprinting: targetSpeed = player.Specs.SprintSpeed; break;
            case PlayerState.Crouching: targetSpeed = player.Specs.CrouchSpeed; break;
            case PlayerState.Sliding: targetSpeed = player.Specs.SlideSpeed; break;
        }
    }

    private void FixedUpdate()
    {
        SetSpeed(targetSpeed);
        SpeedLimit();
        CounterMovement();

        UIManager.Instance.UpdateVelocity(player.Rb.velocity.magnitude);
    }

    private void SetSpeed(float _speed)
    {
        currentSpeed = _speed;
    }

    public void SetTargetSpeed(float _value) => targetSpeed = _value;

    private void Move(Vector2 _dir)
    {
        player.TryWalking = _dir.magnitude > 0;

        var _moveDir = _dir.x * player.Orientation.forward + _dir.y * player.Orientation.right;

        if (player.State == PlayerState.Airborne)
            player.Rb.AddForce(currentSpeed * drag * airModifier * _moveDir.normalized, ForceMode.Acceleration);
        else
            player.Rb.AddForce(currentSpeed * drag * _moveDir.normalized, ForceMode.Acceleration);
    }

    private void SpeedLimit()
    {
        var _flatVel = new Vector3(player.Rb.velocity.x, 0, player.Rb.velocity.z);
        if (_flatVel.magnitude <= currentSpeed) return;

        Vector3 _limitedVel = _flatVel.normalized * currentSpeed;
        player.Rb.velocity = new Vector3(_limitedVel.x, player.Rb.velocity.y, _limitedVel.z);
    }

    private void CounterMovement()
    {
        if (player.State == PlayerState.Falling || player.State == PlayerState.Airborne) return;
        var _friction = (currentSpeed * drag) / (targetSpeed + .1f);
        player.Rb.AddForce(-player.Rb.velocity * _friction, ForceMode.Acceleration);
    }

}
