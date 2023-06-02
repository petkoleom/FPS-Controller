using UnityEngine;

public class Ground : PlayerComponent
{
    public bool IsGrounded;
    private bool wasGrounded;

    [SerializeField]
    private float maxSlopeAngle = 40;

    private GameObject currentGroundObject;
    private Vector3 currentGroundNormal = Vector3.up;

    private void Update()
    {
        player.IsGrounded = IsGrounded;
        wasGrounded = IsGrounded;
    }

    private bool IsFloor(Vector3 _v)
    {
        float _angle = Vector3.Angle(Vector3.up, _v);
        return _angle < maxSlopeAngle;
    }

    private void OnCollisionStay(Collision _collisionInfo)
    {
        foreach (ContactPoint _contact in _collisionInfo.contacts)
        {
            Vector3 _normal = _contact.normal;
            if (IsFloor(_normal))
            {
                IsGrounded = true;
                currentGroundNormal = _normal;
                currentGroundObject = _contact.otherCollider.gameObject;
                return;
            }
            else if (currentGroundObject == _contact.otherCollider.gameObject)
            {
                IsGrounded = false;
                currentGroundObject = null;
            }
        }
    }

    private void OnCollisionExit(Collision _other)
    {
        if (_other.gameObject == currentGroundObject)
        {
            IsGrounded = false;
            currentGroundObject = null;
        }
    }
}
