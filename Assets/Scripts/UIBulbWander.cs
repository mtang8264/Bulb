using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBulbWander : MonoBehaviour {
    Animator animator;
    bool walking = false;
    float lastWalk = -2.5f;
    float startTime = 0;
    int direction = -1;
    float startX;
    float distance;
    float goalTime = 0;
    public float walkSpeed = 100;
    public double walkProbability = 0.75;

    RectTransform tr;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        tr = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("walking", walking);

        if(!walking && lastWalk + 2.5f <= Time.time && Random.value > walkProbability)
        {
            walking = true;
            startTime = Time.time;
            startX = tr.anchoredPosition.x;
            // [-410, 410] is the same as [0, 820]
            if (Random.Range(-410, 410) > tr.anchoredPosition.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            distance = Random.value * 200 + 100;
            goalTime = Time.time + distance * (1/walkSpeed);

            if (startX + direction * distance > 410 || startX + direction * distance < -410)
            {
                direction = direction * -1;
            }
        }
        else if (walking)
        {
            float newX = Mathf.Lerp(startX, startX + (direction * distance), (Time.time - startTime) / (goalTime - startTime));
            tr.anchoredPosition = new Vector2(newX, tr.anchoredPosition.y);

            if (Time.time >= goalTime)
            {
                lastWalk = Time.time;
                walking = false;
            }
        }

        tr.localScale = new Vector3(-1 * direction, 1);
    }
}
