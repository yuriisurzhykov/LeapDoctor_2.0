using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Leap.Unity;
using System;

public class LoadData
{
    private string pathName;
    private const string allGesturesFileName = "AllGestures.json";
    private ListGestureFiles listGesture = new ListGestureFiles();

    private static string savedGesture = null;
    private static bool thisGestureContents = false;

    public LoadData(string pathName, Chirality chirality)
    {
        this.pathName = pathName + chirality.ToString() + ".json";
        if (pathName != savedGesture && pathName != chirality.ToString() + ".json")
        {
            savedGesture = pathName;
            thisGestureContents = false;
        }
        else
        {
            thisGestureContents = true;
        }
    }

    public LoadData()
    {
        string allGestDownload = PlayerPrefs.GetString(allGesturesFileName);
        listGesture = JsonUtility.FromJson<ListGestureFiles>(allGestDownload);
    }

    public void Save(List<Vector3> loadedTransform, List<Quaternion> loadedRotation,
                     List<Vector3> deltaPoses, List<Quaternion> deltaRots)
    {
        //Save file with new Gesture
        saveNewGesture(loadedTransform, loadedRotation, deltaPoses, deltaRots);

        //If file with this gesture present in file with all gestures, then save it
        AddNewGestureFileName();
    }

    private bool saveNewGesture(List<Vector3> loadedTransform, List<Quaternion> loadedRotation,
                                List<Vector3> deltaPoses, List<Quaternion> deltaRots)
    {
        SavedData saveData = new SavedData();
        saveData._handPosition = loadedTransform;
        saveData._handRotation = loadedRotation;
        saveData._deltaPos = deltaPoses;
        saveData._deltaRot = deltaRots;

        //Convert to Json
        string jsonData = JsonUtility.ToJson(saveData);
        //Save Json string gesture class
        PlayerPrefs.SetString(pathName, jsonData);
        PlayerPrefs.Save();
        if (jsonData != null || jsonData != "")
            return true;
        else
            return false;
    }

    private bool AddNewGestureFileName()
    {
        if (!thisGestureContents)
        {
            string allGestDownlod = PlayerPrefs.GetString(allGesturesFileName);
            if (allGestDownlod != "")
            {
                listGesture = JsonUtility.FromJson<ListGestureFiles>(allGestDownlod);
            }
            listGesture._allFileName.Add(savedGesture);
            string allGestUpload = JsonUtility.ToJson(listGesture);
            PlayerPrefs.SetString(allGesturesFileName, allGestUpload);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public ListGestureFiles getAllFileNamesOfGestures()
    {
        string allGestDownload = PlayerPrefs.GetString(allGesturesFileName);
        listGesture = JsonUtility.FromJson<ListGestureFiles>(allGestDownload);
        return listGesture;
    }

    public SavedData getTransformByIndex(int index, Chirality chirality)
    {
        try
        {
            SavedData savedDataC = Load(listGesture._allFileName[index] + chirality + ".json");
            return savedDataC;
        } catch(ArgumentOutOfRangeException)
        {
            return new SavedData();
        }
    }

    private SavedData Load(string neededFile)
    {
        //Load saved Json
        string jsonData = PlayerPrefs.GetString(neededFile);
        if (jsonData == "")
            return new SavedData();
        //Convert to Class
        SavedData loadedData = JsonUtility.FromJson<SavedData>(jsonData);
        return loadedData;
    }
    public void Delete()
    {
        PlayerPrefs.DeleteKey(pathName);
    }
}
