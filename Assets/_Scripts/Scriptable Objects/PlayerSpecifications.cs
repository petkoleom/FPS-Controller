using UnityEngine;

[CreateAssetMenu]
public class PlayerSpecifications : ScriptableObject
{
    [Header("Movement")]
    public int WalkSpeed;
    public int SprintSpeed;
    public int CrouchSpeed;
    public int SlideSpeed;
    public int wallrunSpeed;
    public float airborneSpeedModifier;

    [Header("Air")]
    public int JumpForce;

    [Header("Camera")]
    public float Sensitivity;

}
