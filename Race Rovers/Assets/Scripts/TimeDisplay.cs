using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeDisplay : MonoBehaviour
{
    Text timeText;
    public Text bestTimeText;
    float time;
    float timeInSeconds;
    float timeInMinutes;
    float timeInMilisecs;
    float bestTime;
    float bestTimeInSeconds;
    float bestTimeInMinutes;
    float bestTimeInMilisecs;
    bool timeFlow;
    // Start is called before the first frame update
    void Start()
    {
        bestTime = float.MaxValue;
        timeText = GetComponent<Text>();
        timeReset();
        timeFlow = true;
        LapsDisplay.endOfRace += newBestTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeFlow)
        {
                time += Time.deltaTime;
                //decimal decTime = new decimal(Time.deltaTime);
                timeInMinutes = Mathf.FloorToInt(time / 60);
                timeInSeconds = Mathf.FloorToInt(time % 60);
                timeInMilisecs = Mathf.FloorToInt((time % 1) * 1000);
                timeText.text = "Time: " + timeInMinutes + "m " + timeInSeconds + "s " + timeInMilisecs + "ms"; 
        }
    }

    public void timeReset()
    {
        time = 0;
        timeInSeconds = 0;
        timeInMinutes = 0;
        timeInMilisecs = 0;
    }

    public void timeStop()
    {
        timeFlow = false;
    }

    public void timeStart()
    {
        timeFlow = true;
    }

    void newBestTime()
    {
        Debug.Log("czas: " + time + " najlepszy czas: " + bestTime);
        if (time < bestTime)
        {
            bestTimeInSeconds = timeInSeconds;
            bestTimeInMinutes = timeInMinutes;
            bestTimeInMilisecs = timeInMilisecs;
            bestTimeText.text = "Personal Best: " + bestTimeInMinutes + "m " + bestTimeInSeconds + "s " + bestTimeInMilisecs + "ms";
            bestTime = time;
        }
        timeStop();
    }
}
