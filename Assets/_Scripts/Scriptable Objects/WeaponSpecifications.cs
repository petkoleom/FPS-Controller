using UnityEngine;

[CreateAssetMenu]
public class WeaponSpecifications : ScriptableObject
{
    [Header("General")]
    public string Name;
    public GameObject Prefab;

    public int Damage;
    public int RateOfFire;
    public int Spread;
    public float AimSpreadModifier;
    public FireMode Mode;
    public WeaponSize Size;
    public ProjectileType Projectile;


    [Header("Recoil")]
    public float Snapiness;
    public float ReturnSpeed;
    public Vector3 VisualRecoil;
    public float Kickback;

    [Header("Ammo")]
    public float ReloadDuration;
    public int MagSize;
    public int ReserveSize;

    public int AmmoInMag { get; set; }
    public int AmmoInRes { get; set; }
}

public enum FireMode
{
    Single,
    Semi,
    Burst,
    Auto
}

public enum WeaponSize
{
    Short,
    Long
}

public enum ProjectileType
{
    Bullet,
    Pellets,
    Missile
}
