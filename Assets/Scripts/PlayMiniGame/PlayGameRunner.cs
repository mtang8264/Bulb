using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGameRunner : MonoBehaviour {
    public static int[] points = { 0, 0 };
    float width = 1;
    RectTransform transform;

    Button left;
    Button right;

    bool faceLeft = true;
    float timeCount = float.Epsilon;
    public float changeTime = 0.125f;

    float pickTime = -1f;
    public float pickWait = 2f;

    int solution;
    Text scoreboard;

	// Use this for initialization
	void Start () {
        points[0] = 0;
        points[1] = 0;

        transform = GetComponent<RectTransform>();

        scoreboard = GameObject.Find("Points").GetComponent<Text>();

        left = GameObject.Find("Left").GetComponent<Button>();
        right = GameObject.Find("Right").GetComponent<Button>();
        left.onClick.AddListener(Left);
        right.onClick.AddListener(Right);
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time > timeCount + changeTime)
        {
            faceLeft = !faceLeft;
            timeCount = Time.time;
        }

        if(solution == 0)
        {
            transform.localScale = faceLeft ? new Vector3(width, 1) : new Vector3(-1 * width, 1);
        }
        else
        {
            if(solution == 1)
            {
                transform.localScale = new Vector3(-1 * width, 1);
            }
            else if(solution == -1)
            {
                transform.localScale = new Vector3(width, 1);
            }
        }

        if(pickTime +pickWait < Time.time)
        {
            pickTime = -1f;
            solution = 0;
            if (points[0] + points[1] >= 10)
                SceneManager.LoadScene(0);
        }

        scoreboard.text = points[0] + " vs " + points[1];

        if (points[0] + points[1] >= 11)
            SceneManager.LoadScene(0);
    }

    void Left()
    {
        Choose();
        if (solution == -1)
            points[0]++;
        else
            points[1]++;
    }
    void Right()
    {
        Choose();
        if (solution == 1)
            points[0]++;
        else
            points[1]++;
    }

    void Choose()
    {
        pickTime = Time.time;
        solution = Random.value > 0.5 ? 1 : -1;
    }
}
