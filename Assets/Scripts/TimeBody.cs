using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    bool isRewinding = false;

    public float recordTime = 3.0f;
    public float rewindAfter = 10.0f;
    float elapsedTime = 0.0f;

    List<PointInTime> pointsInTime;

    Rigidbody rb;
    RigidbodyProperties startStateRB;
    FixedJoint fj;
    FixedJointProperties startStateFJ;

    // Use this for initialization
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        startStateRB = new RigidbodyProperties(rb);

        fj = GetComponent<FixedJoint>();
        if(fj != null)
            startStateFJ = new FixedJointProperties(fj);
    }

    void FixedUpdate()
    {
        if (PauseGame.isGamePaused)
            return;

        if (isRewinding)
            Rewind();
        else if (transform.localPosition != Vector3.zero || transform.localRotation != Quaternion.identity)
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.localPosition = pointInTime.localPosition;
            transform.localRotation = pointInTime.localRotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            if (fj == null && startStateFJ != null)
            {
                gameObject.AddComponent<FixedJoint>();
                fj = gameObject.GetComponent<FixedJoint>();

                fj.autoConfigureConnectedAnchor = startStateFJ.autoConfigureConnectedAnchor;
                fj.breakForce = startStateFJ.breakForce;
                fj.breakTorque = startStateFJ.breakTorque;
                fj.connectedAnchor = startStateFJ.connectedAnchor;
                fj.connectedBody = startStateFJ.connectedBody;
                fj.enablePreprocessing = startStateFJ.enablePreprocessing;

                fj.connectedMassScale = startStateFJ.connectedMassScale;
                fj.enableCollision = startStateFJ.enableCollision;
            }
            StopRewind();
        }

    }

    void Record()
    {
        if (elapsedTime < recordTime)
        {
            pointsInTime.Insert(0, new PointInTime(transform.localPosition, transform.localRotation));
            
        }
        if(elapsedTime > rewindAfter)
        {
            StartRewind();
        }

        elapsedTime += Time.fixedDeltaTime;
    }

    void StartRewind()
    {
        if (!pointsInTime[0].localPosition.IsObjectMovingOnTheXAxis(0.01f) &&
            !pointsInTime[0].localPosition.IsObjectMovingOnTheYAxis(0.01f) &&
            !pointsInTime[0].localPosition.IsObjectMovingOnTheZAxis(0.01f))
        {
            StopRewind();
            return;
        }
        
        isRewinding = true;
        rb.isKinematic = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = startStateRB.isKinematic;
        rb.useGravity = startStateRB.useGravity;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        elapsedTime = 0.0f;
        GetComponent<BoxCollider>().enabled = true;
    }
}

public class FixedJointProperties
{
    public Vector3 anchor;
    public bool autoConfigureConnectedAnchor;
    public Vector3 axis;
    public float breakForce;
    public float breakTorque;
    public Vector3 connectedAnchor;
    public Rigidbody connectedBody;
    public float connectedMassScale;
    public bool enableCollision;
    public bool enablePreprocessing;

    public FixedJointProperties(FixedJoint joint)
    {
        anchor = joint.anchor;
        autoConfigureConnectedAnchor = joint.autoConfigureConnectedAnchor;
        axis = joint.axis;
        breakForce = joint.breakForce;
        breakTorque = joint.breakTorque;
        connectedAnchor = joint.connectedAnchor;
        connectedBody = joint.connectedBody;
        connectedMassScale = joint.connectedMassScale;
        enableCollision = joint.enableCollision;
        enablePreprocessing = joint.enablePreprocessing;
    }
}

public class RigidbodyProperties
{
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public bool isKinematic;
    public bool useGravity;
    public bool detectCollisions;
    public Vector3 position;
    public Quaternion rotation;
    public float mass;
    public float drag;
    public float angularDrag;

    public RigidbodyProperties(Rigidbody body)
    {
        velocity = body.velocity;
        angularVelocity = body.angularVelocity;
        isKinematic = body.isKinematic;
        useGravity = body.useGravity;
        position = body.position;
        rotation = body.rotation;
        mass = body.mass;
        drag = body.drag;
        angularDrag = body.angularDrag;
        detectCollisions = body.detectCollisions;
    }
}

public class PointInTime
{
    public Vector3 localPosition;
    public Quaternion localRotation;

    public PointInTime(Vector3 pos, Quaternion rot)
    {
        localPosition = pos;
        localRotation = rot;
    }
}
