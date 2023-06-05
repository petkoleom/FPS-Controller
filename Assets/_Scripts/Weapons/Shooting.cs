using System;
using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Shooting : WeaponComponent
{

    [SerializeField] private Transform firePoint;

    private ParticleSystem muzzleflash;

    private float timeUntilNextShot;

    private bool allowFire = true;
    private bool canFire { get { return allowFire && timeUntilNextShot <= 0; } }
    private bool isAiming;

    public static event Action<bool, float> OnShot;

    private void OnEnable()
    {
        PlayerInput.OnFireInput += HandleFireInput;
        Ammo.OnReloading += SetAllowFire;
        WeaponHandler.OnWeaponSwitch += Setup;
        ADS.OnAiming += SetIsAiming;
    }

    private void OnDisable()
    {
        PlayerInput.OnFireInput -= HandleFireInput;
        Ammo.OnReloading -= SetAllowFire;
        WeaponHandler.OnWeaponSwitch -= Setup;
        ADS.OnAiming -= SetIsAiming;
    }

    private void Start()
    {
        Setup(handler.Specs.Projectile);
    }

    private void Setup(ProjectileType _type)
    {
        muzzleflash = handler.WeaponTransform.GetComponentInChildren<ParticleSystem>();
    }

    private void SetAllowFire(bool _value, float _duration)
    {
        allowFire = !_value;
    }

    private void SetIsAiming(bool _value, float _float) => isAiming = _value;

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
        if (handler.Specs.Mode == FireMode.Single)
        {
            if (!_wasPerformed) return;
            Shoot();
            StartCoroutine(PlayCycleAnimation((60f / handler.Specs.RateOfFire)));
        }
        if (handler.Specs.Mode == FireMode.Semi)
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

    private IEnumerator PlayCycleAnimation(float _duration)
    {
        handler.Animator.SetFloat("CycleDuration", 1 / _duration);
        handler.Animator.CrossFade("Cycle", 0, 0);
        yield return new WaitForSeconds(_duration);
        handler.Animator.CrossFade("Idle", 0, 0);
    }

    private void Shoot()
    {
        OnShot?.Invoke(true, .2f);
        if (TryGetComponent(out Ammo _ammo)) _ammo.ShotFired();
        muzzleflash.Play();

        if (handler.Specs.Projectile == ProjectileType.Bullet)
            RaycastShot();
        else if (handler.Specs.Projectile == ProjectileType.Pellets)
        {
            for (int i = 0; i < 10; i++)
            {
                RaycastShot();
            }
        }
        else if (handler.Specs.Projectile == ProjectileType.Missile)
        {

        }

        timeUntilNextShot = 60f / handler.Specs.RateOfFire;

    }

    private void RaycastShot()
    {
        var _trajectory = firePoint.position + firePoint.forward * 1000f;

        float _randomDeviation1 = UnityEngine.Random.Range(-handler.Specs.Spread, handler.Specs.Spread);
        float _randomDeviation2 = UnityEngine.Random.Range(-handler.Specs.Spread, handler.Specs.Spread);

        _randomDeviation1 = isAiming ? _randomDeviation1 * handler.Specs.AimSpreadModifier : _randomDeviation1;
        _randomDeviation2 = isAiming ? _randomDeviation2 * handler.Specs.AimSpreadModifier : _randomDeviation2;


        _trajectory += _randomDeviation1 * firePoint.up;
        _trajectory += _randomDeviation2 * firePoint.right;

        _trajectory -= firePoint.position;
        _trajectory.Normalize();

        if (Physics.Raycast(firePoint.position, _trajectory, out RaycastHit _hit))
        {
            Bullethole(_hit);
            if (_hit.transform.TryGetComponent(out IDamage _iDamage))
                _iDamage.TakeDamage(handler.Specs.Damage);
        }
    }

    private void Bullethole(RaycastHit _hit)
    {
        GameObject _bulletHole = Instantiate(AssetManager.Instance.BulletHole, _hit.point + _hit.normal * 0.001f, Quaternion.identity);
        _bulletHole.transform.LookAt(_hit.point + _hit.normal);
        _bulletHole.transform.parent = _hit.transform;
        Destroy(_bulletHole, 60);
    }

}
