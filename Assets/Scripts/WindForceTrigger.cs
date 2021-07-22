using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForceTrigger : MonoBehaviour
{
    public float XWindForce;
    public float YWindForce;
    public float ZWindForce;

    public bool isSpeedForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && isSpeedForce)
        {
            GetComponent<AudioSource>().Play();
        }

        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() == null)
            return;

        if (!isSpeedForce)
        {
            float distance = transform.position.y - other.gameObject.transform.position.y;
            float tempYWindForce = YWindForce + (distance * 10.0f);
            if (tempYWindForce < 0.0f)
            {
                tempYWindForce = 0.0f;
            }
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(XWindForce, tempYWindForce, ZWindForce));
        }
        else
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(XWindForce, YWindForce, ZWindForce));
        }
    }
}
