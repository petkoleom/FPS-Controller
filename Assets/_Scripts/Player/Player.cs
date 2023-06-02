using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsGrounded;
    public bool TryWalking;
    public bool TrySprinting;
    public bool TryCrouching;

    [SerializeField] private PlayerSpecifications specs;


    public PlayerState State;
    private PlayerState prevState;

    public static event Action<PlayerState> OnStateChange;

    private void Awake()
    {
        PlayerComponent[] _components = GetComponents<PlayerComponent>();
        foreach (PlayerComponent _comp in _components)
        {
            _comp.Init(this, specs);
        }
    }

    private void Update()
    {
        HandleStates();
        if (prevState != State)
            OnStateChange?.Invoke(State);
        prevState = State;
        UIManager.Instance.UpdateState(State);
    }

    private void HandleStates()
    {
        if (IsGrounded)
        {
            if (TryCrouching)
                State = PlayerState.Crouching;
            else if (TrySprinting)
                State = PlayerState.Sprinting;
            else if (TryWalking)
                State = PlayerState.Walking;
            else
                State = PlayerState.Idle;
        }
        else
            State = PlayerState.Airborne;
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Sprinting,
    Crouching,
    Airborne
}
