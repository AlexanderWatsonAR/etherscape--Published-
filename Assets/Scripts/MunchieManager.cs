using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MunchieManager : MonoBehaviour
{
    private static TextMeshProUGUI text;
    private static Animator animator;
    private static int munchieCount;
    private static int previousMunchieCount;

    public GameObject deathWindow;

    public static int MunchieCount
    {
        get {return munchieCount; }
        set
        {
            previousMunchieCount = munchieCount;
            munchieCount = value;
            UpdateDisplay();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        munchieCount = PlayerPrefs.GetInt("MunchieToUpdate");
        //MunchieCount = MunchieCount == null ? 0 : MunchieCount;

        text = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
        UpdateDisplay();
    }

    private static void UpdateDisplay()
    {
        text.text = MunchieCount.ToString();
        if (munchieCount > previousMunchieCount)
        {
            animator.Play("PositiveTextAnimation");
        }
        else
        {
            animator.Play("NegativeTextAnimation");
        }
        PlayerPrefs.SetInt("MunchieToUpdate", munchieCount);
    }

    public void SpendMunchie()
    {
        if (MunchieCount > 0)
        {
            MunchieCount--;
            GameObject.Find("Player").GetComponent<Fall>().ResetGameObject();
        }
    }

    public static void BuyMunchie(int quantity)
    {
        MunchieCount += quantity;
    }

    public void BuyOneMunchie()
    {
        MunchieCount++;
    }
}
