using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class Bulb : MonoBehaviour {
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

    // Use this for initialization
    void Start () {
        // Attempts to load the stats when started
        LoadStats();

        // Finds and assigns the sliders for the stats
        hungerSlider = GameObject.Find("HungerBar").GetComponent<Slider>();
        happinessSlider = GameObject.Find("HappinessBar").GetComponent<Slider>();
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        // Sets the max values just in case
        hungerSlider.maxValue = happinessSlider.maxValue = healthSlider.maxValue = 100;
	}
	
	// Update is called once per frame
	void Update () {
        StatUpdate();
	}

    // Called every time the game is paused or unpaused
    void OnApplicationPause(bool pause)
    {
        if(pause)
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
        //Defines difference since the hunger epoch
        TimeSpan timeSpan = DateTime.UtcNow - hungerEpoch;
        //Total seconds since that epoch
        double s = timeSpan.TotalSeconds;
        //Calculation for hunger
        hunger = -1 * (s / 792) + 100;

        //Same for happiness
        timeSpan = DateTime.UtcNow - happinessEpoch;
        s = timeSpan.TotalSeconds;
        happiness = -1 * (100 * Mathf.Pow((float)s, 2) / Mathf.Pow(72000, 2)) + 100;

        //Same for health
        timeSpan = DateTime.UtcNow - healthEpoch;
        s = timeSpan.TotalSeconds;
        health = -1 * (100 / Mathf.Sqrt(86400)) * Mathf.Sqrt((float)s) + 100;

        //Adds any offsets from not fully restored stats
        hunger += huOff;
        happiness += haOff;
        health += heOff;

        //Sets the GUI to show the stats
        hungerSlider.value = (float)hunger;
        happinessSlider.value = (float)happiness;
        healthSlider.value = (float)health;
    }

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