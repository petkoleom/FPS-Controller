using UnityEngine;

public class Bobbing : WeaponComponent
{
    private Rigidbody rb;

    [SerializeField] private Transform bobbingTransform;

    private PlayerState state;

    [SerializeField]
    private float movementCounter, idleCounter, xIntensity, yIntensity;

    private Vector3 walkOriginPos;
    private Vector3 sprintOriginPos = new Vector3(-.2f, 0, -.1f);
    private Vector3 sprintOriginRot = new Vector3(20, -60, 0);

    private Vector3 targetPos;
    private Vector3 targetRot;

    private bool isAiming;


    private void OnEnable()
    {
        Player.OnStateChange += SetState;
        ADS.OnAiming += SetADS;
    }

    private void OnDisable()
    {
        Player.OnStateChange -= SetState;
        ADS.OnAiming -= SetADS;
    }

    private void SetState(PlayerState _state)
    {
        state = _state;
    }

    private void SetADS(bool _value) => isAiming = _value;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        walkOriginPos = transform.position;

    }

    private void Update()
    {
        CalculateTargetPos(rb.velocity.magnitude);
        bobbingTransform.localPosition = Vector3.Lerp(bobbingTransform.localPosition, targetPos, Time.deltaTime * 6);
        bobbingTransform.localRotation = Quaternion.Slerp(bobbingTransform.localRotation, Quaternion.Euler(targetRot), Time.deltaTime * 6);
    }

    private void CalculateTargetPos(float _velocity)
    {

        movementCounter += Time.deltaTime * (_velocity + .3f);
        if(state == PlayerState.Idle)
        {
            targetPos = walkOriginPos + new Vector3(Mathf.Cos(movementCounter) * (isAiming ? xIntensity * .1f : xIntensity) * .2f, Mathf.Sin(movementCounter * 2) * (isAiming ? yIntensity * .1f : yIntensity) * .2f, 0);
            targetRot = Vector3.zero;

        }
        else if (state == PlayerState.Walking)
        {
            targetPos = walkOriginPos + new Vector3(Mathf.Cos(movementCounter) * (isAiming ? xIntensity * .1f : xIntensity), Mathf.Sin(movementCounter * 2) * (isAiming ? yIntensity * .1f : yIntensity), 0);
            targetRot = Vector3.zero;
        }
        else if(state == PlayerState.Sprinting)
        {
            targetPos = sprintOriginPos + new Vector3(Mathf.Cos(movementCounter) * (isAiming ? xIntensity * .1f : xIntensity) * 2, Mathf.Sin(movementCounter * 2) * (isAiming ? yIntensity * .1f : yIntensity) * 2, 0);
            targetRot = sprintOriginRot;
        }
        else if(state == PlayerState.Airborne)
        {

        }
    }



}