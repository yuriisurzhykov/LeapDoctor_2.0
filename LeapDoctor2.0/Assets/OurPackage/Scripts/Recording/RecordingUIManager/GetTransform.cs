using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class GetTransform : HandRecordingManager
{
    [SerializeField] float timeRecognition = 0.1f;
    /*Transform of real hand*/
    [SerializeField] private List<Transform> recordingHand = new List<Transform>();

    private List<Vector3> recPos = new List<Vector3>();
    private List<Quaternion> recRot = new List<Quaternion>();
    private List<Vector3> deltaPos = new List<Vector3>();
    private List<Quaternion> deltaRot = new List<Quaternion>();

    private bool wasRecording = false;
    private bool isRecording = false;

    private float tempTime = 0f;

    void Awake()
    {
        InitComponents(handedness);
    }

    //public GetTransform(Chirality handedness)
    //{
    //    InitComponents(handedness);
    //}

    public void InitComponents(Chirality chirality)
    {
        recordingHand.AddRange(GetComponentsInChildren<Transform>());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StopRecording();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGesture(handedness.ToString());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteRecordedGest();
        }
        RecordMovement();
    }

    public void SaveGesture(string fileName)
    {
        if (fileName != "" && !isRecording)
            wasRecording = true;
        if (wasRecording)
        {
            Debug.Log("Gesture saves right!");
            loadData = new LoadData(fileName, handedness);
            loadData.Save(recPos, recRot, deltaPos, deltaRot);
            wasRecording = false;
        }
    }

    public void DeleteRecordedGest()
    {
        recPos.Clear();
        recRot.Clear();
    }

    public void StartRecording()
    {
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
        for (int i = 0; i < recPos.Count - recordingHand.Count; i++)
        {
            deltaPos.Add(LeapMath.LeapMath.CalculateDeltaV(recPos[i], recPos[i + recordingHand.Count]));
            deltaRot.Add(LeapMath.LeapMath.CalculateDeltaQ(recRot[i], recRot[i + recordingHand.Count]));
        }
    }

    private void RecordMovement()
    {
        if (isRecording)
        {
            for (int i = 0; i < recordingHand.Count; i++)
            {
                recPos.Add(recordingHand[i].position);
                recRot.Add(recordingHand[i].rotation);
            }
        }
    }

    public List<Transform> JointList()
    {
        return recordingHand;
    }
    public List<Vector3> GetRecPos()
    {
        return recPos;
    }
    public List<Quaternion> GetRecRot()
    {
        return recRot;
    }
}
