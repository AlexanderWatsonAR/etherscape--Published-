using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVTrigger : MonoBehaviour
{
    public float wide = 100.0f;
    public float normal = 90.0f;

    public float far = -2.8f;
    public float close = -2.5f;

    public float high = 1.5f;
    public float low = 1.5f;

    public float lookLow = 12.0f;
    public float lookNormal = 12.0f;

    public float centrePoint = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (StartScene.IsPortrait && other.name == "Player")
        {
            UpdateFOV.targetFOV = wide;
            UpdateFOV.targetDistance = far;
            UpdateFOV.targetHeight = high;
            UpdateFOV.targetRotation = lookLow;
            UpdateFOV.targetCentrePoint = centrePoint;
            UpdateFOV.hasFovChanged = true;
            UpdateFOV.rotationAtTarget = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (StartScene.IsPortrait && other.name == "Player")
        { 
            UpdateFOV.targetFOV = normal;
            UpdateFOV.targetDistance = close;
            UpdateFOV.targetHeight = low;
            UpdateFOV.targetRotation = lookNormal;
            UpdateFOV.targetCentrePoint = 0.0f;
            UpdateFOV.rotationAtTarget = false;
        }
    }
}
