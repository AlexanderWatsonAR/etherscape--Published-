using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetForceTrigger : MonoBehaviour
{
    public float strength;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Metalic")
        {
            Vector3 direction = Vector3.Normalize(transform.position - other.transform.position);
            transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(direction * -(strength));
            if(other.attachedRigidbody != null)
                other.attachedRigidbody.AddForce(direction * (strength / 2));
        }
    }

}
