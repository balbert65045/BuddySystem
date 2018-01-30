using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    public bool Occupied = false;


    //Check for RobotController
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Occupied = true;
    }

    //Check For RobotController
    private void OnTriggerExit2D(Collider2D collision)
    {
        Occupied = false;
    }

}
