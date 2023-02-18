using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    public float rotateSpeed = 5;
    Vector3 offset;
    bool canMoveCamera;
    void Start()
    {
        offset = player.transform.position - transform.position;
        canMoveCamera = true;
        LapsDisplay.endOfRace += disableCamera;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        if (canMoveCamera)
        {
            //if(SceneManager.GetActiveScene().name == "SingleplayerScene")
            //{

            
            //float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            //float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            //player.transform.Rotate(vertical, horizontal, 0);

            //float desiredAngleHorizontal = player.transform.eulerAngles.y;
            //float desiredAngleVertical = player.transform.eulerAngles.x;
            //Quaternion rotation = Quaternion.Euler(desiredAngleVertical, desiredAngleHorizontal, 0);
            //transform.position = player.transform.position - (rotation * offset);

            //transform.LookAt(player.transform);
            //}
        }
    }

    public void disableCamera()
    {
        canMoveCamera = false;
    }

    public void enableCamera()
    {
        canMoveCamera = true;
    }
}
