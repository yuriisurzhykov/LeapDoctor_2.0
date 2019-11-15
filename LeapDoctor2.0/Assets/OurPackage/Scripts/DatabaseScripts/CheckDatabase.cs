using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.DeleteKey("DatabasePresent");
        if (PlayerPrefs.GetString("DatabasePresent") != "True")
        {
            CreateDatabase createDatabase = new CreateDatabase();
            PlayerPrefs.SetString("DatabasePresent", "True");
            PlayerPrefs.Save();
        }
    }
}
