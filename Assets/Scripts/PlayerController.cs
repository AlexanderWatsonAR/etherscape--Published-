using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public ConstantForce force;
    public static float  Multiplier = 3.0f;

    // Update is called once per frame
    void Update()
    {
        force.force = new Vector3(joystick.Horizontal * Multiplier, 0.0f, joystick.Vertical * Multiplier);

        ScoreManagerCloudOnce.Distance = (int)transform.position.z;
    }
}
