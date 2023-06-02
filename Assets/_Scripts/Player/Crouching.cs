using DG.Tweening;
using UnityEngine;

public class Crouching : PlayerComponent
{
    [SerializeField] private float crouchScale;

    Vector3 originScale;

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
        if (player.State == PlayerState.Airborne || player.State == PlayerState.Sprinting) return;
        if (player.State == PlayerState.Crouching)
            StandUp();
        else
        {
            transform.DOScaleY(crouchScale, .2f);
            player.TryCrouching = true;
        }
    }

    private void StandUp()
    {
        transform.DOScaleY(originScale.y, .2f);
        player.TryCrouching = false;

    }
}
