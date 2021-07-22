using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableAnimationControl
{
    public GameObject[] children;
    public Mesh newAltStartState;
    public Mesh newEndState;

    public void RestartAdjustTexCoords()
    {
        foreach (GameObject child in children)
        {
            child.GetComponent<AdjustTexCoords>().endState = newEndState;
            child.GetComponent<AdjustTexCoords>().altStartState = newAltStartState;
            child.GetComponent<AdjustTexCoords>().Restart();
        }

    }
}

public class MultipleAnimationControl : MonoBehaviour
{
    public SerializableAnimationControl[] animationControls;

    void Play(int index)
    {
        animationControls[index].RestartAdjustTexCoords();
    }
}
