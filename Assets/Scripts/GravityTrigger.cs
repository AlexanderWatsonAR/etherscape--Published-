using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public GameObject aGameObject;
    public GameObject lid;
    public GameObject forcePosition;
    public List<GameObject> extras;
    public AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if(lid != null)
                lid.SetActive(false);
            if (aGameObject != null)
            {
                aGameObject.GetComponent<Rigidbody>().useGravity = true;
                aGameObject.GetComponent<Rigidbody>().isKinematic = false;
            }

            if (forcePosition != null)
            {
                aGameObject.GetComponent<AddForceAtPosition>().enabled = true;
            }

            if (audio != null)
            {
                audio.Play();
            }

            foreach(GameObject extra in extras)
            {
                extra.GetComponent<Rigidbody>().useGravity = true;
                extra.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
