using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

public class Bulb : MonoBehaviour {
    public string az = "0 = Linear, 1 = DropOff, 2 = LevelOut";
    public int hungerBehavior, happinessBehavior, healthBehavior;

    // The stats after they have been processed
    public double hunger, happiness, health;
    // Any offsets which may occur;
    public double huOff, haOff, heOff;
    // These are the last times these stats were filled
    DateTime hungerEpoch;
    DateTime happinessEpoch;
    DateTime healthEpoch;
    // The UI elements which show the stats %
    Slider hungerSlider, happinessSlider, healthSlider;
    Text status;

    Button feed, play, treat;

    string[] goodStatuses = {
        "\nSTUFFED",
        "\nECSTATIC",
        "\nFULL OF ENERGY"
    };
    string[] mediumStatuses = {
        "\nFULL",
        "\nCONTENT",
        "\nHEALTHY"
    };
    string[] badStatuses = {
        "\nLETHARGIC",
        "\nJITTERY",
        "\nALIVE"
    };

    // Use this for initialization
    void Start() {
        if (File.Exists(Application.persistentDataPath + "/pet.blb") == false)
        {
            SceneManager.LoadScene(1);
        }

        // Attempts to load the stats when started
        LoadStats();

        // Finds and assigns the sliders for the stats
        hungerSlider = GameObject.Find("HungerBar").GetComponent<Slider>();
        happinessSlider = GameObject.Find("HappinessBar").GetComponent<Slider>();
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        // Sets the max values just in case
        hungerSlider.maxValue = happinessSlider.maxValue = healthSlider.maxValue = 100;

        status = GameObject.Find("Status").GetComponent<Text>();

        feed = GameObject.Find("Feed").GetComponent<Button>();
        feed.onClick.AddListener(Feed);
        play = GameObject.Find("Play").GetComponent<Button>();
        play.onClick.AddListener(Play);
        treat = GameObject.Find("Treat").GetComponent<Button>();
        treat.onClick.AddListener(Treat);
    }

    // Update is called once per frame
    void LateUpdate() {
        StatUpdate();

        if(hunger + huOff <= 0 || happiness + haOff <= 0 || health + heOff <= 0)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/end.blb");
            bf.Serialize(file, DateTime.UtcNow);
            file.Close();

            SceneManager.LoadScene(2);
        }

        double[] st = { hunger + huOff, happiness + haOff, health + heOff };
        double avg = (st[0] + st[1] + st[2]) / 3;
        int idxMax = 0;
        for (int i = 0; i < 3; i ++)
        {
            if (st[i] > st[idxMax])
                idxMax = i;
        }
        status.text = gameObject.name + " IS FEELING";
        if(avg > 66)
        {
            status.text += goodStatuses[idxMax];
        }
        else if(avg >33)
        {
            status.text += mediumStatuses[idxMax];
        }
        else
        {
            status.text += badStatuses[idxMax];
        }


        Debug.Log(DateTime.UtcNow - healthEpoch);
    }

    // Called every time the game is paused or unpaused
    void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveStats();
    }
    // Called right before the program quits
    void OnApplicationQuit()
    {
        SaveStats();
    }

    // Calculates the stats with some math
    void StatUpdate()
    {
        //Defines difference since the health epoch
        TimeSpan timeSpan = DateTime.UtcNow - healthEpoch;
        //Total seconds since that epoch
        double s = timeSpan.TotalSeconds;
        switch (healthBehavior)
        {
            case 0:
                health = Linear(s);
                break;
            case 1:
                health = DropOff(s);
                break;
            case 2:
                health = LevelOut(s);
                break;
        }

        timeSpan = DateTime.UtcNow - happinessEpoch;
        s = timeSpan.TotalSeconds;
        switch(happinessBehavior)
        {
            case 0:
                happiness = Linear(s);
                break;
            case 1:
                happiness = DropOff(s);
                break;
            case 2:
                happiness = LevelOut(s);
                break;
        }

        timeSpan = DateTime.UtcNow - hungerEpoch;
        s = timeSpan.TotalSeconds;
        switch(hungerBehavior)
        {
            case 0:
                hunger = Linear(s);
                break;
            case 1:
                hunger = DropOff(s);
                break;
            case 2:
                hunger = LevelOut(s);
                break;
        }

        //Sets the GUI to show the stats
        hungerSlider.value = (float)(hunger + huOff);
        happinessSlider.value = (float)(happiness + haOff);
        healthSlider.value = (float)(health + heOff);
    }

    float RoundStat(double s)
    {
        float o = (float)s;
        o *= 100;
        o = Mathf.RoundToInt(o);
        o /= 100;
        return o;
    }

    public void Feed()
    {
        Debug.Log("fed" + Time.time);
        huOff += 10;
        if (hunger + huOff >= 100)
        {
            hunger = 100;
            huOff = 0;
            hungerEpoch = DateTime.UtcNow;
        }
    }
    public void Play()
    {
        Debug.Log("played" + Time.time);
        haOff += 10;
        if (happiness + haOff >= 100)
        {
            happiness = 100;
            haOff = 0;
            happinessEpoch = DateTime.UtcNow;
        }
    }
    public void Treat()
    {
        Debug.Log("treated" + Time.time);
        heOff += 10;
        if (health + heOff >= 100)
        {
            health = 100;
            heOff = 0;
            healthEpoch = DateTime.UtcNow;
        }
    }

    double Linear(double s)
    {
        return -1 * (s / 720) + 100;
    }
    double LevelOut(double s)
    {
        return -1 * (100 / Mathf.Sqrt(86400)) * Mathf.Sqrt((float)s) + 100;
    }
    double DropOff(double s)
    {
        return -1 * (100 * Mathf.Pow((float)s, 2) / Mathf.Pow(79200, 2)) + 100;
    }

    // Save load stuff
    void SaveStats()
    {
        // The file path at which the game saves
        String path = Application.persistentDataPath + "/bulb.blb";
        // Generates a serializable object to save
        Stats s = new Stats(hunger, happiness, health, huOff, haOff, heOff, hungerEpoch, happinessEpoch, healthEpoch);
        // The formatter which will save the data
        BinaryFormatter bf = new BinaryFormatter();
        // Either creates or overwrites the save file
        FileStream file = File.Create(path);
        // Saves the object to the files
        bf.Serialize(file, s);
        // Closes the file connection
        file.Close();
        // Logs the resulted file
        Debug.Log("Saved to " + path);
    }
    void LoadStats()
    {
        // The local file where the save should be
        String path = Application.persistentDataPath + "/bulb.blb";
        // Empty stat object to be assigned to
        Stats s;
        // Checks if there is a save file
        if(File.Exists(path))
        {
            // Formatter to unpack the save data
            BinaryFormatter bf = new BinaryFormatter();
            // Opens the file
            FileStream file = File.Open(path,FileMode.Open);
            // Deserializes the save into an object
            s = (Stats)bf.Deserialize(file);
            // Logs the result
            Debug.Log("Loaded from " + path);
            // Closes the file connection
            file.Close();
        }
        else
        {
            // If there is no save data it just creates a new stat object
            s = new Stats();
            // And logs the information
            Debug.Log("No file found at " + path);
        }
        // Sets all variables from s
        hunger = s.h1;
        happiness = s.h2;
        health = s.h3;
        huOff = s.h1o;
        haOff = s.h2o;
        heOff = s.h3o;
        hungerEpoch = s.ep1;
        happinessEpoch = s.ep2;
        healthEpoch = s.ep3;
    }
}

[System.Serializable]
public class Stats
{
    public double h1, h2, h3, h1o, h2o, h3o;
    public DateTime ep1, ep2, ep3;

    public Stats(double h1, double h2, double h3, double h1o, double h2o, double h3o, DateTime ep1, DateTime ep2, DateTime ep3)
    {
        this.h1 = h1;
        this.h2 = h2;
        this.h3 = h3;
        this.h1o = h1o;
        this.h2o = h2o;
        this.h3o = h3o;
        this.ep1 = ep1;
        this.ep2 = ep2;
        this.ep3 = ep3;
    }
    public Stats()
    {
        h1 = h2 = h3 = 100;
        h1o = h2o = h3o = 0;
        ep1 = ep2 = ep3 = DateTime.UtcNow;
    }
}