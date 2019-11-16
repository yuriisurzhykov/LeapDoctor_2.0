using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using System;

public class CreateCollider : MonoBehaviour
{

    [SerializeField] Chirality handedness;
    [SerializeField] private float realFingerRadius;
    [SerializeField] private float virtualFingerRadius;
    [SerializeField] private float palmRaduis;

    List<Transform> localPos = new List<Transform>();
    List<Transform> virtualPos = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        GetVertualPos();
        GetVirtualPos();
        CreateRealColliders();
        CreateVirtualColliders();
    }
    private void GetVertualPos()
    {
        HandMover[] haM = FindObjectsOfType<HandMover>();
        for (int i = 0; i < haM.Length; i++)
        {
            if (handedness == haM[i].getChirality())
            {
                virtualPos = haM[i].getLocalTransforms();
            }
        }
    }

    private void GetVirtualPos()
    {
        if (handedness == Chirality.Left)
            localPos = GameObject.FindGameObjectWithTag("RealHandLeft").GetComponent<RiggedHand>().JointList;
        else
            localPos = GameObject.FindGameObjectWithTag("RealHandRight").GetComponent<RiggedHand>().JointList;
    }

    private void CreateRealColliders()
    {
        foreach (var pos in localPos)
        {
            var col = pos.gameObject.AddComponent<SphereCollider>();
            pos.gameObject.AddComponent<Rigidbody>().isKinematic = true;
            col.isTrigger = false;
            col.radius = realFingerRadius;
        }
    }

    private void CreateVirtualColliders()
    {
        foreach (var pos in virtualPos)
        {
            pos.gameObject.AddComponent<CheckZone>();
            var col = pos.gameObject.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = virtualFingerRadius;
        }
    }
}