using DG.Tweening;
using UnityEngine;

public class Crouching : PlayerComponent
{
    [SerializeField] private float crouchScale;

    [SerializeField] Transform cameraPosition;

    Vector3 originScale;

    bool isCrouching;

    private void Start()
    {
        originScale = player.Body.localScale;
    }

    private void OnEnable()
    {
        PlayerInput.OnCrouchInput += Crouch;
        Jumping.OnStandUp += StandUp;
        Sprinting.OnStartSprinting += StandUp;
    }

    private void OnDisable()
    {
        PlayerInput.OnCrouchInput -= Crouch;
        Jumping.OnStandUp -= StandUp;
        Sprinting.OnStartSprinting -= StandUp;
    }

    private void Crouch()
    {
        if (player.State == PlayerState.Airborne || player.State == PlayerState.Sprinting || player.State == PlayerState.Sliding) return;
        if (player.State == PlayerState.Crouching)
            StandUp();
        else
        {
            isCrouching = true;
            player.Body.DOScaleY(crouchScale, .2f);
            cameraPosition.DOLocalMoveY(1f, .2f);
            player.TryCrouching = true;
        }
    }

    private void StandUp()
    {
        if (!isCrouching) return;
        isCrouching = false;
        player.Body.DOScaleY(originScale.y, .2f);
        cameraPosition.DOLocalMoveY(1.5f, .2f);
        player.TryCrouching = false;

    }
}
