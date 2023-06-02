using UnityEngine;

public class Sprinting : PlayerComponent
{
    private bool sprintHeld;

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
    }
}
