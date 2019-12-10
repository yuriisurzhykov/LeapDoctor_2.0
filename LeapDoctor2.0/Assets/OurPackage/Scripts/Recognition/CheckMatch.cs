using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CheckMatch : MonoBehaviour
{
    private CheckZone[] checkZones;
    private LoadData loadData;
    private SavedData savedData;
    private List<Vector3> poses = new List<Vector3>();
    private List<Quaternion> rots = new List<Quaternion>();
    [SerializeField] private HandMover handMover;
    private float curTime = 0f;

    public List<Vector3> recPoses = new List<Vector3>();
    public List<Quaternion> recRots = new List<Quaternion>();
    public List<Transform> localTrans = new List<Transform>();

    private List<Vector3> deltaPos = new List<Vector3>();
    private List<Quaternion> deltaRots = new List<Quaternion>();


    public bool thisHandTrue { get; set; }
    private bool canRecord = true;

    // Start is called before the first frame update
    void Start()
    {
        localTrans.AddRange(gameObject.GetComponentsInChildren<Transform>());
        handMover = gameObject.GetComponent<HandMover>();
        Debug.Log(PlayerPrefs.GetInt("ChoosedGesture"));
        savedData = new LoadData().getTransformByIndex(PlayerPrefs.GetInt("ChoosedGesture"), handMover.getChirality());
        poses = savedData._handPosition;
        rots = savedData._handRotation;
        Debug.Log(poses.Count + " poses.Count");
        checkZones = GetComponentsInChildren<CheckZone>();
        Debug.Log(checkZones.Length);
        handMover.SetNewTransforms(poses, rots);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            for (int i = 0; i < checkZones.Length; i++)
            {
                if (checkZones[i].getZoneCheck)
                {
                    canRecord = true;
                    thisHandTrue = true;
                    continue;
                }
                else
                {
                    thisHandTrue = false;
                    break;
                }
            }
        }
        catch (NullReferenceException)
        {
            handMover = GetComponent<HandMover>();
        }
    }
    public void RecMovement()
    {
        if (canRecord)
        {
            for (int i = 0; i < localTrans.Count; i++)
            {
                recPoses.Add(localTrans[i].position);
                recRots.Add(localTrans[i].rotation);
            }
        }
    }

    public void SetPlay()
    {
        handMover.StartPlay();
    }

    public void SetStop()
    {
        handMover.StopPlay();
    }

    public bool GestureDone()
    {
        return !handMover.FirstCircule();
    }

    public void CalculateDelta()
    {
        canRecord = false;
        for (int i = 0; i < recPoses.Count - localTrans.Count; i++)
        {
            deltaPos.Add(LeapMath.LeapMath.CalculateDeltaV(recPoses[i], recPoses[i + localTrans.Count]));
            deltaRots.Add(LeapMath.LeapMath.CalculateDeltaQ(recRots[i], recRots[i + localTrans.Count]));
        }
        Debug.Log(deltaRots.Count + "   " + savedData._deltaRot.Count);
        //CalculateAccuracy();
    }

    public void CalculateAccuracy()
    {
        if(deltaPos.Count != 0)
        {
            double accuracy = 1f;
            Vector3 sumDeltsPos = Vector3.zero;
            //Quaternion sumDeltsRot = Quaternion.identity;

            double sumDeltasPos = 0f;
            int amountPluses = 0;
            //float sumDeltasRot = 0f;
            for (int i = 0; i < savedData._deltaPos.Count; i++)
            {
                sumDeltsPos = LeapMath.LeapMath.CalculateDeltaV(deltaPos[i], savedData._deltaPos[i]);
                
                sumDeltsPos = savedData._deltaPos[i] - sumDeltsPos;
                sumDeltsPos = new Vector3((sumDeltsPos.x) / savedData._deltaPos[i].x,
                                          (sumDeltsPos.y) / savedData._deltaPos[i].y,
                                          (sumDeltsPos.z) / savedData._deltaPos[i].z);
                Debug.Log(sumDeltsPos.x + "   sd");
                sumDeltasPos += (sumDeltsPos.x + sumDeltsPos.y + sumDeltsPos.z) / 3;
                Debug.Log(sumDeltasPos + " sumDeltas");
                if(i % 200 == 0 && i != 0)
                {
                    accuracy += sumDeltasPos / 200;
                    amountPluses++;
                    sumDeltasPos = 0;
                }
            }
            
            accuracy /= amountPluses;
            Debug.Log("Жест выполнен с точностью " + accuracy + " " + sumDeltasPos);
        }
    }
}