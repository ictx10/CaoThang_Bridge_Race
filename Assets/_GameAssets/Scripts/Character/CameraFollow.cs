using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;

    [SerializeField] Vector3 offset;

    private void FixedUpdate()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTransform.position + offset, Time.deltaTime * 5f);
    }

}
