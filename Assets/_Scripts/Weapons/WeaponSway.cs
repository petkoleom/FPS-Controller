using DG.Tweening;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Windows;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private Transform swayTransform;
    [SerializeField] private float amount, maxAmount, smoothAmount, rotationAmount, maxRotationAmount, smoothRotationAmount;

    private Vector3 originPos;
    private Quaternion originRot;


    private void OnEnable()
    {
        PlayerInput.OnLookInput += HandleInput;
    }

    private void OnDisable()
    {
        PlayerInput.OnLookInput -= HandleInput;
    }

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
        var _moveX = Mathf.Clamp(amount * -_input.y, -maxAmount, maxAmount);
        var _moveY = Mathf.Clamp(amount * -_input.x, -maxAmount, maxAmount);
        var _finalPos = new Vector3(_moveX, _moveY, 0);

        swayTransform.localPosition = Vector3.Lerp(swayTransform.localPosition, originPos + _finalPos, Time.deltaTime * smoothAmount);
    }

    private void TiltSway(Vector2 _input)
    {
        var _tiltX = Mathf.Clamp(rotationAmount * -_input.x, -maxRotationAmount, maxRotationAmount);
        var _tiltY = Mathf.Clamp(rotationAmount * -_input.y, -maxRotationAmount, maxRotationAmount);
        var _finalRot = Quaternion.Euler(new Vector3(-_tiltX, 0, _tiltY));

        swayTransform.localRotation = Quaternion.Slerp(swayTransform.localRotation, originRot * _finalRot, Time.deltaTime * smoothRotationAmount);
    }

}
