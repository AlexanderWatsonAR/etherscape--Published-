using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMunchie : MonoBehaviour
{
    public GameObject MunchieBox;
    public int upperMostRange = 41;

    public void MaybeMunchie()
    {
        if (MunchieBox == null)
            return;

        int munchieChance = 1;

        int upperRange = GameStopWatch.TimeElapsedInSeconds;

        upperRange = upperRange > upperMostRange ? upperMostRange : upperRange;

        if (MunchieManager.MunchieCount >= 3)
            upperRange = upperMostRange;

        bool isThereAMunchie = Random.Range(1,upperRange) == munchieChance ? true : false;

        if(isThereAMunchie)
        {
            MunchieBox.SetActive(true);
        }
        
    }
}
