using UnityEngine;

public class AnimationControl : MonoBehaviour
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
