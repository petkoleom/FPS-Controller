using UnityEngine;

public class Crouching : PlayerComponent
{
    Vector3 originScale;
    [SerializeField] private float crouchScale;



    private void Start()
    {
        originScale = transform.localScale;
    }

    private void OnEnable()
    {
        PlayerInput.OnCrouchInput += Crouch;
        Jumping.OnStandUp += StandUp;
    }

    private void OnDisable()
    {
        PlayerInput.OnCrouchInput -= Crouch;
        Jumping.OnStandUp -= StandUp;
    }

    private void Crouch()
    {
        if (player.State == PlayerState.Airborne) return;
        if (player.State == PlayerState.Crouching)
            StandUp();
        else
        {
            transform.localScale = new Vector3(1, crouchScale, 1);
            player.TryCrouching = true;
        }
    }

    private void StandUp()
    {
        transform.localScale = originScale;
        player.TryCrouching = false;

    }
}
