using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public Gate firstGate;
    public Gate lastGate;
    public bool lapComplete;
    public delegate void finishedLap();
    public static event finishedLap lapEnd = delegate { };
    public Player countedPlayer;
    bool gatesSet;
    public Gate activeGate;
    void Start()
    {
        gatesSet = false;

    }

    private void OnDisable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!gatesSet)
        {
            gatesAssign();
            newLap();
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 coord1 = countedPlayer.gravityCenter.transform.position - transform.GetChild(i).transform.position;
                transform.GetChild(i).transform.up = -coord1;
                if (name == "OtherGates")
                {
                    transform.GetChild(i).transform.RotateAround(transform.position, transform.GetChild(i).transform.up, 180f);
                }
            }
        }

        if (gatesSet)
        {
            if (lastGate.currentState == Gate.gateStates.Active)
            {
                newLap();
                countedPlayer.finishedLaps++;
                lapEnd();

            }
        }
    }
    private void FixedUpdate()
    {
        if (gatesSet)
        {
            countedPlayer.transform.GetChild(7).LookAt(activeGate.transform);
            //countedPlayer.transform.GetChild(7).transform.forward = Vector3.Cross(countedPlayer.transform.GetChild(7).transform.forward, -countedPlayer.transform.up);
        }
    }
    public void newLap()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Gate>().currentState = Gate.gateStates.Inactive;
        }
        firstGate.currentState = Gate.gateStates.Waiting;
        activeGate = firstGate;
    }

    void gatesAssign()
    {
        firstGate = transform.GetChild(0).gameObject.GetComponent<Gate>();
        lastGate = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<Gate>();
        gatesSet = true;
    }
}
