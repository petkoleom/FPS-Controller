using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private Transform camPosition;
    private Transform thisTransform;

    public void Init(Transform _camPos)
    {
        thisTransform = transform;
        camPosition = _camPos;
    }

    private void Update()
    {
        thisTransform.position = camPosition.position;
    }
}
