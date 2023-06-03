using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Sliding : PlayerComponent
{
    [SerializeField] Transform cameraPosition;

    [SerializeField] private float slideScale;
    [SerializeField] private float slideForce;

    Vector3 originScale;
    private void Start()
    {
        originScale = player.Body.localScale;
    }

    private void OnEnable()
    {
        PlayerInput.OnCrouchInput += Slide;
        Jumping.OnStandUp += StandUp;
    }

    private void OnDisable()
    {
        PlayerInput.OnCrouchInput -= Slide;
        Jumping.OnStandUp -= StandUp;
    }

    private void Slide()
    {
        if (player.State != PlayerState.Sprinting) return;

        player.Rb.AddForce(slideForce * player.Orientation.forward, ForceMode.Impulse);
        player.Body.DOScaleY(slideScale, .2f);
        cameraPosition.DOMoveY(1f, .2f);
        player.TrySliding = true;

        Invoke("StandUp", .5f);
    }

    private void StandUp()
    {
        player.Body.DOScaleY(originScale.y, .2f);
        cameraPosition.DOMoveY(1.5f, .2f);

        player.TrySliding = false;
    }

}
