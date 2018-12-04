using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public class Spawner : MonoBehaviour {
    GameObject s0, s1;
    int sel;

	// Use this for initialization
	void Start ()
    {
        if(File.Exists(Application.persistentDataPath + "/pet.blb") == false)
        {
            SceneManager.LoadScene(1);
        }

        s0 = Resources.Load<GameObject>("Bulbs/00_ONION");
        s1 = Resources.Load<GameObject>("Bulbs/01_BEANIE");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/pet.blb", FileMode.Open);
        sel = (int)bf.Deserialize(file);
        file.Close();

        switch(sel)
        {
            case 0:
                Instantiate(s0, GameObject.Find("BulbSpace").transform).name = "ONION";
                break;
            case 1:
                Instantiate(s1, GameObject.Find("BulbSpace").transform).name = "BEANIE";
                break;
        }

        Destroy(this);
	}
}
