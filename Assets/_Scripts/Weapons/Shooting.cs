using System;
using UnityEngine;

public class Shooting : WeaponComponent
{

    [SerializeField] private Transform firePoint;

    private float timeUntilNextShot;

    private bool allowFire = true;
    private bool canFire { get { return allowFire && timeUntilNextShot <= 0; } }

    public static event Action<bool> OnShot;


    private void OnEnable()
    {
        PlayerInput.OnFireInput += HandleFireInput;
        Ammo.OnReloading += SetAllowFire;
    }

    private void OnDisable()
    {
        PlayerInput.OnFireInput -= HandleFireInput;
        Ammo.OnReloading -= SetAllowFire;
    }

    private void SetAllowFire(bool _value)
    {
        allowFire = !_value;
    }

    private void Update()
    {
        if (timeUntilNextShot > .0f)
        {
            timeUntilNextShot -= Time.deltaTime;
        }

    }

    private void HandleFireInput(bool _isPressed, bool _wasPerformed)
    {
        if (!canFire) return;
        if(handler.Specs.Mode == FireMode.Semi)
        {
            if (!_wasPerformed) return;
            Shoot();
        }
        else if(handler.Specs.Mode == FireMode.Burst)
        {

        }
        else if(handler.Specs.Mode == FireMode.Auto)
        {
            if (!_isPressed) return;
            Shoot();
        }
    }

    private void Shoot()
    {
        OnShot?.Invoke(true);
        if (TryGetComponent(out Ammo _ammo)) _ammo.ShotFired();
        if(Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit _hit))
        {
            if(_hit.transform.TryGetComponent(out IDamage _iDamage))
                _iDamage.TakeDamage(handler.Specs.Damage);
        }
        timeUntilNextShot = 60f / handler.Specs.RateOfFire;
    }

}
