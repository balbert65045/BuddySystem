using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robots : MonoBehaviour {

    // Use this for initialization
    public enum DirectionFacing { Up,Down,Left,Right }
    public DirectionFacing CurrentFacingDirection;

    public Sprite[] Sprites;

    public float speed = 1;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetButtonDown("Right"))
        {
            switch (CurrentFacingDirection)
            {
                case DirectionFacing.Up:
                    CurrentFacingDirection = DirectionFacing.Right;
                    break;
                case DirectionFacing.Down:
                    CurrentFacingDirection = DirectionFacing.Left;
                    break;
                case DirectionFacing.Left:
                    CurrentFacingDirection = DirectionFacing.Up;
                    break;
                case DirectionFacing.Right:
                    CurrentFacingDirection = DirectionFacing.Down;
                    break;
            }
        }
        else if (Input.GetButtonDown("Left"))
        {

        }
        else if (Input.GetButtonDown("Up"))
        {

        }
        else if (Input.GetButtonDown("Down"))
        {

        }


        

        
		
	}
}
