using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionInputMgr : MonoBehaviour
{

    bool buttonPressed;
    float buttonDownstart;
    float inputDelay;
    float targetRotateAngle;
    int orientationIncrement;

    public RobotMgr robotManager;

    [Range(0.1f, 0.5f)]
    public float turnDuration;
    [Range(0.1f, 2f)]
    public float moveDuration;


    private void Awake()
    {
        inputDelay = turnDuration + moveDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!buttonPressed)
        {
            //move forward in current direction
            if (Input.GetAxis("Vertical") > 0)
            {
                Debug.Log("move forward in current direction");
                targetRotateAngle = 0;
                orientationIncrement = 0;
                buttonPressed = true;
            }

            //turn around & move forward
            else if (Input.GetAxis("Vertical") < 0)
            {
                Debug.Log("turn around & move forward");
                targetRotateAngle = 180;
                orientationIncrement = 2;
                buttonPressed = true;
            }

            //turn left & move forward
            else if (Input.GetAxis("Horizontal") < 0)
            {
                Debug.Log("turn left & move forward");
                targetRotateAngle = 90;
                orientationIncrement = 1;
                buttonPressed = true;
            }

            //turn right & move forward
            else if (Input.GetAxis("Horizontal") > 0)
            {
                Debug.Log("turn right & move forward");
                targetRotateAngle = 270;
                orientationIncrement = 3;
                buttonPressed = true;
            }

            if (buttonPressed)
            {
                robotManager.MoveRobots(targetRotateAngle, orientationIncrement, turnDuration, moveDuration);
                buttonDownstart = Time.time;
            }
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.000001f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.000001f)
        {
            if (buttonDownstart + inputDelay < Time.time)
            {
                buttonPressed = false;
            }
        }
    }
}
