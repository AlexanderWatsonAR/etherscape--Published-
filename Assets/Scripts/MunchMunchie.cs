using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunchMunchie : MonoBehaviour
{
    public GameObject munchSmash;

    public void MunchTheMunchie()
    {
        GameObject newMunchieSmash = Instantiate(munchSmash);
        newMunchieSmash.transform.parent = null;
        newMunchieSmash.transform.position = transform.position;
        newMunchieSmash.transform.rotation = transform.rotation;
        newMunchieSmash.SetActive(true);
        MunchieManager.MunchieCount++;
        gameObject.SetActive(false);
    }
}
