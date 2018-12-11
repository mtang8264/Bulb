using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SpritePicked : MonoBehaviour {
    GameObject s0, s1, s2, s3, s4;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

        if (File.Exists(Application.persistentDataPath + "/pet.blb") == false)
        {
            SceneManager.LoadScene(1);
        }

        s0 = Resources.Load<GameObject>("Bulbs/00_ONION");
        s1 = Resources.Load<GameObject>("Bulbs/01_BEANIE");
        s2 = Resources.Load<GameObject>("Bulbs/02_BILLA");
        s3 = Resources.Load<GameObject>("Bulbs/03_SKETCHY");
        s4 = Resources.Load<GameObject>("Bulbs/04_GEOMO");

        int sel;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/pet.blb", FileMode.Open);
        sel = (int)bf.Deserialize(file);
        file.Close();

        switch(sel)
        {
            case 0:
                animator.runtimeAnimatorController = s0.GetComponent<Animator>().runtimeAnimatorController;
                break;
            case 1:
                animator.runtimeAnimatorController = s1.GetComponent<Animator>().runtimeAnimatorController;
                break;
            case 2:
                animator.runtimeAnimatorController = s2.GetComponent<Animator>().runtimeAnimatorController;
                break;
            case 3:
                animator.runtimeAnimatorController = s3.GetComponent<Animator>().runtimeAnimatorController;
                break;
            case 4:
                animator.runtimeAnimatorController = s4.GetComponent<Animator>().runtimeAnimatorController;
                break;
        }

        Destroy(this);
    }
}
