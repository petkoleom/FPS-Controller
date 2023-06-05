using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    public GameObject BulletHole;

    private void Awake()
    {
        Instance = this;
    }


}
