using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationTrigger : MonoBehaviour
{
    public GameObject AnimationParent = null;
    public string ClipName;

    bool stop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !stop)
        {
            AnimationParent.GetComponent<Animator>().enabled = true;
            AnimationParent.GetComponent<Animator>().Play(ClipName);
            TriggerEvent();
            enabled = false;
            stop = true;
        }
    }

    protected virtual void TriggerEvent()
    {

    }
}
