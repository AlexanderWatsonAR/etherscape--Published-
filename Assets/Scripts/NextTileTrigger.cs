using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NextTileTrigger : MonoBehaviour
{
    public Vector3 ChildLocalPosition;
    GameObject nextChild;
    public GameObject[] PrefabList;
    public GameObject Parent;

    bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        GameObject nextTile = PrefabList[Random.Range(0, PrefabList.ToList().Count)];

        nextChild = Instantiate(nextTile);
        nextChild.tag = "Tile";
        nextChild.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isTriggered)
        {
            TriggerEvent();
            nextChild.transform.position = new Vector3(transform.position.x + ChildLocalPosition.x, transform.position.y + ChildLocalPosition.y, transform.position.z + ChildLocalPosition.z);
            nextChild.SetActive(true);
            //GetComponent<BoxCollider>().enabled = false;
            enabled = false;
            isTriggered = true;
        }
    }

    protected virtual void TriggerEvent()
    {

    }
}
