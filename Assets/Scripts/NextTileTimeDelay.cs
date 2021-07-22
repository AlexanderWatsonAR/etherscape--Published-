using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NextTileTimeDelay : MonoBehaviour
{
    public float time = 0.75f;
    public GameObject nextTile;
    public GameObject[] extras;
    public GameObject[] randomTiles;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        if (nextTile != null)
            nextTile.SetActive(true);

        foreach (GameObject extra in extras)
        {
            extra.SetActive(true);
        }

        if (randomTiles.Length > 0)
        {
            int randomIndex = Random.Range(0, randomTiles.ToList().Count);
            randomTiles[randomIndex].SetActive(true);
        }
    }
}
