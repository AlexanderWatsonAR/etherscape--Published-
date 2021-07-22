using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFanAnimationTrigger : MonoBehaviour
{
    public ParticleSystem Particles;
    public GameObject lid;
    public GameObject windBox;

    public AudioSource audioSource;
    public AudioClip motorRun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(PlayFanMotor());
            Particles.Play();
            BoxCollider bc = lid.GetComponent<BoxCollider>();
            Vector3 boxRight = new Vector3(bc.bounds.max.x - 0.1f, lid.transform.position.y - 0.01f, bc.bounds.center.z + 0.1f);
            lid.AddComponent<Rigidbody>();
            if (lid.GetComponent<Rigidbody>() != null)
            {
                lid.GetComponent<Rigidbody>().mass = 0.3f;
                lid.GetComponent<Rigidbody>().useGravity = true;
                lid.GetComponent<Rigidbody>().isKinematic = false;
                lid.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0.0f, 10.0f, 0.0f), boxRight, ForceMode.Impulse);
                lid.AddComponent<Fall>();
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
            windBox.SetActive(true);
        }
        
    }

    private IEnumerator PlayFanMotor()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = motorRun;
        audioSource.loop = true;
        audioSource.Play();
    }

}
