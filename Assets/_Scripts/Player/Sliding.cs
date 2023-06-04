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

    private float slideTimer;

    private bool isSliding;

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

    private void Update()
    {
        if (slideTimer > 0) slideTimer -= Time.deltaTime;
    }

    private void Slide()
    {
        if (player.State != PlayerState.Sprinting || slideTimer > 0 || isSliding) return;

        player.TrySliding = true;
        isSliding = true;
        slideTimer = .8f;
        player.Rb.AddForce(slideForce * player.Orientation.forward, ForceMode.Impulse);
        player.Body.DOScaleY(slideScale, .2f);
        cameraPosition.DOLocalMoveY(1f, .2f);


        Invoke("StandUp", .5f);
    }

    private void StandUp()
    {
        player.Body.DOScaleY(originScale.y, .2f);
        cameraPosition.DOLocalMoveY(1.5f, .2f);

        player.TrySliding = false;
        isSliding = false;
    }

}
