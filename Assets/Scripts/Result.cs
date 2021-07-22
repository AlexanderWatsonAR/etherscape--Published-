using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    private void OnGUI()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Result: " + ScoreManagerCloudOnce.Distance.ToString() + "m";
        GameObject playButton = transform.parent.GetChild(2).gameObject;
        GameObject munchieImage = playButton.transform.GetChild(0).gameObject;

        if (MunchieManager.MunchieCount <= 0)
        {
            playButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
            munchieImage.GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else
        {
            playButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
            munchieImage.GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
