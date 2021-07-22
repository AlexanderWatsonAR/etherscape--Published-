using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindBehaviour : MonoBehaviour
{
    GameObject player;
    ParticleSystem wind;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        wind = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Rigidbody>().velocity.magnitude > 3.0f)
        {
            var main = wind.main;
            main.startSpeed = player.GetComponent<Rigidbody>().velocity.magnitude / 5;
            Vector3 direction = player.GetComponent<Rigidbody>().velocity.normalized;
            Vector3 futurePos = transform.position + (direction * 10);
            Vector3 relativePos = transform.position - futurePos;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        }
        else
        {
            var main = wind.main;
            main.startSpeed = 0.0f;
        }
           
    }
}
