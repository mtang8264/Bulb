using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour {
    [Header("Horizontal movement and time")]
    public float speed = 1f;
    public AnimationCurve hor;

    [Header("Is the player moving?")]
     bool moving = false;

    [Header("Vertical jump curve")]
    public AnimationCurve ver;
    public float height;

    float startX;
    float goalX;
    float startT;
    float goalT;

    RectTransform rect;

    BoxCollider2D collider;

    public float allowable = 0.8f;

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
        collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(moving)
        {
            float x = Mathf.Lerp(startX, goalX, hor.Evaluate((Time.time - startT) / (goalT - startT)));
            float y = ver.Evaluate((Time.time - startT) / (goalT - startT));

            rect.anchoredPosition = new Vector3(x, 100 + height*y);

            if(Time.time >= goalT)
            {
                moving = false;
            }
        }
        else
        {
            rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, 100);
        }

        collider.enabled = (Time.time - startT) / (goalT - startT) > allowable ? true : false;
	}

    public void Left()
    {
        Debug.Log("Left");
        startX= rect.anchoredPosition.x;
        goalX = -260;
        startT = Time.time;
        goalT = startT + speed;
        moving = true;
    }
    public void Right()
    {
        Debug.Log("Right");
        startX = rect.anchoredPosition.x;
        goalX = 260;
        startT = Time.time;
        goalT = startT + speed;
        moving = true;
    }
    public void Center()
    {
        Debug.Log("Center");
        startX = rect.anchoredPosition.x;
        goalX = 0;
        startT = Time.time;
        goalT = startT + speed;
        moving = true;
    }
}
