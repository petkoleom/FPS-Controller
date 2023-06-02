using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    protected WeaponSpecifications specs;
    protected Animator animator;

    public void Init(WeaponSpecifications _specs, Animator _animator)
    {
        specs = _specs;
        animator = _animator;
    }
}
