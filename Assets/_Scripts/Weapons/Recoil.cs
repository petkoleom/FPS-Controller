using UnityEngine;

public class Recoil : WeaponComponent
{
    [SerializeField] private Transform recoilTransform;

    private Vector3 currentRot, targetRot, currentPos, targetPos, originPos;

    private void Awake()
    {
        originPos = recoilTransform.localPosition;
    }

    private void OnEnable()
    {
        Shooting.OnShot += CalculateRecoil;
    }

    private void OnDisable()
    {
        Shooting.OnShot -= CalculateRecoil;
    }

    private void Update()
    {
        targetRot = Vector3.Lerp(targetRot, Vector3.zero, Time.deltaTime * handler.Specs.ReturnSpeed);
        currentRot = Vector3.Slerp(currentRot, targetRot, Time.fixedDeltaTime * handler.Specs.Snapiness);
        recoilTransform.localRotation = Quaternion.Euler(currentRot);
        CalculateKickback();
    }

    private void CalculateKickback()
    {
        targetPos = Vector3.Lerp(targetPos, originPos, Time.deltaTime * handler.Specs.ReturnSpeed);
        currentPos = Vector3.Lerp(currentPos, targetPos, Time.fixedDeltaTime * handler.Specs.Snapiness);
        recoilTransform.localPosition = currentPos;
    }

    public void CalculateRecoil(bool _value, float _duration)
    {
        targetPos -= new Vector3(0, 0, handler.Specs.Kickback);
        targetRot += new Vector3(handler.Specs.VisualRecoil.x, Random.Range(-handler.Specs.VisualRecoil.y, handler.Specs.VisualRecoil.y), Random.Range(-handler.Specs.VisualRecoil.z, handler.Specs.VisualRecoil.z));
    }

}
