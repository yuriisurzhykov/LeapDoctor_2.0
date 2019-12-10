//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Leap.Unity;

//public class UIRecordingManager : HandRecordingManager
//{
//    private List<Transform> recordingHand = new List<Transform>();
//    private Chirality handednes;
//    private List<Vector3> recPos = new List<Vector3>();
//    private List<Quaternion> recRot = new List<Quaternion>();
//    private bool wasRecording = false;
//    private bool isRecording = false;

//    HandMover _Hand;

//    private float tempTime = 0f;

//    public UIRecordingManager(Chirality chirality)
//    {
//        handednes = chirality;
//        if (handednes == Chirality.Left)
//        {
//            rigHand = GameObject.FindGameObjectWithTag("RealHandLeft").GetComponent<RiggedHand>();
//        }
//        else
//        {
//            rigHand = GameObject.FindGameObjectWithTag("RealHandRight").GetComponent<RiggedHand>();
//        }
//        recordingHand = rigHand.JointList;
//    }
//    private void Update()
//    {
//        if (isRecording)
//        {
//            Recording();
//        }
//    }

//    public void StopRecording()
//    {
//        isRecording = false;
//    }

//    public void DeleteCurrentGesture()
//    {
//        loadData.Delete();
//    }

//    public void StartRecording()
//    {
//        isRecording = true;
//    }

//    private void Recording()
//    {
//        for (int i = 0; i < recordingHand.Count; i++)
//        {
//            recPos.Add(recordingHand[i].position);
//            recRot.Add(recordingHand[i].rotation);
//        }
//    }

//    public IEnumerator PlayGesture(List<Transform> playedTransform)
//    {
//        yield return new WaitUntil(() =>
//        {
//            for (int i = 0; i < recPos.Count; i++)
//            {
//                for (int j = 0; j < playedTransform.Count; j++)
//                {
//                    playedTransform[j].position = recPos[i];
//                    playedTransform[j].rotation = recRot[i];
//                    i++;
//                }
//            }
//            return true;
//        });
//        Debug.Log(playedTransform.Count);
//        Debug.Log(recPos.Count);
//    }

//    public void SaveGesture(string fileName)
//    {
//        StopRecording();
//        if (recordingHand.Count != 0)
//            wasRecording = true;
//        if (wasRecording)
//        {
//            if(handednes == Chirality.Left)
//                loadData = new LoadData(fileName, handednes);
//            else
//                loadData = new LoadData(fileName, handednes);
//            loadData.Save(recPos, recRot);
//            wasRecording = false;
//        }
//    }

//    public List<Transform> GetTransforms()
//    {
//        return recordingHand;
//    }
//}
