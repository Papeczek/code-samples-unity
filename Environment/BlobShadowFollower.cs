using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadowFollower : MonoBehaviour
{
    [SerializeField] bool updateRotation = false;
    [SerializeField] private Transform transformToFollow;
    [SerializeField] private Vector3 positionOffset;
    private float rightAngle = 90f;
    private void FixedUpdate()
    {
        UpdatePosition();
        if (!updateRotation) return;
        UpdateRotation();
    }
    private void UpdateRotation()
    {
        if (transform.rotation.y != transformToFollow.rotation.y + rightAngle)
            transform.rotation = Quaternion.Euler(0, transformToFollow.eulerAngles.y + rightAngle, 0);
    }
    private void UpdatePosition()
    {
        if (transform.position != transformToFollow.position + positionOffset)
            transform.position = transformToFollow.position + positionOffset;
    }
}

