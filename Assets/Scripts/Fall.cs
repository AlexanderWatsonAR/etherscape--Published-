using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Fall : MonoBehaviour
{
    public GameObject DeathScreen;
    public Vector3 startPosition;
    public bool isDestructable = true;

    Rigidbody rigidBody;
    ConstantForce force;
    Stopwatch watch;
    Vector3 direction;
    bool hasPlayerReset;
    float speed;
    float frameRate;
    float deltaTime;

    GameObject startTile;

    // Start is called before the first frame update
    void Start()
    {
        if (name == "Player")
        {
            startTile = Instantiate(GameObject.Find("StartTile"));
            startTile.tag = "Untagged";
            startTile.SetActive(false);
            isDestructable = false;
        }

        watch = new Stopwatch();
        rigidBody = GetComponent<Rigidbody>();
        force = GetComponent<ConstantForce>();
        startPosition = transform.position;
        frameRate = Time.deltaTime * 1000;
        deltaTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidBody.velocity.y < -22.0f)
        {
            if (isDestructable)
            {
                Destroy(gameObject);
            }
            else
            {
                GameObject.Find("Settings Button").GetComponent<UnityEngine.UI.Button>().interactable = false;
                DeathScreen.SetActive(true);
                GetComponent<PlayerController>().enabled = false;
                rigidBody.velocity = new Vector3();
                rigidBody.angularVelocity = new Vector3();
                rigidBody.ResetInertiaTensor();
                rigidBody.useGravity = false;
                force.force = new Vector3();
                PauseGame.PleasePause();
                GetComponent<SphereCollider>().enabled = false;
            }
        }
        if(hasPlayerReset)
        {
            ScoreManagerCloudOnce.Distance = (int)transform.position.z;
            TravelToDestination();
            GetComponent<TrackObject>().Track(); // Take control of camera tracking.
        }
    }

    void TravelToDestination()
    {
        if (transform.position.z < 0.0f || transform.position.y > startPosition.y)
        {
            transform.position = startPosition;
        }

        if (Vector3.Distance(transform.position, startPosition) >= 0.1f)
        {
            transform.position -= direction * speed;
        }
        else
        {
            hasPlayerReset = false;
            GetComponent<PlayerController>().enabled = true;
            transform.position = startPosition;
            rigidBody.velocity = new Vector3();
            rigidBody.angularVelocity = new Vector3();
            rigidBody.ResetInertiaTensor();
            rigidBody.useGravity = true;
            force.force = new Vector3();
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<TrackObject>().hasPlayerReset = false;
            watch.Stop();
            UnityEngine.Debug.Log(watch.ElapsedMilliseconds);
        }
    }

    public void ResetGameObject()
    {
        watch.Restart();
        DeathScreen.SetActive(false);
        hasPlayerReset = true;
        float distance = Vector3.Distance(transform.position, startPosition);
        direction = Vector3.Normalize(transform.position - startPosition);
        PauseGame.PleaseResume();
        float duration = 1.5f;
        speed = (distance / duration) * deltaTime;
        GetComponent<TrackObject>().hasPlayerReset = true;
    }

    public void Restart()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach(GameObject tile in tiles)
        {
            Destroy(tile);
        }

        GameObject newStartTile = Instantiate(startTile);
        newStartTile.tag = "Tile";
        newStartTile.SetActive(true);

        startPosition = new Vector3(0.0f, 5.0f, 1.0f);
        transform.position = startPosition;
        transform.rotation = new Quaternion();
        Camera.main.transform.position = new Vector3(0.0f, 6.5f, 1.5f);

        GetComponent<TrackObject>().hasPlayerReset = false;
        hasPlayerReset = false;
        GetComponent<PlayerController>().enabled = true;
        rigidBody.velocity = new Vector3();
        rigidBody.angularVelocity = new Vector3();
        rigidBody.ResetInertiaTensor();
        rigidBody.useGravity = true;
        force.force = new Vector3();
        GetComponent<SphereCollider>().enabled = true;
        PauseGame.PleaseResume();
    }
}
