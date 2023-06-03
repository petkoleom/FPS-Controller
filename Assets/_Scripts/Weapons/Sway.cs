using DG.Tweening;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Windows;

public class Sway : MonoBehaviour
{
    [SerializeField] private Transform swayTransform;
    [SerializeField] private float amount, maxAmount, smoothAmount, rotationAmount, maxRotationAmount, smoothRotationAmount, aimModifier;

    private Vector3 originPos;
    private Quaternion originRot;

    private bool isAiming;


    private void OnEnable()
    {
        PlayerInput.OnLookInput += HandleInput;
        ADS.OnAiming += SetAiming;
    }

    private void OnDisable()
    {
        PlayerInput.OnLookInput -= HandleInput;
        ADS.OnAiming -= SetAiming;
    }

    private void SetAiming(bool _value) => isAiming = _value;

    private void Start()
    {
        originPos = swayTransform.localPosition;
        originRot = swayTransform.localRotation;
    }

    private void HandleInput(Vector2 _input)
    {
        MoveSway(_input);
        TiltSway(_input);
    }

    private void MoveSway(Vector2 _input)
    {
        var _amount = isAiming ? amount * aimModifier : amount;
        var _maxAmount = isAiming ? maxAmount * aimModifier : maxAmount;
        var _moveX = Mathf.Clamp(_amount * -_input.y, -_maxAmount, _maxAmount);
        var _moveY = Mathf.Clamp(_amount * -_input.x, -_maxAmount, _maxAmount);
        var _finalPos = new Vector3(_moveX, _moveY, 0);

        swayTransform.localPosition = Vector3.Lerp(swayTransform.localPosition, originPos + _finalPos, Time.deltaTime * smoothAmount);
    }

    private void TiltSway(Vector2 _input)
    {
        var _amount = isAiming ? rotationAmount * aimModifier : rotationAmount;
        var _maxAmount = isAiming ? maxRotationAmount * aimModifier : maxRotationAmount;

        var _tiltX = Mathf.Clamp(_amount * -_input.x, -_maxAmount, _maxAmount);
        var _tiltY = Mathf.Clamp(_amount * -_input.y, -_maxAmount, _maxAmount);
        var _finalRot = Quaternion.Euler(new Vector3(-_tiltX, 0, _tiltY));

        swayTransform.localRotation = Quaternion.Slerp(swayTransform.localRotation, originRot * _finalRot, Time.deltaTime * smoothRotationAmount);
    }

}
