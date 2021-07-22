using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryParticles : MonoBehaviour
{
    ParticleSystem pS;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SphereCollider sphereCollider = player.GetComponent<SphereCollider>();

        pS = GetComponent<ParticleSystem>();
        pS.trigger.SetCollider(0, sphereCollider);
    }

    private void OnParticleTrigger()
    {
        //Debug.Log("Particle Trigger");
    }
}
