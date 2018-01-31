using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BensInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Right"))
        {
            CheckForRobotsMoving(RobotsBen.DirectionFacing.Right);
        }
        else if (Input.GetButtonDown("Left"))
        {
            CheckForRobotsMoving(RobotsBen.DirectionFacing.Left);
        }
        else if (Input.GetButtonDown("Up"))
        {
            CheckForRobotsMoving(RobotsBen.DirectionFacing.Up);

        }
        else if (Input.GetButtonDown("Down"))
        {
            CheckForRobotsMoving(RobotsBen.DirectionFacing.Down);
        }
    }

    void CheckForRobotsMoving(RobotsBen.DirectionFacing Direction)
    {
        RobotsBen[] RobotsOut = FindObjectsOfType<RobotsBen>();
        bool AllRobotsStopped = true;
        foreach (RobotsBen Robot in RobotsOut)
        {
            if (Robot.Moving) { AllRobotsStopped = false; }
        }

        if (AllRobotsStopped)
        {
            foreach (RobotsBen Robot in RobotsOut)
            {
                Robot.MoveTowards(Direction);
            }
        }

    }
}
