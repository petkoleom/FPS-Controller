using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rb;

    public PlayerSpecifications Specs;
    public Transform Orientation;
    public Transform Body;

    private Vector3 flatVelocity;

    public bool IsGrounded;
    public bool TryWalking;
    public bool TrySprinting;
    public bool TryCrouching;
    public bool TrySliding;
    public bool TryWallrunning;

    public PlayerState State;
    private PlayerState prevState;

    private float slowDownTimer;

    public static event Action<PlayerState> OnStateChange;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;

        PlayerComponent[] _components = GetComponents<PlayerComponent>();
        foreach (PlayerComponent _comp in _components)
        {
            _comp.Init(this);
        }
    }

    private void OnEnable()
    {
        Shooting.OnShot += SlowDown;
        ADS.OnAiming += SlowDown;
        Ammo.OnReloading += SlowDown;
    }

    private void OnDisable()
    {
        Shooting.OnShot -= SlowDown;
        ADS.OnAiming -= SlowDown;
        Ammo.OnReloading -= SlowDown;
    }

    private void Update()
    {
        HandleStates();
        if (prevState != State)
            OnStateChange?.Invoke(State);
        prevState = State;
        UIManager.Instance.UpdateState(State);

        flatVelocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);

        if (slowDownTimer > 0) slowDownTimer -= Time.deltaTime;
    }

    private void HandleStates()
    {
        if (IsGrounded)
        {
            if (TryCrouching)
                State = PlayerState.Crouching;
            else if (TrySliding)
                State = PlayerState.Sliding;
            else if (TrySprinting && TryWalking && slowDownTimer <= 0)
                State = PlayerState.Sprinting;
            else if (TryWalking)
                State = PlayerState.Walking;
            else
                State = PlayerState.Idle;
        }
        else
        {
            if (TryWallrunning && flatVelocity.magnitude > 8)
                State = PlayerState.Wallrunning;
            else if (Rb.velocity.y < 0)
                State = PlayerState.Falling;
            else
                State = PlayerState.Jumping;

        }
    }

    private void SlowDown(bool _value, float _duration)
    {
        if (_value)
            slowDownTimer = _duration;
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Sprinting,
    Crouching,
    Sliding,
    Wallrunning,
    Jumping,
    Falling
}
