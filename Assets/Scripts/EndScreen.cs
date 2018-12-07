using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System;

public class EndScreen : MonoBehaviour {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream s;
    FileStream e;
    DateTime sTime;
    DateTime eTime;

	// Use this for initialization
	void Start () {
        if(File.Exists(Application.persistentDataPath + "/start.blb") == false || File.Exists(Application.persistentDataPath + "/end.blb") == false)
        {
            FileStream f = File.Create(Application.persistentDataPath + "/start.blb");
            bf.Serialize(f, DateTime.UtcNow);
            f = File.Create(Application.persistentDataPath + "/end.blb");
            bf.Serialize(f, DateTime.UtcNow);
            f.Close();
        }
        s = File.Open(Application.persistentDataPath + "/start.blb", FileMode.Open) ?? null;
        sTime = (DateTime)bf.Deserialize(s);
        s.Close();
        e = File.Open(Application.persistentDataPath + "/end.blb", FileMode.Open) ?? null;
        eTime = (DateTime)bf.Deserialize(e);
        e.Close();

        TimeSpan span = eTime - sTime;

        GetComponent<Text>().text += span.Days + " Days,\n" +
            span.Hours + " Hours,\n" +
            span.Minutes + " Minutes, and\n" +
            span.Seconds + " Seconds";
	}
}
