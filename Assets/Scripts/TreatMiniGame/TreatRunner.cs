using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreatRunner : MonoBehaviour
    {
    public static int value = 0;

    GameObject[] blocks = new GameObject[3];
    RectTransform bulb;

    int[] order = { 0, 1, 2 };
    public AnimationCurve over, under, across1, across2;

    public float speed;

    public int[] switching;

    Vector3 first;
    Vector3 second;

    float start = 0f;

    int switchesPerformed = 0;

    bool started = false;
    bool finished = false;
    int selection = -1;

	// Use this for initialization
	void Start () {
        bulb = GameObject.Find("Bulb").GetComponent<RectTransform>();

        blocks[0] = GameObject.Find("block1");
        blocks[1] = GameObject.Find("block2");
        blocks[2] = GameObject.Find("block3");

        switching = Positions();
        first = blocks[switching[0]].GetComponent<RectTransform>().anchoredPosition3D;
        second = blocks[switching[1]].GetComponent<RectTransform>().anchoredPosition3D;
        across1.AddKey(0, first.x);
        across1.AddKey(1, second.x);
        across2.AddKey(0, second.x);
        across2.AddKey(1, first.x);
    }
	
	// Update is called once per frame
	void Update () {
        if(!started)
        {
            blocks[1].GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 10);
            bulb.anchoredPosition += new Vector2(0, 10);

            if(blocks[1].GetComponent<RectTransform>().anchoredPosition.y <= 0)
            {
                blocks[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                bulb.gameObject.SetActive(false);
                started = true;
            }
        }

        if (switchesPerformed < 10 && started)
        {
            blocks[switching[0]].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(across1.Evaluate((Time.time - start) * speed), 350 * over.Evaluate((Time.time - start) * speed));
            blocks[switching[1]].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(across2.Evaluate((Time.time - start) * speed), 350 * under.Evaluate((Time.time - start) * speed));

            if ((Time.time - start) * speed > 1f)
            {
                switching = Positions();
                first = blocks[switching[0]].GetComponent<RectTransform>().anchoredPosition3D;
                second = blocks[switching[1]].GetComponent<RectTransform>().anchoredPosition3D;
                across1.RemoveKey(0); across1.RemoveKey(0);
                across2.RemoveKey(0); across2.RemoveKey(0);
                across1.AddKey(0, first.x);
                across1.AddKey(1, second.x);
                across2.AddKey(0, second.x);
                across2.AddKey(1, first.x);

                start = Time.time;

                switchesPerformed++;
            }
        }

        if(switchesPerformed >= 10)
        {
            blocks[0].GetComponent<Button>().interactable = true;
            blocks[1].GetComponent<Button>().interactable = true;
            blocks[2].GetComponent<Button>().interactable = true;
            blocks[0].GetComponent<Button>().onClick.AddListener(One);
            blocks[1].GetComponent<Button>().onClick.AddListener(Two);
            blocks[2].GetComponent<Button>().onClick.AddListener(Three);
        }

        if(finished)
        {
            bulb.gameObject.SetActive(true);
            blocks[selection].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 10);
            bulb.anchoredPosition -= new Vector2(0, 10);

            if(blocks[selection].GetComponent<RectTransform>().anchoredPosition.y >= 350)
            {
                if(blocks[selection].transform.childCount > 0)
                {
                    value = 25;
                }
                SceneManager.LoadScene(0);
            }
        }
    }

    int[] Positions()
    {
        int[] ret = { -1, -1 };

        ret[0] = Random.Range(0, 2);
        switch (ret[0])
        {
            case 0:
                ret[1] = Random.value > 0.5f ? 1 : 2;
                break;
            case 1:
                ret[1] = Random.value > 0.5f ? 0 : 2;
                break;
            case 2:
                ret[1] = Random.value > 0.5f ? 0 : 1;
                break;
        }

        return ret;
    }

    void One()
    {
        if (!finished)
        {
            selection = 0;
            finished = true;
        }
    }
    void Two()
    {
        if (!finished)
        {
            selection = 1;
            finished = true;
        }
    }
    void Three()
    {
        if (!finished)
        {
            selection = 2;
            finished = true;
        }
    }
}
