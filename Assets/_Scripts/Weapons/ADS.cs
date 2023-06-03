using System;
using UnityEngine;

public class ADS : WeaponComponent
{
    [SerializeField] private Transform adsTransform;
    [SerializeField] private float zOffset;
    [SerializeField] private Transform weaponPositionInSpace;

    private Vector3 originPos;

    private bool tryAiming;

    public static event Action<bool, float> OnAiming;

    private void OnEnable()
    {
        PlayerInput.OnADSInput += Aim;
    }

    private void OnDisable()
    {
        PlayerInput.OnADSInput -= Aim;
    }

    private void Start()
    {
        originPos = handler.WeaponTransform.localPosition;
    }

    private void Update()
    {
        CalculateADSPosition();
        OnAiming?.Invoke(tryAiming, .1f);
    }


    private void Aim(bool _value)
    {
        tryAiming = _value;
    }

    private void CalculateADSPosition()
    {
        var _aimPos = handler.WeaponTransform.GetChild(0).localPosition;
        var _center = -weaponPositionInSpace.localPosition - _aimPos + Vector3.forward * zOffset;
        var _targetPos = tryAiming ? _center : originPos;
        Vector3 _vel = Vector3.zero;
        adsTransform.localPosition = Vector3.SmoothDamp(adsTransform.localPosition, _targetPos, ref _vel, .01f);
    }
}
