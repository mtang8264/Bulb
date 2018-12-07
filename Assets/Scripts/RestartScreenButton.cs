using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class RestartScreenButton : MonoBehaviour {
    [Header("Button Mode")]
    public Options buttonMode;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Clicked);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Clicked()
    {
        switch(buttonMode)
        {
            case Options.YES:
                if (File.Exists(Application.persistentDataPath + "/bulb.blb"))
                {
                    File.Delete(Application.persistentDataPath + "/bulb.blb");
                }
                if (File.Exists(Application.persistentDataPath + "/pet.blb"))
                {
                    File.Delete(Application.persistentDataPath + "/pet.blb");
                }
                SceneManager.LoadScene(1);
                break;
            case Options.NO:
                SceneManager.LoadScene(0);
                break;
        }
    }

    public enum Options { YES, NO };
}
