using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Sliding : PlayerComponent
{
    private Rigidbody rb;

    [SerializeField] private float slideScale;
    [SerializeField] private float slideForce;

    Vector3 originScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        originScale = transform.localScale;
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

        rb.AddForce(slideForce * orientation.forward, ForceMode.Impulse);
        transform.DOScaleY(slideScale, .2f);
        player.TrySliding = true;

        Invoke("StandUp", .5f);
    }

    private void StandUp()
    {
        transform.DOScaleY(originScale.y, .2f);
        player.TrySliding = false;
    }

}
