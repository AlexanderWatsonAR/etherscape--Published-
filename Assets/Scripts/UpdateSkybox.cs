using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSkybox : MonoBehaviour
{
    float maxExposure = 1.09f;
    float minExposure = 0.93f;
    float step = 0.0005f;
    float exposure = 1.0f;

    float rotateX = -0.001f;

    float minRed = 0.5f;
    float maxRed = 0.62f;
    float redStep = 0.00005f;
    Color skyBoxColor = new Color(0.5f, 0.5f, 0.5f);

    void Start()
    {
        float rotation = Random.Range(0.0f, 360.0f);
        rotateX = (-0.55f)*Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseGame.isGamePaused)
            return;
        FluctuateExposure();
        RotateBox();

        if (GameStopWatch.TimeElapsedInSeconds > 30.0f)
            ModulateTint();
    }

    void FluctuateExposure()
    {
        if(RenderSettings.skybox.GetFloat("_Exposure") < minExposure ||
            RenderSettings.skybox.GetFloat("_Exposure") > maxExposure)
        {
            step = -step;
        }

        exposure += step;

        RenderSettings.skybox.SetFloat("_Exposure", exposure);
    }

    void ModulateTint()
    {

        if (RenderSettings.skybox.GetColor("_Tint").r < minRed ||
            RenderSettings.skybox.GetColor("_Tint").r > maxRed)
        {
            redStep = -redStep;
        }

        skyBoxColor.r += redStep;

        RenderSettings.skybox.SetColor("_Tint", skyBoxColor);
    }

    void RotateBox()
    {
        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");
        currentRotation = currentRotation > 360.0f ? 0.0f : currentRotation;
        float newRotation = currentRotation + rotateX;
        RenderSettings.skybox.SetFloat("_Rotation", newRotation);
    }

    private void OnApplicationQuit()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 1.0f);
        RenderSettings.skybox.SetFloat("_Rotation", 0.0f);
        RenderSettings.skybox.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
    }
}
