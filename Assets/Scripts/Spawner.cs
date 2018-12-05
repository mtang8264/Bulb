using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public class Spawner : MonoBehaviour {
    GameObject s0, s1, s2, s3, s4;
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
        s2 = Resources.Load<GameObject>("Bulbs/02_BILLA");
        s3 = Resources.Load<GameObject>("Bulbs/03_SKETCHY");
        s4 = Resources.Load<GameObject>("Bulbs/04_GEOMO");

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
            case 2:
                Instantiate(s2, GameObject.Find("BulbSpace").transform).name = "BILLA";
                break;
            case 3:
                Instantiate(s3, GameObject.Find("BulbSpace").transform).name = "SKETCHY";
                break;
            case 4:
                Instantiate(s4, GameObject.Find("BulbSpace").transform).name = "GEOMO";
                break;
        }

        Destroy(this);
	}
}
