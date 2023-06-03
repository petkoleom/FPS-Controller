using System;
using UnityEngine;

public class Sprinting : PlayerComponent
{
    private bool sprintHeld;

    public static event Action OnStartSprinting;

    private void Update()
    {
        player.TrySprinting = sprintHeld;
    }

    private void OnEnable()
    {
        PlayerInput.OnSprintInput += Sprint;
    }

    private void OnDisable()
    {
        PlayerInput.OnSprintInput -= Sprint;
    }

    private void Sprint(bool _value)
    {
        sprintHeld = _value;
        if(sprintHeld && player.Rb.velocity.magnitude > .01f) OnStartSprinting?.Invoke();
    }
}
