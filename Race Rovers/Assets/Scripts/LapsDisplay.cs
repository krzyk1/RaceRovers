using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LapsDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    Text lapText;
    Text winText;
    //int currentLaps;
    public int maxLaps;
    public GateContainer allGates;
    public MenuScript gameOverLayout;
    public delegate void wonRace();
    public static event wonRace endOfRace = delegate { };
    void Start()
    {
        winText = GameObject.Find("WinText").GetComponent<Text>();
        //gameOverLayout = FindObjectOfType<MenuScript>();
        lapText = GetComponent<Text>();
        
        raceStart();
        //allGates = FindObjectOfType<GateContainer>();
        GateContainer.lapEnd += updateLaps;
    }

    private void OnDisable()
    {
        GateContainer.lapEnd -= updateLaps;
    }

    // Update is called once per frame
    void Update()
    {
        if (allGates.countedPlayer.finishedLaps == maxLaps)
        {
            gameOverLayout.gameObject.SetActive(true);
            if (SceneManager.GetActiveScene().name != "SingleplayerScene")
            {
                winText.text = allGates.countedPlayer.name + " wins!";
            }
            winText.gameObject.SetActive(true);
            //gameOverLayout.transform.GetChild(3).GetComponent<Text>().text = allGates.countedPlayer.name + " wins!";
            endOfRace();
        }
    }

    void updateLaps()
    {
        //allGates.countedPlayer.finishedLaps++;
        lapText.text = "Laps: " + allGates.countedPlayer.finishedLaps + " / " + maxLaps;
        //Debug.Log("funkcja");
    }

    public void raceStart()
    {
        allGates.countedPlayer.finishedLaps = 0;
        lapText.text = "Laps: " + allGates.countedPlayer.finishedLaps + " / " + maxLaps;
    }


}
