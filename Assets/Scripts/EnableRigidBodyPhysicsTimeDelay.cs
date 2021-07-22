using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRigidBodyPhysicsTimeDelay : MonoBehaviour
{
    public float time = 0.5f;
    public GameObject nextGameObject;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();

        if(nextGameObject != null)
            nextGameObject.GetComponent<EnableRigidBodyPhysicsTimeDelay>().enabled = true;
    }
}
