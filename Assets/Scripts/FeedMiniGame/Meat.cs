using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Meat : MonoBehaviour {
    RectTransform rect;
    public static int spawned = 0;

	// Use this for initialization
	void Start () {
        spawned++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            Debug.Log("hit the ground");
        }
        else if(collision.gameObject.name == "Bulb")
        {
            Debug.Log("fed");
            FeedGameRunner.score++;
        }

        if(spawned >= 10)
        {
            FeedCarrier.feedValue = FeedGameRunner.score;
            SceneManager.LoadScene(0);
        }

        Destroy(gameObject);
    }
}
