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
    public List<Vector3> _deltaPos = new List<Vector3>();
    public List<Quaternion> _deltaRot = new List<Quaternion>();

    public SavedData()
    {
        _handPosition.Add(Vector3.zero);
        _handRotation.Add(Quaternion.identity);
        _deltaPos.Add(Vector3.zero);
        _deltaRot.Add(Quaternion.identity);
    }

    public override string ToString()
    {
        return "";
    }
}