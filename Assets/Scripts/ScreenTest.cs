using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest : MonoBehaviour {

	// Use this for initialization
    void Start ()
     {
        Camera camera = GetComponent<Camera>();
        float aspect = camera.aspect;
        //iPhone 8
        if(aspect > 0.55f  && aspect < .57f)
         {
            camera.orthographicSize = 4.1f;
         }
        //iPhone X
        else if(aspect > 0.45f && aspect < 0.47f)
        {
            camera.orthographicSize = 5f;
        }
     }
	
	// Update is called once per frame
	void Update () {
		
	}
}
