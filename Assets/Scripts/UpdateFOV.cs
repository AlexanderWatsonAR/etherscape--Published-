using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Also updates camera distance, height and rotation.
public class UpdateFOV : MonoBehaviour
{
    [HideInInspector]
    public static float targetFOV = 90.0f;
    [HideInInspector]
    public static float targetDistance = -2.5f;
    [HideInInspector]
    public static float targetHeight = 1.5f;
    [HideInInspector]
    public static float targetRotation = 12.0f;
    [HideInInspector]
    public static bool hasFovChanged = false;
    [HideInInspector]
    public static float targetCentrePoint = 0.0f;
    [HideInInspector]
    public static bool rotationAtTarget = false;

    Camera mainCamera;
    Tracker playerTracker;

    float speed = 5.0f;
    float cameraSpeed = 0.3f;

    Animator mator;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        mator = GetComponent<Animator>();
        playerTracker = GameObject.Find("Player").GetComponent<TrackObject>().trackers[0];

        if (StartScene.IsPortrait)
            targetFOV = 90.0f;
        else
            targetFOV = 60.0f;

        speed *= Time.deltaTime;
        cameraSpeed *= Time.deltaTime;
    }

    private void Update()
    {
        if (!StartScene.IsPortrait)
            return;
        if (!hasFovChanged)
            return;

        if (targetFOV != mainCamera.fieldOfView)
        {
            float previousFOV = mainCamera.fieldOfView;

            if (targetFOV > mainCamera.fieldOfView)
                mainCamera.fieldOfView += speed;
            else
                mainCamera.fieldOfView -= speed;

            if (previousFOV < targetFOV && Camera.main.fieldOfView > targetFOV)
                mainCamera.fieldOfView = targetFOV;
        }

        if (targetCentrePoint != playerTracker.offset.x)
        {
            float previousCentrePoint = playerTracker.offset.x;

            if (targetCentrePoint > playerTracker.offset.x)
                playerTracker.offset.x += cameraSpeed * 2;
            else
                playerTracker.offset.x -= cameraSpeed * 2;

            if (previousCentrePoint < targetCentrePoint && playerTracker.offset.x > targetCentrePoint)
                playerTracker.offset.x = previousCentrePoint;
        }

        if (targetHeight != playerTracker.offset.y)
        {
            float previousHeight = playerTracker.offset.y;

            if (targetHeight > playerTracker.offset.y)
                playerTracker.offset.y += cameraSpeed;
            else
                playerTracker.offset.y -= cameraSpeed;

            if (previousHeight < targetHeight && playerTracker.offset.y > targetHeight)
                playerTracker.offset.y = targetHeight;
        }

        if (targetDistance != playerTracker.offset.z)
        {
            float previousDistance = playerTracker.offset.z;

            if (targetDistance < playerTracker.offset.z)
                playerTracker.offset.z -= cameraSpeed;
            else
                playerTracker.offset.z += cameraSpeed;

            if (previousDistance < targetDistance && playerTracker.offset.z > targetDistance)
                playerTracker.offset.z = targetDistance;
        }

        if (targetRotation != mainCamera.transform.eulerAngles.x && !rotationAtTarget)
        {
            mator.enabled = false;
            float previousRotation = mainCamera.transform.eulerAngles.x;

            if (targetRotation > Camera.main.transform.eulerAngles.x)
                mainCamera.transform.eulerAngles = new Vector3(mainCamera.transform.eulerAngles.x + speed, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);
            else
                mainCamera.transform.eulerAngles = new Vector3(mainCamera.transform.eulerAngles.x - speed, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);

            if (previousRotation < targetRotation && mainCamera.transform.eulerAngles.x > targetRotation)
            {
                mainCamera.transform.eulerAngles = new Vector3(targetRotation, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);
                rotationAtTarget = true;
            }
        }
    }
}
