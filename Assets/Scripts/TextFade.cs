using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {
    Text text;
    public float secondsTillFade = 1f;
    public float durationOfFade = 0.5f;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time > secondsTillFade)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, (Time.time) / (durationOfFade + secondsTillFade)));
        }
	}
}
