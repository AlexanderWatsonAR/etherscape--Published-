using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceAtPosition : MonoBehaviour
{
    public GameObject forcePosition;
    public Vector3 force;
    public ForceMode forceMode = ForceMode.Force;

    void Start()
    {
        AddForceAtPos();
    }

    public void AddForceAtPos()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(force, forcePosition.transform.position, forceMode);
    }
}
