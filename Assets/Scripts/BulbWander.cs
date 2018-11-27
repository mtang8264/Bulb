using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbWander : MonoBehaviour {
    Animator animator;
    public bool walking = false;
    float lastWalk = -2.5f;
    float startTime = 0;
    int direction = -1;
    float startX;
    float distance;
    float goalTime = 0;
    float timeMulti = 1;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("walking", walking);

        if (!walking && lastWalk + 2.5 < Time.time)
        {
            if (Random.value > .5)
            {
                walking = true;
                startTime = Time.time;
                startX = transform.position.x;
                // [-1.8, 1.8] is the same as [0, 3.6]
                if (Random.Range(-1.8f, 1.8f) > transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }

                distance = Random.value + 0.25f;
                goalTime = Time.time + distance * timeMulti;

                if(startX + direction * distance > 1.8 || startX + direction * distance < -1.8)
                {
                    direction = direction * -1;
                }
            }
        }
        else if (walking)
        {
            float newX = Mathf.Lerp(startX, startX + (direction * distance), (Time.time - startTime) / (goalTime - startTime));
            transform.position = new Vector3(newX, transform.position.y);

            if(Time.time >= goalTime)
            {
                lastWalk = Time.time;
                walking = false;
            }
        }

        transform.localScale = new Vector3( -1 * direction,1);
	}
}
