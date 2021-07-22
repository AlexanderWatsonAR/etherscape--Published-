using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tracker
{
    public GameObject anObject;
    public Vector3 offset;
}

public class TrackObject : MonoBehaviour
{
    public Tracker[] trackers;
    BoxCollider aBoxCollider = null;
    public bool hasPlayerReset;

    void Start()
    {
        if(GetComponent<BoxCollider>() != null)
        {
            aBoxCollider = GetComponent<BoxCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerReset)
            return;
        Track();
    }

    public void Track()
    {
        Vector3 position = transform.position;
        

        if (aBoxCollider != null)
        {
            foreach (Tracker tracker in trackers)
            {
                float maxY = aBoxCollider.bounds.size.y;
                tracker.anObject.transform.position = new Vector3(position.x + tracker.offset.x, position.y + tracker.offset.y + maxY, position.z + tracker.offset.z);
            }
        }
        else
        {
            foreach (Tracker tracker in trackers)
            {
                tracker.anObject.transform.position = new Vector3(position.x + tracker.offset.x, position.y + tracker.offset.y, position.z + tracker.offset.z);
            }
        }
    }
         
}
