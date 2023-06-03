using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponHandler))]
public abstract class WeaponComponent : MonoBehaviour
{
    protected WeaponHandler handler;

    public void Init(WeaponHandler _handler)
    {
        handler = _handler;
    }
}
