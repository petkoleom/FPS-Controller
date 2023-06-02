using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PlayerComponent : MonoBehaviour
{
    protected Player player;

    protected PlayerSpecifications specs;

    public void Init(Player _player, PlayerSpecifications _specs)
    {
        player = _player;
        specs = _specs;
    }

}
