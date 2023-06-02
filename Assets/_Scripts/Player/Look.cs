using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Look : PlayerComponent
{
    private PlayerInput input;
    private Transform cam;

    [SerializeField] private Transform cameraPosition, cameraPrefab;

    private float xRot, yRot;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        cam = Instantiate(cameraPrefab);
        cam.GetComponent<PlayerCamera>().Init(cameraPosition);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Looking(input.Look);
    }

    private void Looking(Vector2 _look)
    {
        Vector2 _adjustedLook = Time.fixedDeltaTime * specs.Sensitivity * _look;
        var _rot = cam.rotation.eulerAngles;
        xRot -= _adjustedLook.x;
        xRot = Mathf.Clamp(xRot, -90, 90);
        yRot = _rot.y + _adjustedLook.y;

        SetRotation(xRot, yRot);
    }

    private void SetRotation(float _xRot, float _yRot)
    {
        cam.rotation = Quaternion.Euler(_xRot, _yRot, 0);
        cameraPosition.rotation = Quaternion.Euler(_xRot, _yRot, 0);
        orientation.localRotation = Quaternion.Euler(0, _yRot, 0);
    }
}
