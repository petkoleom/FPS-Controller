using UnityEngine;

[RequireComponent (typeof(Player))]
public class Jumping : PlayerComponent
{
    private Rigidbody rb;

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
        if (player.State == PlayerState.Airborne || !_value) return;
        rb.AddForce(specs.JumpForce * Vector3.up, ForceMode.Impulse);
    }
}
