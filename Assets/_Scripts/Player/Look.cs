using UnityEngine;

public class Look : PlayerComponent
{
    private Transform cam;

    [SerializeField] private Transform cameraPosition, cameraPrefab, weaponHolder;

    private float xRot, yRot;

    private void Awake()
    {
        cam = Instantiate(cameraPrefab);
        cam.GetComponent<PlayerCamera>().Init(cameraPosition);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        PlayerInput.OnLookInput += Looking;
    }

    private void OnDisable()
    {
        PlayerInput.OnLookInput -= Looking;
    }

    private void Looking(Vector2 _look)
    {
        Vector2 _adjustedLook = Time.fixedDeltaTime * player.Specs.Sensitivity * _look;
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
        weaponHolder.rotation = Quaternion.Euler(_xRot, _yRot, 0);
        player.Orientation.localRotation = Quaternion.Euler(0, _yRot, 0);
    }
}
