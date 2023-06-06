using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Camera cam;
    private Transform camPosition;
    private Transform thisTransform;

    private float defaultFOV;

    private void OnEnable()
    {
        Wallrunning.OnWallRunning += DoTilt;
        ADS.OnAiming += DoFOV;
    }

    private void OnDisable()
    {
        Wallrunning.OnWallRunning -= DoTilt;
        ADS.OnAiming -= DoFOV;
    }

    public void Init(Transform _camPos)
    {
        thisTransform = transform;
        camPosition = _camPos;
        cam = transform.GetComponentInChildren<Camera>();
        defaultFOV = cam.fieldOfView;
    }

    private void Update()
    {
        thisTransform.position = camPosition.position;
    }

    private void DoFOV(bool _value, float _target)
    {
        var _fov = _value ? defaultFOV * .8f : defaultFOV;
        cam.DOFieldOfView(_fov, .1f);
    }

    private void DoTilt(bool _onWall, bool _side)
    {
        var _tilt = _side ? new Vector3(0, 0, 3) : new Vector3(0, 0, -3);
        var _doTilt = _onWall ? _tilt : Vector3.zero;
        cam.transform.DOLocalRotate(_doTilt, .2f);
    }

}
