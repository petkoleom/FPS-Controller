using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : WeaponComponent
{

    private int ammoInMag;
    private int ammoInRes;

    public static event Action<bool> OnReloading;


    private void Start()
    {
        ammoInMag = specs.MagSize;
        ammoInRes = specs.ReserveSize;

        UIManager.Instance.UpdateAmmo($"{ammoInMag} | {ammoInRes}");
    }

    public void ShotFired()
    {
        ammoInMag--;
        UIManager.Instance.UpdateAmmo($"{ammoInMag} | {ammoInRes}");
        if (ammoInMag == 0)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        OnReloading?.Invoke(true);
        yield return new WaitForSeconds(specs.ReloadDuration);

        int _amountNeeded = specs.MagSize - ammoInMag;

        if (_amountNeeded >= ammoInRes)
        {
            ammoInMag += ammoInRes;
            ammoInRes = 0;
        }
        else
        {
            ammoInMag = specs.MagSize;
            ammoInRes -= _amountNeeded;
        }

        UIManager.Instance.UpdateAmmo($"{ammoInMag} | {ammoInRes}");

        OnReloading?.Invoke(false);
    }

}
