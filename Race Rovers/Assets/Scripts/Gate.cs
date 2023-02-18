using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum gateStates { Inactive, Active, Waiting}
    public gateStates currentState = gateStates.Inactive;
    public Light gateLight;
    // Start is called before the first frame update
    void Start()
    {
        gateLight = transform.GetChild(2).GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == gateStates.Inactive)
        {
            gateLight.color = Color.red;
        }
        else if (currentState == gateStates.Waiting)
        {
            gateLight.color = Color.yellow;
        }
        else if(currentState == gateStates.Active)
        {
            gateLight.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentState == gateStates.Waiting && other.gameObject.GetComponent<Player>().tag == transform.GetComponentInParent<GateContainer>().countedPlayer.tag)
        {
            currentState = gateStates.Active;
            if(GetNext())
            { 
            GetNext().GetComponent<Gate>().currentState = gateStates.Waiting;
            transform.parent.GetComponent<GateContainer>().activeGate = GetNext().GetComponent<Gate>();
            }
        }    
    }

    Transform GetNext()
    {
        var myself = transform;
        var parent = transform.parent;
        var childCount = parent.childCount;
        for (int i = 0; i < childCount - 1; i++)
        { // skip the last, as it doesn't have a successor
            if (parent.GetChild(i) == myself)
                return parent.GetChild(i + 1);
        }
        return null;
    }
}
