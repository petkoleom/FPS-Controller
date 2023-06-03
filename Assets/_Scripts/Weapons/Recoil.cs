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
        targetRot = Vector3.Lerp(targetRot, Vector3.zero, Time.deltaTime * specs.ReturnSpeed);
        currentRot = Vector3.Slerp(currentRot, targetRot, Time.fixedDeltaTime * specs.Snapiness);
        recoilTransform.localRotation = Quaternion.Euler(currentRot);
        CalculateKickback();
    }

    private void CalculateKickback()
    {
        targetPos = Vector3.Lerp(targetPos, originPos, Time.deltaTime * specs.ReturnSpeed);
        currentPos = Vector3.Lerp(currentPos, targetPos, Time.fixedDeltaTime * specs.Snapiness);
        recoilTransform.localPosition = currentPos;
    }

    public void CalculateRecoil()
    {
        targetPos -= new Vector3(0, 0, specs.Kickback);
        targetRot += new Vector3(specs.VisualRecoil.x, Random.Range(-specs.VisualRecoil.y, specs.VisualRecoil.y), Random.Range(-specs.VisualRecoil.z, specs.VisualRecoil.z));
    }

}
