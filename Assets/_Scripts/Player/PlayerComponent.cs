using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PlayerComponent : MonoBehaviour
{
    protected Player player;

    public void Init(Player _player)
    {
        player = _player;
    }

}
