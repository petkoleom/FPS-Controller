using UnityEngine;

[CreateAssetMenu]
public class WeaponSpecifications : ScriptableObject
{
    [Header("General")]
    public string Name;
    public GameObject Prefab;

    public int Damage;
    public FireMode Mode;
    public int RateOfFire;

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
    Semi,
    Burst,
    Auto
}
