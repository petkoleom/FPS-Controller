using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private WeaponSpecifications[] loadout;


    private Transform currentWeaponTransform;
    private WeaponSpecifications currentWeapon;
    private Animator animator;

    public static event Action OnChangedWeapon;

    private void Awake()
    {

        foreach (WeaponSpecifications _weapon in loadout)
        {
            var _weaponGO = Instantiate(_weapon.Prefab, weaponHolder);
            _weaponGO.SetActive(_weaponGO.transform.GetSiblingIndex() == 0);
        }
        SetWeaponData(0);   

        SendSpecsToComponents();
    }

    private void SendSpecsToComponents()
    {
        WeaponComponent[] _components = GetComponents<WeaponComponent>();
        foreach (WeaponComponent _comp in _components)
        {
            _comp.Init(currentWeapon, animator);
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

        for (int i = 0; i < weaponHolder.childCount; i++)
            if (currentWeaponTransform == weaponHolder.GetChild(i)) _currentIdx = i;

        SetWeaponData(1 - _currentIdx);

        foreach (Transform _weapon in weaponHolder)
            _weapon.gameObject.SetActive(_weapon == currentWeaponTransform);

        SendSpecsToComponents();
    }

    private void SetWeaponData(int _idx)
    {
        currentWeaponTransform = weaponHolder.GetChild(_idx);
        currentWeapon = loadout[_idx];
        animator = currentWeaponTransform.GetComponent<Animator>();
        OnChangedWeapon?.Invoke();
    }
}
