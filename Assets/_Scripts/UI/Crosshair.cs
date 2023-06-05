using UnityEngine;
using static UnityEditor.Progress;

public class Crosshair : MonoBehaviour
{

    public static Crosshair Instance;

    [SerializeField] private RectTransform[] crosshairs;

    private int activeIdx = 0;

    private void Awake()
    {
        Instance = this;
        SetActive();

    }

    private void OnEnable()
    {
        WeaponHandler.OnWeaponSwitch += SetCrosshair;
        ADS.OnAiming += SetIsAiming;
    }

    private void OnDisable()
    {
        WeaponHandler.OnWeaponSwitch -= SetCrosshair;
        ADS.OnAiming -= SetIsAiming;
    }

    private void Update()
    {
        UpdateCrosshair();
    }

    private void SetCrosshair(ProjectileType _type)
    {
        switch (_type)
        {
            case ProjectileType.Bullet: activeIdx = 0; break;
            case ProjectileType.Pellets: activeIdx = 1; break;
            case ProjectileType.Missile: activeIdx = 2; break;
        }
        SetActive();
    }

    private void SetActive()
    {
        for (int i = 0; i < crosshairs.Length; i++)
        {
            crosshairs[i].gameObject.SetActive(activeIdx == i);
        }

    }


    [SerializeField] private float originSize, changeSpeed;
    private float currentSize, targetSize, oldTargetSize, currentAlpha, targetAlpha;

    private bool isAiming;

    public void SetVelocity(int _velocity)
    {
        if (isAiming) return;
        targetSize = originSize + _velocity * 10;
        oldTargetSize = targetSize;
    }

    private void SetIsAiming(bool _value, float _float) => isAiming = _value;

    public void UpdateCrosshair()
    {
        targetSize = isAiming ? 0 : oldTargetSize;
        targetAlpha = isAiming ? 0 : 1;
        float _vel = 0;
        float _vel2 = 0;

        currentSize = Mathf.SmoothDamp(currentSize, targetSize, ref _vel, changeSpeed);
        crosshairs[activeIdx].sizeDelta = new Vector2(currentSize, currentSize);

        currentAlpha = Mathf.SmoothDamp(currentAlpha, targetAlpha, ref _vel2, changeSpeed / 2);
        crosshairs[activeIdx].GetComponent<CanvasGroup>().alpha = currentAlpha;

    }

}
