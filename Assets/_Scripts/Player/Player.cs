using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsGrounded;
    public bool TryWalking;
    public bool TrySprinting;

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
        State = PlayerState.Airborne;

        if (!IsGrounded) return;

        State = PlayerState.Idle;

        if (!TryWalking) return;

        State = PlayerState.Walking;

        if(!TrySprinting) return;

        State = PlayerState.Sprinting;

        
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Sprinting,
    Airborne
}
