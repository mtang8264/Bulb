using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeleteSaveFile : MonoBehaviour {
    Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>() ?? null;
        button.onClick.AddListener(delegate { DeleteSaveData(true); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DeleteSaveData(bool t)
    {
        if (t)
        {
            if (File.Exists(Application.persistentDataPath + "/bulb.blb"))
            {
                File.Delete(Application.persistentDataPath + "/bulb.blb");
            }
            if (File.Exists(Application.persistentDataPath + "/pet.blb"))
            {
                File.Delete(Application.persistentDataPath + "/pet.blb");
            }
        }

        SceneManager.LoadScene(1);
    }
}
