using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private UnityEngine.InputSystem.PlayerInput input;

    // Movement Input
    public static event Action<Vector2> OnMoveInput;
    public static event Action<Vector2> OnLookInput;

    public static event Action<bool> OnJumpInput;
    public static event Action<bool> OnSprintInput;
    public static event Action OnCrouchInput;

    // Weapon Input
    public static event Action OnSwitchInput;
    public static event Action<bool, bool> OnFireInput;

    private void Awake()
    {
        input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

    private void Update()
    {
        LookInput();
        SprintInput();
        FireInput();
    }

    private void FixedUpdate()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        var _forward = input.actions["Move"].ReadValue<Vector2>().y;
        var _side = input.actions["Move"].ReadValue<Vector2>().x;
        OnMoveInput?.Invoke(new Vector2(_forward, _side));
    }

    private void LookInput()
    {
        var _hor = input.actions["Look"].ReadValue<Vector2>().y;
        var _ver = input.actions["Look"].ReadValue<Vector2>().x;
        OnLookInput?.Invoke(new Vector2(_hor, _ver));
    }

    private void SprintInput()
    {
        OnSprintInput?.Invoke(input.actions["Sprint"].IsPressed());
    }

    private void FireInput()
    {
        var _action = input.actions["Fire"];
        OnFireInput?.Invoke(_action.IsPressed(), _action.WasPressedThisFrame());
    }

    public void OnJump(InputValue _value)
    {
        OnJumpInput?.Invoke(_value.isPressed);
    }

    public void OnCrouch(InputValue _value)
    {
        OnCrouchInput?.Invoke();
    }

    public void OnSwitch(InputValue _value)
    {
        OnSwitchInput?.Invoke();
    }



}
