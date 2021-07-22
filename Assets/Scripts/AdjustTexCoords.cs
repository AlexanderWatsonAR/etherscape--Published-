using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdjustTexCoords : MonoBehaviour
{
    public Animator mator;

    public Mesh altStartState;
    public Mesh endState;
    public Mesh originalStartMeshState;
    public Mesh originalEndMeshState;

    public int joints = 1;
    
    public bool atStartState;
    public bool loop;
    public bool seamlessLoop;
    public bool restart;

    Vector2[] uvNextStep;
    Mesh newMeshState;
    bool atEndState = false;
    float numberOfFrames;
    float count;
    float countIncriment;

    // Start is called before the first frame update
    void Start()
    {
        // Animator works on duration of clips rather than the number of frames.
        // This converts the duration of the clip to a set number of frames.
        //numberOfFrames = ((mator.GetCurrentAnimatorStateInfo(0).length / joints) * 1000) / (Time.deltaTime * 1000);
        float delta = Time.deltaTime;
        delta = delta == 0.0f ? 0.01666666666f : delta;
        numberOfFrames = (mator.GetCurrentAnimatorStateInfo(0).length / joints) / delta;
        //numberOfFrames = ((mator.GetCurrentAnimatorStateInfo(0).length / joints) * 1000) * Time.deltaTime;
        countIncriment = 1.0f / numberOfFrames;

        loop = seamlessLoop == true ? false : loop;
        seamlessLoop = loop == true ? false : seamlessLoop;

        newMeshState = GetComponent<MeshFilter>().mesh;

        if (altStartState != null)
        {
            newMeshState.uv = altStartState.uv;
        }

        count = 0.0f;

        uvNextStep = new Vector2[endState.uv.Length];

        for (int i = 0; i < endState.uv.Length; i++)
        {
            uvNextStep[i].x = (endState.uv[i].x - newMeshState.uv[i].x) / numberOfFrames;
            uvNextStep[i].y = (endState.uv[i].y - newMeshState.uv[i].y) / numberOfFrames;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            Restart();
            return;
        }

        if (!atStartState)
        {
            return;
        }

        if (atEndState)
        {
            return;
        }

        if(PauseGame.isGamePaused)
        {
            return;
        }

        Vector2[] newUVs = newMeshState.uv;

        // Texture Coordinates
        for (int i = 0; i < newUVs.Length; i++)
        {
            // X
            if (newUVs[i].x != endState.uv[i].x)
            {
                if (endState.uv[i].x < newUVs[i].x)
                {
                    if (endState.uv[i].x < 0.0f && newUVs[i].x < 0.0f)
                    {
                        newUVs[i].x += uvNextStep[i].x;
                    }
                    else
                    {
                        if (uvNextStep[i].x < 0.0f)
                        {
                            newUVs[i].x += uvNextStep[i].x;
                        }
                        else
                        {
                            newUVs[i].x -= uvNextStep[i].x;
                        }
                    }
                        
                }
                else
                {
                    newUVs[i].x += uvNextStep[i].x;
                }
            }

            // Y
            if (newUVs[i].y != endState.uv[i].y)
            {
                if (endState.uv[i].y < newUVs[i].y)
                {
                    if (endState.uv[i].y < 0.0f && newUVs[i].y < 0.0f)
                    {
                        newUVs[i].y += uvNextStep[i].y;
                    }
                    else
                    {
                        if (uvNextStep[i].y < 0.0f)
                        {
                            newUVs[i].y += uvNextStep[i].y;
                        }
                        else
                        {
                            newUVs[i].y -= uvNextStep[i].y;
                        }
                    }
                }
                else
                {
                    newUVs[i].y += uvNextStep[i].y;
                }
            }
        }

        count += countIncriment;
        newMeshState.uv = newUVs;

        if (count >= 1.0f)
        {
             atEndState = true;

            if(!loop && !seamlessLoop)
            {
                newMeshState.SetUVs(0, endState.uv.ToList());
            }
            
            if (loop)
            {
                atEndState = false;
                newMeshState.SetUVs(0, originalStartMeshState.uv.ToList());
                count = 0.0f;
            }
            if(seamlessLoop)
            {
                atEndState = false;
                if(endState.EqualMesh(originalEndMeshState))
                {
                    endState = originalStartMeshState.CopyMesh();
                }
                else
                {
                    endState = originalEndMeshState.CopyMesh();
                }
                count = 0.0f;
            }
        }
    }

    public void Restart()
    {
        atEndState = false;
        Start();
    }
}
