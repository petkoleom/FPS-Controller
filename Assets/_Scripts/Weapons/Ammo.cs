using System;
using System.Collections;
using UnityEngine;

public class Ammo : WeaponComponent
{

    public static event Action<bool> OnReloading;

    private void Start()
    {
        handler.Specs.AmmoInMag = handler.Specs.MagSize;
        handler.Specs.AmmoInRes = handler.Specs.ReserveSize;

        UIManager.Instance.UpdateAmmo($"{handler.Specs.AmmoInMag} | {handler.Specs.AmmoInRes}");

    }

    private void OnEnable()
    {
        WeaponHandler.OnWeaponSwitch += Setup;
        PlayerInput.OnReloadInput += Reload;
    }

    private void OnDisable()
    {
        WeaponHandler.OnWeaponSwitch -= Setup;
        PlayerInput.OnReloadInput -= Reload;
    }

    private void Setup()
    {
        UIManager.Instance.UpdateAmmo($"{handler.Specs.AmmoInMag} | {handler.Specs.AmmoInRes}");
    }


    public void ShotFired()
    {
        handler.Specs.AmmoInMag--;
        UIManager.Instance.UpdateAmmo($"{handler.Specs.AmmoInMag} | {handler.Specs.AmmoInRes}");
        if (handler.Specs.AmmoInMag == 0)
            Reload();
    }

    private void Reload()
    {
        if (handler.Specs.AmmoInMag < handler.Specs.MagSize && handler.Specs.AmmoInRes > 0)
            StartCoroutine(Reload(handler.Specs.ReloadDuration));
    }

    private IEnumerator Reload(float _duration)
    {
        OnReloading?.Invoke(true);
        handler.Animator.SetFloat("ReloadDuration", 1 / _duration);
        handler.Animator.CrossFade("Reload", 0, 0);
        yield return new WaitForSeconds(_duration);
        handler.Animator.CrossFade("Idle", 0, 0);
        int _amountNeeded = handler.Specs.MagSize - handler.Specs.AmmoInMag;

        if (_amountNeeded >= handler.Specs.AmmoInRes)
        {
            handler.Specs.AmmoInMag += handler.Specs.AmmoInRes;
            handler.Specs.AmmoInRes = 0;
        }
        else
        {
            handler.Specs.AmmoInMag = handler.Specs.MagSize;
            handler.Specs.AmmoInRes -= _amountNeeded;
        }

        UIManager.Instance.UpdateAmmo($"{handler.Specs.AmmoInMag} | {handler.Specs.AmmoInRes}");

        OnReloading?.Invoke(false);
    }

}
