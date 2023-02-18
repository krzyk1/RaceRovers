using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScriptManager : MonoBehaviour
{
    MenuScript gameMenu;
    Text winText;
    public GameObject planetObject;
    public GameObject gatePrefab;
    public GameObject gatePrefab2;
    float planetDistance;
    public int gateNumber = 12;
    //public delegate void globalScripts();
    //public static event globalScripts setGates = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        planetDistance = planetObject.transform.localScale.x / 2;
        gameMenu = FindObjectOfType<MenuScript>();
        winText = GameObject.Find("WinText").GetComponent<Text>();
        startRace();

        for (int i = 0; i < gateNumber; i++)
        {
            Vector3 coords = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
            var newGate = Instantiate(gatePrefab, Vector3.Normalize(coords) * planetDistance, Quaternion.identity, GameObject.Find("Gates").transform);
            if (SceneManager.GetActiveScene().name != "SingleplayerScene")
            {
                var secondNewGate = Instantiate(gatePrefab2, Vector3.Normalize(coords) * planetDistance, Quaternion.identity, GameObject.Find("OtherGates").transform);
            }

            //setGates();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            pauseMenu();
        }
    }

    void pauseMenu()
    {
        if (gameMenu.gameObject.activeSelf)
        {
            gameMenu.gameObject.SetActive(false);
        }
        else
        {
            gameMenu.gameObject.SetActive(true);
        }
    }

    public void startRace()
    {
        gameMenu.gameObject.SetActive(false);
        winText.text = "";
    }
}
