using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorTrigger : MonoBehaviour
{
    public Color colour;
    public GameObject text;
    public AudioSource source;

    bool hasPlayerTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
        {
            Destroy(other.gameObject);
            return;
        }

        if (hasPlayerTriggered)
            return;
        hasPlayerTriggered = true;
        for (int i = 0; i < transform.childCount-6; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = colour;
            child.GetComponent<Renderer>().material.mainTexture = null;
        }

        if(source != null)
        {
            source.Play();
        }

        GameObject player = GameObject.Find("Player");
        Fall fall = player.GetComponent<Fall>();
        Vector3 boxColliderCenter = GetComponent<BoxCollider>().center;
        Vector3 newStartPosition = new Vector3(transform.position.x + boxColliderCenter.x, transform.position.y + boxColliderCenter.y, transform.position.z + boxColliderCenter.z);
        fall.startPosition = newStartPosition;
        text.GetComponent<TextMeshPro>().text = ScoreManagerCloudOnce.Distance.ToString() + "m";

        DestroyObjectsBehindDoor();
    }

    void DestroyObjectsBehindDoor()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            if (tile != null && tile.transform.position.z < transform.position.z && tile.GetComponent<SelfDestruct>() != null && tile != gameObject)
            {
                tile.GetComponent<SelfDestruct>().enabled = true;
            }
        }
    }
}
