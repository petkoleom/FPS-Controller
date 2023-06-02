using UnityEngine;

[CreateAssetMenu]
public class WeaponSpecifications : ScriptableObject
{
    public string Name;
    public GameObject Prefab;

    public int Damage;
    public FireMode Mode;
    public int RateOfFire;

    [Header("Ammo")]
    public float ReloadDuration;
    public int MagSize;
    public int ReserveSize;
}

public enum FireMode
{
    Semi,
    Burst,
    Auto
}
