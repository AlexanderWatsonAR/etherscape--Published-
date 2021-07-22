using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;
using UnityEngine.UI;
using TMPro;

public class ScoreManagerCloudOnce : MonoBehaviour
{
    private static Animator animator;
    private static TextMeshProUGUI text;

    private static int distance;
    private static int previousDistance;

    public static int Distance
    {
        get {return distance;}
        set 
        {
            previousDistance = distance;
            distance = value;
            UpdateDistance();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject mainCamera = GameObject.Find("Main Camera");
        text = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
        previousDistance = 0;
        Distance = 0;

        Cloud.OnInitializeComplete += CloudIntializeComplete;
        Cloud.Initialize(false, true);
    }

    public void CloudIntializeComplete()
    {
        Cloud.OnInitializeComplete -= CloudIntializeComplete;
        Debug.Log("Initialized");
    }

    private static void UpdateDistance()
    {
        text.text = Distance.ToString() + "m";
        
        if (Distance % 10 == 0 && Distance > previousDistance)
        {
            animator.Play("PositiveTextAnimation");
        }

        if (Distance % 10 == 0 && Distance < previousDistance)
        {
            animator.Play("NegativeTextAnimation");
        }

        PlayerPrefs.SetInt("ScoreToUpdate", distance);
    }

    public void UpdateLeaderboard()
    {
        if(PlayerPrefs.GetInt("ScoreToUpdate", 0) == 0)
        {
            return;
        }

        Leaderboards.DistanceTravelled.SubmitScore(PlayerPrefs.GetInt("ScoreToUpdate", 0));
        PlayerPrefs.SetInt("ScoreToUpdate", 0);
    }
}
