using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    Button resetButton;
    Button menuButton;
    Button quitButton;

    void Start()
    {
        resetButton = transform.GetChild(0).gameObject.GetComponent<Button>();
        menuButton = transform.GetChild(1).gameObject.GetComponent<Button>();
        quitButton = transform.GetChild(2).gameObject.GetComponent<Button>();
        quitButton.onClick.AddListener(Application.Quit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void goToSingleplayer()
    {
        SceneManager.LoadScene("SingleplayerScene");
    }

    public void goToMultiplayer()
    {
        SceneManager.LoadScene("MultiplayerScene");
    }
}
