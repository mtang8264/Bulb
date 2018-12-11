using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedGameRunner : MonoBehaviour {
    public Difficulty difficulty;
    public GameObject meat;
    bool spawn = true;
    float last = float.Epsilon;
    public float wait = 2f;

    public static int score = 0;

    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        switch(FeedCarrier.difficulty)
        {
            case 0:
                difficulty = Difficulty.EASY;
                break;
            case 1:
                difficulty = Difficulty.NORMAL;
                break;
            case 2:
                difficulty = Difficulty.HARD;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(last + wait < Time.time)
        {
            last = Time.time;
            int pos = Random.Range((int)-1, (int)2);
            GameObject temp = Instantiate(meat, GameObject.Find("Canvas").transform);
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos * 270, 1200);
            switch(difficulty)
            {
                case Difficulty.EASY:
                    temp.GetComponent<Rigidbody2D>().gravityScale = 25;
                    break;
                case Difficulty.NORMAL:
                    temp.GetComponent<Rigidbody2D>().gravityScale = 50;
                    break;
                case Difficulty.HARD:
                    temp.GetComponent<Rigidbody2D>().gravityScale = 100;
                    break;
            }
        }

        scoreText.text = "" + score + "/10";
	}

    public enum Difficulty { EASY, NORMAL, HARD };
}
