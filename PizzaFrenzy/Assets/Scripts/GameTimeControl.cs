using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controls game timer
public class GameTimeControl : MonoBehaviour
{
    int currTime;  // current time in seconds
    float realTime;  // current time as float in seconds
    public string time;  // score string

    // Start is called before the first frame update
    void Start()
    {
        currTime = 0;
        realTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate current game time
        realTime += Time.deltaTime;
        currTime = System.Convert.ToInt32(System.Math.Floor(realTime));
        int mins = currTime / 60;
        int secs = currTime - (mins * 60);
        time = mins.ToString("00") + ":" + secs.ToString("00");
        GetComponent<Text>().text = time;
        GetComponent<Text>().enabled = true;
    }

    private void OnDisable()
    {
        // set score on disable
        PlayerPrefs.SetString("score", time); 
    }
}