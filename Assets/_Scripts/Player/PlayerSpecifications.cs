using UnityEngine;

[CreateAssetMenu]
public class PlayerSpecifications : ScriptableObject
{
    [Header("Movement")]
    public int WalkSpeed;
    public int SprintSpeed;
    public float airborneSpeedModifier;

    [Header("Air")]
    public int JumpForce;

    [Header("Camera")]
    public float Sensitivity;

}
