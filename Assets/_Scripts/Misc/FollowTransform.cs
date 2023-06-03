using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        //transform.rotation = target.rotation;
        transform.position = target.position;
    }
}
