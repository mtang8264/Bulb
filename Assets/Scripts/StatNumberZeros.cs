using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatNumberZeros : MonoBehaviour {
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(text.text.Length ==2)
        {
            text.text += ".00";
        }
        else if(text.text.Length == 3 )
        {
            if (text.text == "100")
            {
                text.text += ".00";
            }
            else
            {
                text.text += "00";
            }
        }
        else if(text.text.Length == 4)
        {
            text.text += "0";
        }
	}
}
