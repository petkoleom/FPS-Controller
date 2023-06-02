using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PlayerComponent : MonoBehaviour
{
    protected Player player;
    protected PlayerSpecifications specs;
    protected Transform orientation;

    public void Init(Player _player, PlayerSpecifications _specs, Transform _orientation)
    {
        player = _player;
        specs = _specs;
        orientation = _orientation;
    }

}
