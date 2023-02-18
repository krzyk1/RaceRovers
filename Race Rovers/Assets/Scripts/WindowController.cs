using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public Rect helpRect = new (Screen.height - (Screen.height / 2), Screen.width - (Screen.width / 2), 300, 150);
    // Start is called before the first frame update
    public bool displayHelp = false;
    GUIStyle helpStyle = new GUIStyle();
    
    private void OnGUI()
    {
        if(displayHelp)
        {
            helpRect = GUI.Window(0, helpRect, HelpWindow, "Help");
                
            //GUI.Label(new Rect(800, 500, 300, 300), "To win the race you must race", helpStyle);
        }
    }

    void Start()
    {
        helpStyle.normal.textColor = Color.white;
        helpStyle.fontSize = 17;
        helpRect = new (Screen.width / 2 - 350, Screen.height / 2 + 120, 700, 200);
        displayHelp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HelpWindow(int windowID)
    {
        //if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
        //{
        //    print("Got a click in window with color " + GUI.color);
        //}

        // Make the windows be draggable.
        GUI.Label(new Rect(10, 20, 100, 500), "You are about to play a racing game with randomly generated tracks.\nThe tracks consist of control points, which appear in three states.\nThe point lit with <b>yellow</b> light is the one you need to reach at the moment.\nOnce you pass through it, it becomes <b>green</b>, so you know you've passed through it.\nThe <b>red</b> ones will become yellow once you pass through the point that precedes it.\nThe <b>arrow</b> displayed above you shows you the direction to the next yellow point.\nA lap ends once you pass through all of the control points.\nOne race consists of three laps. Have fun!", helpStyle);
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    public void toggleHelp()
    {
        displayHelp = !displayHelp;
    }
}
