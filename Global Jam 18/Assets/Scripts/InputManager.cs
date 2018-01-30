using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    bool buttonPressed;

    float buttonDownstart;
    [Range(0.2f, .5f)]
    public float inputDelay = .2f;
    [Range(0.1f, 0.2f)]
    public float moveDuration = .2f;

    public LevelManager levelManager;

    // Update is called once per frame
    void Update()
    {
        if (buttonDownstart + inputDelay > Time.time)
        {
            return;
        }

        if (!buttonPressed)
        {
            //move forward in current direction
            if (Input.GetAxis("Vertical") > 0)
            {
                Debug.Log("Try to move up");
                buttonPressed = true;

                levelManager.ValidateMove(Direction.UP);
                buttonDownstart = Time.time;
            }

            //turn around & move forward
            else if (Input.GetAxis("Vertical") < 0)
            {
                Debug.Log("Try to move down");
                buttonPressed = true;

                levelManager.ValidateMove(Direction.DOWN);
                buttonDownstart = Time.time;
            }

            //turn left & move forward
            else if (Input.GetAxis("Horizontal") < 0)
            {
                Debug.Log("Try to move left");
                buttonPressed = true;

                levelManager.ValidateMove(Direction.LEFT);
                buttonDownstart = Time.time;
            }

            //turn right & move forward
            else if (Input.GetAxis("Horizontal") > 0)
            {
                Debug.Log("Try to move right");
                buttonPressed = true;

                levelManager.ValidateMove(Direction.RIGHT);
                buttonDownstart = Time.time;
            }
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.000001f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.000001f)
        {
            buttonPressed = false;
        }
    }
}

//Directions are relative to the robot and its facing direction
public enum Direction
{
    UP, DOWN, LEFT, RIGHT
}
