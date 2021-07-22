using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
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

    public void OpenLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void UpdateLeaderboard()
    {
        if(PlayerPrefs.GetInt("ScoreToUpdate", 0) == 0)
        {
            return;
        }

        Social.ReportScore(PlayerPrefs.GetInt("ScoreToUpdate", 1), etherscapeIds.leaderboard_distance_travelled, (bool success) =>
        {
            if(success)
            {
                PlayerPrefs.SetInt("ScoreToUpdate", 0);
            }
        });
    }
}
