using UnityEngine;

public class Shooting : WeaponComponent
{

    [SerializeField] private Transform firePoint;

    private float timeUntilNextShot;

    private bool allowFire = true;
    private bool canFire { get { return allowFire && timeUntilNextShot <= 0; } }


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
        if(specs.Mode == FireMode.Semi)
        {
            if (!_wasPerformed) return;
            Shoot();
        }
        else if(specs.Mode == FireMode.Burst)
        {

        }
        else if(specs.Mode == FireMode.Auto)
        {
            if (!_isPressed) return;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (TryGetComponent(out Ammo _ammo)) _ammo.ShotFired();
        if(Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit _hit))
        {
            if(_hit.transform.TryGetComponent(out IDamage _iDamage))
                _iDamage.TakeDamage(specs.Damage);
        }
        timeUntilNextShot = 60f / specs.RateOfFire;
    }

}
