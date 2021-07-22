using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    public Vector2 xRandomSpeedRange;
    public Vector2 yRandomSpeedRange;
    public Vector2 zRandomSpeedRange;

    public bool isRotateSpeedRandom = false;

    protected AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(isRotateSpeedRandom == true)
        {
            xSpeed = Random.Range(xRandomSpeedRange.x, xRandomSpeedRange.y);
            ySpeed = Random.Range(yRandomSpeedRange.x, yRandomSpeedRange.y);
            zSpeed = Random.Range(zRandomSpeedRange.x, zRandomSpeedRange.y);
        }

        xSpeed = xSpeed * Time.deltaTime;
        ySpeed = ySpeed * Time.deltaTime;
        zSpeed = zSpeed * Time.deltaTime;

        if (GameStopWatch.TimeElapsedInSeconds > 60 && GameStopWatch.TimeElapsedInSeconds < 240)
        {
            xSpeed = xSpeed * 1.5f;
            ySpeed = ySpeed * 1.5f;
            zSpeed = zSpeed * 1.5f;
        }

        if (GameStopWatch.TimeElapsedInSeconds >= 240)
        {
            xSpeed = xSpeed * 2;
            ySpeed = ySpeed * 2;
            zSpeed = zSpeed * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseGame.isGamePaused)
        {
            return;
        }
        ModifyRotation();
        transform.Rotate(xSpeed, ySpeed, zSpeed);
    }

    protected virtual void ModifyRotation()
    {
        
    }

}
