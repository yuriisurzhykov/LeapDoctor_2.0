using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class HandMover : HandRecordingManager
{
    [SerializeField] private List<Transform> localTransform;

    /*Position and Rotation recordered Hand*/
    protected List<Vector3> getedPosition = new List<Vector3>();
    protected List<Quaternion> getedRotation= new List<Quaternion>();

    protected int curInxTransform = 0;
    protected SavedData dataTransform;
    private bool isPlaying = false;
    private bool isFirstCircule = false;

    // Start is called before the first frame update
    private void Awake()
    {
        curInxTransform = 0;
        localTransform.AddRange(gameObject.GetComponentsInChildren<Transform>());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Debug.Log(getedPosition.Count + " getedPosCount");
        if(isPlaying)
            MoveHand();
    }

    public virtual void MoveHand()
    {
        if (getedPosition.Count != 0 && getedRotation.Count != 0)
        {
            for (int i = 0; i < localTransform.Count; i++)
            {
                localTransform[i].position = new Vector3(getedPosition[curInxTransform].x, getedPosition[curInxTransform].y, getedPosition[curInxTransform].z - 0.05f);
                localTransform[i].rotation = getedRotation[curInxTransform];
                curInxTransform++;
            }
            if (curInxTransform > getedPosition.Count - 1 || curInxTransform > getedRotation.Count - 1)
            {
                isFirstCircule = false;
                curInxTransform = localTransform.Count;
            }
            else
            {
                isFirstCircule = true;
            }
        }
    }

    public List<Transform> getLocalTransforms()
    {
        return localTransform;
    }
    public Chirality getChirality()
    {
        return handedness;
    }

    public void StartPlay()
    {
        isFirstCircule = true;
        isPlaying = true;
    }
    public bool FirstCircule()
    {
        return isFirstCircule;
    }

    public void StopPlay()
    {
        isPlaying = false;
    }

    public bool SetNewTransforms(List<Vector3> pos, List<Quaternion> rot)
    {
        getedPosition = pos;
        getedRotation = rot;
        if (getedPosition.Count != 0 && getedRotation.Count != 0)
            return true;
        else
            return false;
    }
}

