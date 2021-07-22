using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject startTile;
    public GameObject scoreText;
    public GameObject munchieText;
    public GameObject joystick;
    public GameObject settingsButton;
    public GameObject rateWindow;
    public GameObject title;
    public Canvas canvas;
    public UnityEngine.UI.CanvasScaler TheCanvasScaler;
    public Camera mainCamera;

    public GameObject checkmarkLeft;
    public GameObject checkmarkRight;

    public GameObject beginButton;
    public GameObject leaderboardButton;
    public GameObject orientationButton;
    
    public GameObject player;

    float aspectRatio;

    float returnTime = 0.25f;

    private static bool isPortrait = true;

    [HideInInspector]
    public static bool IsPortrait
    {
        get
        {
            return isPortrait;
        }
    }

    int rateCount = 5;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
            returnTime = 0.0f;

        if (PlayerPrefs.GetInt("RateToUpdate") == rateCount)
        {
            rateCount += 2;
            rateWindow.SetActive(true);
            beginButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
            leaderboardButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
            //shopButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("RateToUpdate", PlayerPrefs.GetInt("RateToUpdate") + 1);
        }
        Camera.main.transform.eulerAngles = new Vector3(-40.0f, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        Camera.main.gameObject.GetComponent<Animator>().enabled = true;
        UpdateFOV.hasFovChanged = false;

        float height = Screen.currentResolution.height;
        float width = Screen.currentResolution.width;

        aspectRatio = height / width;

        if (PlayerPrefs.GetInt("IsPortrait") == 0)
        {
            StartCoroutine(Portrait());
        }
        else
        {
            StartCoroutine(Landscape());
        }

        Debug.unityLogger.logEnabled = false;

#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#endif
    }

    public void StartAnimation()
    {
        Screen.orientation = Screen.orientation;
        scoreText.SetActive(true);
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        scoreText.GetComponent<Animator>().enabled = true;
    }

    public void SecondAnimation()
    {
        joystick.GetComponent<Joystick>().enabled = true;
        joystick.GetComponent<Animator>().enabled = true;
        if (!isPortrait)
        {
            settingsButton.GetComponent<Animator>().enabled = true;
        }
        player.GetComponent<TrackObject>().enabled = true;
        player.GetComponent<Fall>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<ConstantForce>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;
        startTile.SetActive(true);

    }

    public void ChangeOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            StartCoroutine(Landscape());
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            StartCoroutine(Portrait());
    }

    private IEnumerator Portrait()
    {
        if(Input.deviceOrientation == DeviceOrientation.Portrait)
            Screen.orientation = ScreenOrientation.Portrait;
        else if(Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            Screen.orientation = ScreenOrientation.PortraitUpsideDown;
        else
            Screen.orientation = ScreenOrientation.Portrait;

        yield return new WaitForSeconds(returnTime);

        PlayerPrefs.SetInt("IsPortrait", 0);
        isPortrait = true;

        mainCamera.fieldOfView = 90.0f;

        UpdateFOV.targetFOV = 90.0f;
        UpdateFOV.targetDistance = -2.5f;
        UpdateFOV.targetHeight = 1.5f;
        UpdateFOV.targetRotation = -40.0f;
        UpdateFOV.targetCentrePoint = 0.0f;

        if (aspectRatio > 1.32f && aspectRatio < 1.34f)
        {
            TheCanvasScaler.matchWidthOrHeight = 1.0f;
        }
        else
        {
            TheCanvasScaler.matchWidthOrHeight = 0.0f;
        }

        canvas.GetComponent<Animator>().Play("NewPortraitOrientationAnimation");

        title.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, -250.0f, 0.0f);
        title.GetComponent<RectTransform>().localScale = new Vector3(2.22f, 2.22f, 2.22f);

        scoreText.GetComponent<RectTransform>().localScale = Vector3.one * 2;
        scoreText.GetComponent<RectTransform>().anchoredPosition = new Vector2(250.0f, -100.0f);

        munchieText.GetComponent<RectTransform>().localScale = Vector3.one * 2;
        munchieText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-425.0f, -100.0f);

        joystick.GetComponent<RectTransform>().localScale = new Vector3(2.0f, 2.0f, 2.0f);
        joystick.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.0f);
        joystick.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.0f);
        joystick.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 350.0f);

        rateWindow.GetComponent<RectTransform>().localScale = new Vector3(2.0f, 2.0f, 2.0f);

        // UI Stuff 

        beginButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -250.0f);
        beginButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500.0f, 150.0f);
        beginButton.GetComponent<RectTransform>().localScale = new Vector3(2.22f, 2.22f, 2.22f);

        leaderboardButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -500.0f);
        leaderboardButton.GetComponent<RectTransform>().sizeDelta = new Vector2(850.0f, 150.0f);
        leaderboardButton.GetComponent<RectTransform>().localScale = new Vector3(2.22f, 2.22f, 2.22f);

        //shopButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -750.0f);
        //shopButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500.0f, 150.0f);
        //shopButton.GetComponent<RectTransform>().localScale = new Vector3(2.22f, 2.22f, 2.22f);

        //orientationButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-560.0f, -1512.0f);
        orientationButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(200.0f, 125.0f);
        orientationButton.GetComponent<TMPro.TextMeshProUGUI>().text = "Portrait";
        orientationButton.GetComponent<RectTransform>().localScale = Vector3.one * 2;

        // End of UI Stuff

        isPortrait = true;
    }

    private IEnumerator Landscape()
    {
        
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            Screen.orientation = ScreenOrientation.LandscapeRight;
        else
            Screen.orientation = ScreenOrientation.LandscapeLeft;

        yield return new WaitForSeconds(returnTime);

        PlayerPrefs.SetInt("IsPortrait", 1);
        isPortrait = false;

        canvas.GetComponent<Animator>().Play("NewLandscapeOrientationAnimation");

        scoreText.GetComponent<RectTransform>().localScale = Vector3.one;
        scoreText.GetComponent<RectTransform>().anchoredPosition = new Vector2(125.0f, -50.0f);

        munchieText.GetComponent<RectTransform>().localScale = Vector3.one;
        munchieText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-225.0f, -50.0f);

        if (aspectRatio > 1.32f && aspectRatio < 1.34f)
        {
            mainCamera.fieldOfView = 80.0f;
            title.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, -250.0f, 0.0f);
            title.GetComponent<RectTransform>().localScale = new Vector3(1.33f, 1.33f, 1.33f);

        }
        else
        {
            mainCamera.fieldOfView = 60.0f;
            title.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, -125.0f, 0.0f);
            title.GetComponent<RectTransform>().localScale = new Vector3(1.11f, 1.11f, 1.11f);
        }

        UpdateFOV.hasFovChanged = false;

        TheCanvasScaler.matchWidthOrHeight = 0.0f;

        string joyStickPos = PlayerPrefs.GetString("JoystickToUpdate");

        if (joyStickPos == "Left")
            LeftJoystick();
        else if (joyStickPos == "Right")
            RightJoystick();
        else
            LeftJoystick();

        rateWindow.GetComponent<RectTransform>().localScale = Vector3.one;

        // UI Stuff 

        beginButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -74.0f);
        beginButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200.0f, 50.0f);
        beginButton.GetComponent<RectTransform>().localScale = Vector3.one;

        leaderboardButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -174.0f);
        leaderboardButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200.0f, 50.0f);
        leaderboardButton.GetComponent<RectTransform>().localScale = Vector3.one;

        //shopButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -274.0f);
        //shopButton.GetComponent<RectTransform>().sizeDelta = new Vector2(125.0f, 50.0f);
        //shopButton.GetComponent<RectTransform>().localScale = Vector3.one;

        orientationButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(100.0f, 50.0f);;
        orientationButton.GetComponent<TMPro.TextMeshProUGUI>().text = "Landscape";
        orientationButton.GetComponent<RectTransform>().localScale = Vector3.one;

        // End of UI Stuff

        isPortrait = false;
    }

    public void LeftJoystick()
    {
        PlayerPrefs.SetString("JoystickToUpdate", "Left");

        joystick.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
        joystick.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
        joystick.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 0.0f);
        joystick.GetComponent<RectTransform>().anchoredPosition = new Vector2(175.0f, 175.0f);
        checkmarkLeft.GetComponent<UnityEngine.UI.Image>().enabled = true;
        checkmarkRight.GetComponent<UnityEngine.UI.Image>().enabled = false;
    }

    public void RightJoystick()
    {
        PlayerPrefs.SetString("JoystickToUpdate", "Right");

        joystick.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
        joystick.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.0f);
        joystick.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.0f);
        joystick.GetComponent<RectTransform>().anchoredPosition = new Vector2(-175.0f, 175.0f);
        checkmarkLeft.GetComponent<UnityEngine.UI.Image>().enabled = false;
        checkmarkRight.GetComponent<UnityEngine.UI.Image>().enabled = true;
    }

    public void OpenUrl()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            UnityEngine.iOS.Device.RequestStoreReview();
        if (Application.platform == RuntimePlatform.Android)
            Application.OpenURL("market://details?id=" + Application.identifier);
    }

    public void ResetRate()
    {
        PlayerPrefs.SetInt("RateToUpdate", 0);
    }
}
