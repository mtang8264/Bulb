using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour {
    int curr = 0;
    readonly int options = 2;

    Animator animator;
    Text displayName;

    GameObject s0, s1;

	// Use this for initialization
	void Start () {
        animator = GameObject.Find("Ren").GetComponent<Animator>();
        displayName = GameObject.Find("Name").GetComponent<Text>();

        s0 = Resources.Load<GameObject>("Bulbs/Onion");
        s1 = Resources.Load<GameObject>("Bulbs/Beanie");
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
        }
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
        SceneManager.LoadScene(0);
    }
}
