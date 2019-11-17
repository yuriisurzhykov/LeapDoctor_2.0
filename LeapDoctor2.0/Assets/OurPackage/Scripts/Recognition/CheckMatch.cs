using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckMatch : MonoBehaviour
{
    private CheckZone[] checkZones;
    private LoadData loadData;
    private SavedData savedData;
    private List<Vector3> poses = new List<Vector3>();
    private List<Quaternion> rots = new List<Quaternion>();
    [SerializeField] private HandMover handMover;

    public static int thisHandCheck;
    // Start is called before the first frame update
    void Start()
    {
        handMover = gameObject.GetComponent<HandMover>();
        Debug.Log(PlayerPrefs.GetInt("ChoosedGesture"));
        savedData = new LoadData().getTransformByIndex(PlayerPrefs.GetInt("ChoosedGesture"), handMover.getChirality());
        poses = savedData._handPosition;
        rots = savedData._handRotation;
        Debug.Log(poses.Count + " poses.Count");
        checkZones = GetComponentsInChildren<CheckZone>();
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
                    handMover.StartPlay();
                    continue;
                }
                else
                {
                    handMover.StopPlay();
                    break;
                }
            }
        }
        catch (NullReferenceException)
        {
            handMover = GetComponent<HandMover>();
        }
    }
}