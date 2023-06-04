using System;
using UnityEngine;

public class Sprinting : PlayerComponent
{
    private bool sprintHeld;

    public static event Action OnStartSprinting;

    private bool isWalkingForward;

    private void Update()
    {
        player.TrySprinting = sprintHeld && isWalkingForward;
    }

    private void OnEnable()
    {
        PlayerInput.OnSprintInput += Sprint;
        Movement.OnWalkForward += SetWalkingForward;
    }

    private void OnDisable()
    {
        PlayerInput.OnSprintInput -= Sprint;
        Movement.OnWalkForward -= SetWalkingForward;
    }

    private void SetWalkingForward(bool _value)
    {
        isWalkingForward = _value;
    }

    private void Sprint(bool _value)
    {
        sprintHeld = _value;
        if(sprintHeld && isWalkingForward) OnStartSprinting?.Invoke();
    }
}
