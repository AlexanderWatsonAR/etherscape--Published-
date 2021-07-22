using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnJointBreak : MonoBehaviour
{
    private void OnJointBreak(float breakForce)
    {
        transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
    }
}
