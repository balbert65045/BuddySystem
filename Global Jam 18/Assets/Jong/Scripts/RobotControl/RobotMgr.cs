using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMgr : MonoBehaviour {
    public int robotCount;
    public RobotControl robotPrefab;

    RobotControl[] robotList;

    private void Start()
    {
        Vector3[] positions = new Vector3[robotCount];
        for (int i = 0; i < robotCount; i++)
        {
            positions[i] = new Vector3(i, 0, 0);
        }

        SpawnRobots(positions);
    }

    public void SpawnRobots(Vector3[] robotPositions)
    {
        robotCount = robotPositions.Length;

        robotList = new RobotControl[robotCount];
        for (int i = 0; i < robotCount; i++)
        {
            robotList[i] = Instantiate<RobotControl>(robotPrefab, robotPositions[i], Quaternion.identity);
        }
    }
    
    public void MoveRobots(float rotateAngle, int turnIdxIncrement, float turnDuration, float moveDuration)
    {
        foreach (var item in robotList)
        {
            //item.Move(rotateAngle, turnIdxIncrement, turnDuration, moveDuration);
        }

    }
}
