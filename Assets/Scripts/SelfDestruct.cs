using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float elapedMilliseconds;

    Color grey = new Color(0.7843f, 0.7843f, 0.7843f);

    IEnumerator Start()
    {
        if (GetComponent<MeshFilter>() != null)
        {
            GetComponent<Renderer>().material.color = grey;
        }

        Transform child = transform;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<MeshFilter>())
            {
                transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = grey;
            }
        }

        yield return new WaitForSeconds(elapedMilliseconds / 1000);

        Destroy(gameObject);
    }
}
