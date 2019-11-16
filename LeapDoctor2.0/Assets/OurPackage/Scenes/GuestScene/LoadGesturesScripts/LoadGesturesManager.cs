using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LoadGesturesManager : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private HandMover handMoverLeft;
    [SerializeField] private HandMover handMoverRight;


    private LoadData loadData = null;
    private ListGestureFiles listGesture = null;
    private SavedData savedData;

    private void Awake()
    {
        loadData = new LoadData();
        listGesture = loadData.getAllFileNamesOfGestures();
        if(listGesture != null)
            foreach (string s in listGesture._allFileName)
                Debug.Log(s);
        dropdown.ClearOptions();
        try
        {
            dropdown.AddOptions(listGesture._allFileName);
        }
        catch (NullReferenceException)
        {
            listGesture = new ListGestureFiles();
            listGesture._allFileName.Add("No items!");
            dropdown.AddOptions(listGesture._allFileName);
        }
    }

    public void LoadChoosedGesture()
    {
        Debug.Log(dropdown.value);
        if(listGesture._allFileName[0] != "No items!")
        {
            handMoverLeft.StopPlay();
            handMoverRight.StopPlay();
            savedData = loadData.getTransformByIndex(dropdown.value, handMoverLeft.getChirality());
            handMoverLeft.SetNewTransforms(savedData._handPosition, savedData._handRotation);
            savedData = loadData.getTransformByIndex(dropdown.value, handMoverRight.getChirality());
            handMoverRight.SetNewTransforms(savedData._handPosition, savedData._handRotation);
            handMoverLeft.StartPlay();
            handMoverRight.StartPlay();
            PlayerPrefs.SetInt("ChoosedGesture", dropdown.value);
            SceneManager.LoadScene("ComplitingMovement");
        }
    }
}
