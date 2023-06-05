using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Transform WeaponHolder;
    public Transform WeaponsParent;
    public WeaponSpecifications[] Loadout;


    public Transform WeaponTransform { get; set; }
    public WeaponSpecifications Specs { get; set; }
    public Animator Animator { get; set; }

    public static event Action<ProjectileType> OnWeaponSwitch;


    private void Awake()
    {

        foreach (WeaponSpecifications _weapon in Loadout)
        {
            var _weaponGO = Instantiate(_weapon.Prefab, WeaponHolder);
            _weaponGO.SetActive(_weaponGO.transform.GetSiblingIndex() == 0);
        }
        SetWeaponData(0);

        WeaponComponent[] _components = GetComponents<WeaponComponent>();
        foreach (WeaponComponent _comp in _components)
        {
            _comp.Init(this);
        }
    }

    private void OnEnable()
    {
        PlayerInput.OnSwitchInput += SwitchWeapon;
    }

    private void OnDisable()
    {
        PlayerInput.OnSwitchInput -= SwitchWeapon;
    }

    private void SwitchWeapon()
    {
        int _currentIdx = 0;

        for (int i = 0; i < WeaponHolder.childCount; i++)
            if (WeaponTransform == WeaponHolder.GetChild(i)) _currentIdx = i;

        SetWeaponData(1 - _currentIdx);

        foreach (Transform _weapon in WeaponHolder)
            _weapon.gameObject.SetActive(_weapon == WeaponTransform);

        OnWeaponSwitch?.Invoke(Specs.Projectile);

    }

    private void SetWeaponData(int _idx)
    {
        WeaponTransform = WeaponHolder.GetChild(_idx);
        Specs = Loadout[_idx];
        Animator = WeaponTransform.GetComponent<Animator>();
    }
}
