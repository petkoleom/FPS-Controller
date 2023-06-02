using System;
using UnityEngine;


public class Jumping : PlayerComponent
{
    private Rigidbody rb;

    public static event Action OnStandUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PlayerInput.OnJumpInput += Jump;
    }

    private void OnDisable()
    {
        PlayerInput.OnJumpInput -= Jump;
    }

    public void Jump(bool _value)
    {
        if (player.State == PlayerState.Crouching)
        {
            OnStandUp?.Invoke();
            return;
        }
        if (player.State == PlayerState.Airborne || !_value) return;
        rb.AddForce(specs.JumpForce * Vector3.up, ForceMode.Impulse);
    }
}
