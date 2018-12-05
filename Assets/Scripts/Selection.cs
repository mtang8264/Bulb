using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using System;

public class Selection : MonoBehaviour {
    int curr = 0;
    readonly int options = 5;

    Animator animator;
    Text displayName;
    Button p, n;

    GameObject s0, s1, s2, s3, s4;

	// Use this for initialization
	void Start () {
        animator = GameObject.Find("Ren").GetComponent<Animator>();
        displayName = GameObject.Find("Name").GetComponent<Text>();

        p = GameObject.Find("Prev").GetComponent<Button>();
        n = GameObject.Find("Next").GetComponent<Button>();

        p.onClick.AddListener(Prev);
        n.onClick.AddListener(Next);

        s0 = Resources.Load<GameObject>("Bulbs/00_Onion");
        s1 = Resources.Load<GameObject>("Bulbs/01_Beanie");
        s2 = Resources.Load<GameObject>("Bulbs/02_Billa");
        s3 = Resources.Load<GameObject>("Bulbs/03_Sketchy");
        s4 = Resources.Load<GameObject>("Bulbs/04_Geomo");
	}

    // Update is called once per frame
    void Update()
    {
        curr = curr > options - 1 ? 0 : curr;
        curr = curr < 0 ? options - 1 : curr;

        switch(curr)
        {
            case 0:
                animator.runtimeAnimatorController = s0.GetComponent<Animator>().runtimeAnimatorController;
                displayName.text = s0.name;
                break;
            case 1:
                animator.runtimeAnimatorController = s1.GetComponent<Animator>().runtimeAnimatorController;
                displayName.text = s1.name;
                break;
            case 2:
                animator.runtimeAnimatorController = s2.GetComponent<Animator>().runtimeAnimatorController;
                displayName.text = s2.name;
                break;
            case 3:
                animator.runtimeAnimatorController = s3.GetComponent<Animator>().runtimeAnimatorController;
                displayName.text = s3.name;
                break;
            case 4:
                animator.runtimeAnimatorController = s4.GetComponent<Animator>().runtimeAnimatorController;
                displayName.text = s4.name;
                break;
        }

        displayName.text = displayName.text.Substring(3);
    }

    public void Next()
    {
        curr++;
    }
    public void Prev()
    {
        curr--;
    }
    public void Select()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/pet.blb");
        bf.Serialize(file, curr);
        file.Close();
        file = File.Create(Application.persistentDataPath + "/start.blb");
        bf.Serialize(file, DateTime.UtcNow);
        file.Close();
        SceneManager.LoadScene(0);
    }
}
