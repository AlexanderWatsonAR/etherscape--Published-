using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUIAnimation : MonoBehaviour
{
    RectTransform rectTransform;
    Vector3 speed = new Vector3(0.3f, 0.3f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        speed *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (MunchieManager.MunchieCount <= 0)
        {
            //GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            return;
        }

        rectTransform.localScale += speed;

        if (rectTransform.localScale.x <= 0.65f || rectTransform.localScale.x >= 0.85f)
        {
            speed = -speed;
        }

    }
}
