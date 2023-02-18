using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float steerSpeed;
    public float rotateSpeed;
    public float m_Thrust;
    public float gravityStrenght;
    Rigidbody m_Rigidbody;
    Vector3 steerDirection;
    bool isMoving;
    bool canMove;
    Vector3 startPosition;
    Quaternion startRotation;
    Vector3 carFront;
    Vector3 carUp;
    Vector3 leftBorder;
    Vector3 rightBorder;
    float planetDistance;
    // Start is called before the first frame update
    public GameObject gravityCenter;

    public int finishedLaps;

    void Start()
    {
        carFront = transform.GetChild(1).gameObject.transform.position - transform.position;
        carUp = transform.GetChild(2).gameObject.transform.position - transform.position;
        steerDirection = transform.GetChild(3).gameObject.transform.position - transform.position;
        leftBorder = transform.GetChild(4).gameObject.transform.position - transform.position;
        rightBorder = transform.GetChild(5).gameObject.transform.position - transform.position;
        startPosition = transform.position;
        startRotation = transform.rotation;
        steerSpeed = 90.0f;
        rotateSpeed = 120.0f;
        m_Thrust = 0f;
        m_Rigidbody = GetComponent<Rigidbody>();
        planetDistance = gravityCenter.transform.localScale.x / 2;
        isMoving = false;
        canMove = true;
        LapsDisplay.endOfRace += enterMenu;
        finishedLaps = 0;
    }

    private void OnDisable()
    {
        LapsDisplay.endOfRace -= enterMenu;
    }
    // Update is called once per frame
    void Update()
    {
        carFront = transform.GetChild(1).gameObject.transform.position - transform.position;
        //transform.Rotate(0.0f, -Time.deltaTime * rotateSpeed, 0.0f);
        //transform.Translate(0.0f, 0.0f, -Time.deltaTime);
        //m_Rigidbody.AddForce(carUp*3);


    }

    void FixedUpdate()
    {
        m_Rigidbody.AddForce(Vector3.Normalize(gravityCenter.transform.position - transform.position) * gravityStrenght * Mathf.Pow((1+(gravityCenter.transform.position - transform.position).magnitude-planetDistance),2));        
        leftBorder = transform.GetChild(4).gameObject.transform.position - transform.position;
        rightBorder = transform.GetChild(5).gameObject.transform.position - transform.position;
        carUp = transform.GetChild(2).gameObject.transform.position - transform.position;
        steerDirection = transform.GetChild(3).gameObject.transform.position - transform.position;
        if (canMove)
        {
            if (transform.tag == "Player")
            {
                playerControls(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
            }
            else if (transform.tag == "Player2")
            {
                playerControls(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
            }
                   
            if (m_Thrust != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }            
            if (isMoving)
            {
                //Vector3 newDirection = Vector3.RotateTowards(transform.forward, steerDirection, 1.0f, 0.0f);
                //transform.rotation = Quaternion.LookRotation(newDirection);
                //Debug.Log("Before: " + transform.rotation +" Vector: " + newDirection + "Steer: " + steerDirection);
                //Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(newDirection), Time.deltaTime * rotateSpeed);
                if (Vector3.SignedAngle(steerDirection, carFront, carUp) > 1.0f)
                {
                    transform.Rotate(0.0f, -Time.deltaTime * rotateSpeed, 0.0f);
                }
                else if(Vector3.SignedAngle(steerDirection, carFront, carUp) < -1.0f)
                {
                    transform.Rotate(0.0f, Time.deltaTime * rotateSpeed, 0.0f);
                }
                //Debug.Log("Angle: " + Vector3.SignedAngle(steerDirection, carFront, carUp));
                //transform.rotation = Quaternion.Euler(steerDirection);
                //thrustDirection = newDirection;
                
            }
            m_Rigidbody.AddForce(carFront * m_Thrust);
            Debug.DrawRay(transform.position, steerDirection, Color.green);;
            Debug.DrawRay(transform.position, m_Rigidbody.velocity, Color.red);
            Debug.DrawRay(transform.position, carFront, Color.cyan);
        }
    }

    public void resetPlayer()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        m_Rigidbody.velocity = Vector3.zero;
        canMove = true;
        Time.timeScale = 1;
        finishedLaps = 0;
    }

    public void enterMenu()
    {
        canMove = false;
    }

    public void playerControls(KeyCode forward, KeyCode backward, KeyCode leftTurn, KeyCode rightTurn)
    {
        if (Input.GetKey(leftTurn))
        {
            //if(isMoving)
            //{
            //    
            //}

            //steerDirection.x = transform.eulerAngles.y;
            //steerDirection = Vector3.RotateTowards(steerDirection, transform.rotation, Time.deltaTime, 0.0f);
            //if (steerDirection.x > transform.rotation.y - 0.75)
            if (Vector3.Angle(steerDirection, carFront) < 40)
            {
                //steerDirection = Quaternion.Euler(0, -Time.deltaTime * steerSpeed, 0) * steerDirection;
                steerDirection = Vector3.RotateTowards(steerDirection, leftBorder, Time.deltaTime, 0.0f);
                transform.GetChild(3).gameObject.transform.position = transform.position + steerDirection;
            }
        }
        else if (Input.GetKey(rightTurn))
        {
            //if (isMoving)
            //{
            //    transform.Rotate(0.0f, -Time.deltaTime * rotateSpeed, 0.0f);
            //}
            //steerDirection.x = transform.eulerAngles.y;
            //if (steerDirection.x < transform.rotation.y + 0.75)
            if (Vector3.Angle(steerDirection, carFront) < 40)
            {
                //steerDirection = Quaternion.Euler(0, Time.deltaTime * steerSpeed, 0) * steerDirection;
                steerDirection = Vector3.RotateTowards(steerDirection, rightBorder, Time.deltaTime, 0.0f);
                transform.GetChild(3).gameObject.transform.position = transform.position + steerDirection;
            }
        }
        if (Input.GetKey(forward))
        {
            while (m_Thrust < 20)
            {
                m_Thrust += 5 * Time.deltaTime;
            }
            if (!Input.GetKey(leftTurn) && !Input.GetKey(rightTurn))
            {
                steerDirection = carFront;
            }

        }
        else if (Input.GetKey(backward))
        {
            while (m_Thrust > -15)
            {
                m_Thrust -= 3 * Time.deltaTime;
            }
            if (!Input.GetKey(leftTurn) && !Input.GetKey(rightTurn))
            {
                steerDirection = carFront;
            }
        }
        else
        {
            //m_Rigidbody.velocity = m_Rigidbody.velocity*0.9f;
            Debug.Log("Wielkoœæ wektora: " + Vector3.Magnitude(m_Rigidbody.velocity));
            //steerDirection = Vector3.RotateTowards(steerDirection, carFront, Time.deltaTime, 0.0f);
            //steerDirection = carFront;
            while (m_Thrust != 0)
            {
                if (m_Thrust > 0)
                {
                    m_Thrust -= 3 * Time.deltaTime;
                    if (m_Thrust < 0)
                        m_Thrust = 0;
                }
                else
                {
                    m_Thrust += 3 * Time.deltaTime;
                    if (m_Thrust > 0)
                        m_Thrust = 0;
                }

            }
           
        }
    }
}
