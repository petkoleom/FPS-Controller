using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private UnityEngine.InputSystem.PlayerInput input;
    public Transform Orientation;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public static event Action<bool> OnJumpInput;
    public static event Action<bool> OnSprintInput;

    public bool Sprint;


    private void Awake()
    {
        input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

    private void Update()
    {
        MoveInput();
        LookInput();
        SprintInput();
    }

    private void MoveInput()
    {
        var _forward = input.actions["Move"].ReadValue<Vector2>().y;
        var _side = input.actions["Move"].ReadValue<Vector2>().x;
        Move = new Vector2(_forward, _side);

    }

    private void LookInput()
    {
        var _hor = input.actions["Look"].ReadValue<Vector2>().y;
        var _ver = input.actions["Look"].ReadValue<Vector2>().x;
        Look = new Vector2(_hor, _ver);
    }

    private void SprintInput()
    {
        Sprint = input.actions["Sprint"].IsPressed();
        OnSprintInput?.Invoke(Sprint);
    }


    public void OnJump(InputValue _value)
    {
        OnJumpInput?.Invoke(_value.isPressed);
    }

}
