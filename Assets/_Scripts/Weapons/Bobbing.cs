using UnityEngine;

public class Bobbing : WeaponComponent
{
    private Rigidbody rb;

    [SerializeField] private Transform bobbingTransform;

    private PlayerState state;

    [SerializeField]
    private float movementCounter, idleCounter, xIntensity, yIntensity;

    private Vector3 walkOriginPos;
    private Vector3 sprintOriginPosShort = new Vector3(-.1f, -.05f, 0);
    private Vector3 sprintOriginRotShort = new Vector3(-70, -10, 0);
    private Vector3 sprintOriginPosLong = new Vector3(-.2f, 0, -.1f);
    private Vector3 sprintOriginRotLong = new Vector3(20, -60, 0);

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

    private void SetADS(bool _value, float _duration) => isAiming = _value;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        walkOriginPos = transform.position;

    }

    private void Update()
    {
        CalculateTargetPos(rb.velocity.magnitude);
        var _lerpSpeed = state == PlayerState.Falling || state == PlayerState.Airborne ? 3 : 8;
        bobbingTransform.localPosition = Vector3.Lerp(bobbingTransform.localPosition, targetPos, Time.deltaTime * _lerpSpeed);
        bobbingTransform.localRotation = Quaternion.Slerp(bobbingTransform.localRotation, Quaternion.Euler(targetRot), Time.deltaTime * _lerpSpeed);
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
            var _originPos = Vector3.zero;
            var _originRot = Vector3.zero;
            if (handler.Specs.Size == WeaponSize.Short)
            {
                _originPos = sprintOriginPosShort;
                _originRot = sprintOriginRotShort;
            }
            else if(handler.Specs.Size == WeaponSize.Long)
            {
                _originPos = sprintOriginPosLong;
                _originRot = sprintOriginRotLong;

            }


            targetPos = _originPos + new Vector3(Mathf.Cos(movementCounter) * (isAiming ? xIntensity * .1f : xIntensity) * 2, Mathf.Sin(movementCounter * 2) * (isAiming ? yIntensity * .1f : yIntensity) * 2, 0);
            targetRot = _originRot;
        }
        else if (state == PlayerState.Sliding)
        {
            targetPos = walkOriginPos + Vector3.down * .2f;
            targetRot = Vector3.zero;

        }
        else if(state == PlayerState.Airborne)
        {
            targetPos = walkOriginPos + Vector3.down * .2f;
        }
        else if(state == PlayerState.Falling)
        {
            targetPos = walkOriginPos + Vector3.up * .2f;

        }
    }



}
