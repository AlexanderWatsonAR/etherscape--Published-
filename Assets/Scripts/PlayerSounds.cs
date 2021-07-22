using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSounds : MonoBehaviour
{
    public GameObject ImpactAudio;
    Rigidbody body;

    AudioSource audio;

    public AudioClip[] woodSoundClips;
    public AudioClip[] metalSoundClips;
    public AudioClip munchieSmash;
    public AudioClip glassPing;
    public AudioClip ice;
    public AudioClip rubber;

    void Start()
    {
        audio = GetComponent <AudioSource>();
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionPoint = collision.GetContact(0).point;

        Vector3 absRelativeVelocity = new Vector3(Mathf.Abs(collision.relativeVelocity.x), Mathf.Abs(collision.relativeVelocity.y), Mathf.Abs(collision.relativeVelocity.z));

        if(collision.gameObject.tag == "Munchie")
        {
            collision.gameObject.GetComponent<MunchMunchie>().MunchTheMunchie();
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.transform.position = collisionPoint;
            newImpact.GetComponent<AudioSource>().volume = 0.5f;
            newImpact.GetComponent<AudioSource>().clip = munchieSmash;
            newImpact.GetComponent<AudioSource>().Play();
        }

        if ((collision.gameObject.name == "Right Pillar" || collision.gameObject.name == "Left Pillar") && collision.gameObject.transform.parent.gameObject.name.Contains("Door"))
        {
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.GetComponent<AudioSource>().clip = glassPing;
            newImpact.GetComponent<AudioSource>().volume = 1.0f;
            newImpact.GetComponent<AudioSource>().Play();
            return;
        }
            //Under
        if (collisionPoint.y < transform.position.y && absRelativeVelocity.y > 0.1f)
        {
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.transform.position = collisionPoint;
            newImpact.GetComponent<AudioSource>().volume = 1 - (1 / absRelativeVelocity.y);
            newImpact.GetComponent<AudioSource>().pitch = 1 + (0.3f - (0.3f / (absRelativeVelocity.y)));
            if (collision.gameObject.tag == "Metalic")
            {
                newImpact.GetComponent<AudioSource>().clip = metalSoundClips.First(a => a.name == "Under metal tap");
            }
            else if (collision.gameObject.name == "Ice cube")
            {
                newImpact.GetComponent<AudioSource>().clip = ice;
            }
            else
            {
                newImpact.GetComponent<AudioSource>().clip = woodSoundClips.First(a => a.name == "Under tap");
            }
            newImpact.GetComponent<AudioSource>().Play();
        }
        //Left
        if (collisionPoint.x < transform.position.x && absRelativeVelocity.x > 0.1f)
        {
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.transform.position = collisionPoint;
            newImpact.GetComponent<AudioSource>().volume = 1 - (1 / (absRelativeVelocity.x));
            newImpact.GetComponent<AudioSource>().pitch = 1 + (0.3f - (0.3f / (absRelativeVelocity.x)));
            if (collision.gameObject.tag == "Metalic")
            {
                newImpact.GetComponent<AudioSource>().clip = metalSoundClips.First(a => a.name == "Left metal tap");
            }
            else
            {
                newImpact.GetComponent<AudioSource>().clip = woodSoundClips.First(a => a.name == "Left tap");
            }
            newImpact.GetComponent<AudioSource>().Play();
        }
        //Right
        if (collisionPoint.x > transform.position.x && absRelativeVelocity.x > 0.1f)
        {
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.transform.position = collisionPoint;
            newImpact.GetComponent<AudioSource>().volume = 1 - (1 / absRelativeVelocity.x);
            newImpact.GetComponent<AudioSource>().pitch = 1 + (0.3f - (0.3f / (absRelativeVelocity.x)));
            if (collision.gameObject.tag == "Metalic")
            {
                newImpact.GetComponent<AudioSource>().clip = metalSoundClips.First(a => a.name == "Right metal tap");
            }
            else
            {
                newImpact.GetComponent<AudioSource>().clip = woodSoundClips.First(a => a.name == "Right tap");
            }
            newImpact.GetComponent<AudioSource>().Play();
        }

        //Front
        if (collisionPoint.z > transform.position.z && absRelativeVelocity.z > 0.1f && collision.collider.transform.position.z > transform.position.z && collisionPoint.y > transform.position.y - 0.5f)
        {
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.transform.position = collisionPoint;
            newImpact.GetComponent<AudioSource>().volume = 1 - (1 / absRelativeVelocity.z);
            newImpact.GetComponent<AudioSource>().pitch = 1 + (0.3f - (0.3f / (absRelativeVelocity.z)));
            if (collision.gameObject.tag == "Metalic")
            {
                newImpact.GetComponent<AudioSource>().clip = metalSoundClips.First(a => a.name == "Front metal tap");
            }
            else if(collision.gameObject.name == "Ice cube")
            {
                newImpact.GetComponent<AudioSource>().clip = ice;
            }
            else if (collision.gameObject.name == "Car tyre")
            {
                newImpact.GetComponent<AudioSource>().clip = rubber;
            }
            else
            {
                newImpact.GetComponent<AudioSource>().clip = woodSoundClips.First(a => a.name == "Front tap");
            }
            newImpact.GetComponent<AudioSource>().Play();
        }

        //Back
        if (collisionPoint.z < transform.position.z && absRelativeVelocity.z > 0.1f && collision.gameObject.tag == "Metalic")
        {
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.transform.position = collisionPoint;
            newImpact.GetComponent<AudioSource>().volume = 1 - (1 / (absRelativeVelocity.z));
            newImpact.GetComponent<AudioSource>().pitch = 1 + (0.3f - (0.3f / (absRelativeVelocity.z)));
            newImpact.GetComponent<AudioSource>().clip = metalSoundClips.First(a => a.name == "Back metal tap");
            newImpact.GetComponent<AudioSource>().Play();
        }

        //Back Wall
        if (collisionPoint.z < transform.position.z && body.velocity.z < -0.1f && collision.gameObject.tag != "Metalic")
        {
            GameObject newImpact = Instantiate(ImpactAudio);
            newImpact.transform.position = collisionPoint;
            newImpact.GetComponent<AudioSource>().volume = 1 - (1 / (absRelativeVelocity.z));
            newImpact.GetComponent<AudioSource>().pitch = 1 + (0.3f - (0.3f / (absRelativeVelocity.z)));
            newImpact.GetComponent<AudioSource>().clip = woodSoundClips.First(a => a.name == "Back tap");
            newImpact.GetComponent<AudioSource>().Play();
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (PauseGame.isGamePaused)
        {
            audio.volume = 0;
            return;
        }

        audio.volume = 0.3f - (0.3f / (body.angularVelocity.magnitude));
        audio.pitch = 1 + (0.3f - (0.3f / (body.angularVelocity.magnitude)));

        if(audio.volume < 0.1f && body.velocity.IsObjectMovingOnTheXAxis(0.001f) && body.velocity.IsObjectMovingOnTheZAxis(0.001f))
        {
            audio.volume = 0.1f;
        }

        if(audio.pitch > 1.2f)
        {
            audio.pitch = 1.2f;
        }

        if (audio.pitch < 0.8f)
        {
            audio.pitch = 0.8f;
        }

        if(collision.gameObject.name == "Ice cube")
        {
            audio.pitch = 2.0f;
        }

        PlayerController.Multiplier = 3.0f;
    }

    private void OnCollisionExit(Collision collision)
    {
        audio.volume = 0;
        PlayerController.Multiplier = 1.5f;
    }

}
