using System.Collections.Generic;
using UnityEngine;
using System;
using Leap.Unity;
using System.Data.Sql;
using System.Data.SqlTypes;

[Serializable]
public class SavedData
{
    public int id;
    public string name;
    public List<Vector3> _handPosition = new List<Vector3>();
    public List<Quaternion> _handRotation = new List<Quaternion>();

    public SavedData()
    {
        _handPosition.Add(new Vector3(0, 0, 0));
        _handRotation.Add(new Quaternion(0, 0, 0, 0));
    }

    public override string ToString()
    {
        return "";
    }
}