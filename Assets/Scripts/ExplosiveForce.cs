using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveForce : MonoBehaviour
{
    public float explosiveForce = 3;
    public float radius = 3;
    // Start is called before the first frame update
    void Start()
    {
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, radius);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider collider in objectsInRadius)
        {
            if (collider.attachedRigidbody != null && !rigidbodies.Contains(collider.attachedRigidbody))
            {
                rigidbodies.Add(collider.attachedRigidbody);
            }
        }
        foreach (Rigidbody body in rigidbodies)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, body.position - transform.position, out hitInfo, radius))
            {
                if(!hitInfo.collider.name.Contains("Wall") && hitInfo.collider.tag != "Tile")
                {
                    body.isKinematic = false;
                    body.AddExplosionForce(explosiveForce, transform.position, radius, 1, ForceMode.Impulse);
                } 
            }
            //Debug.DrawRay(transform.position, (body.position - transform.position) * radius, Color.green, 600.0f);
        }
    }
}
