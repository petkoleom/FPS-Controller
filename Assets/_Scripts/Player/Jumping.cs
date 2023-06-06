using System;
using UnityEngine;

public class Jumping : PlayerComponent
{
    public static event Action OnStandUp;

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
        if (player.State == PlayerState.Crouching || player.State == PlayerState.Sliding)
        {
            OnStandUp?.Invoke();
            return;
        }
        if (player.State == PlayerState.Jumping || player.State == PlayerState.Falling || player.State == PlayerState.Wallrunning || !_value) return;

        player.Rb.AddForce(player.Specs.JumpForce * Vector3.up, ForceMode.Impulse);
    }
}
