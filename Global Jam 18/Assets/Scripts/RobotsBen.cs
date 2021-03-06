﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsBen : MonoBehaviour
{

    // Use this for initialization
    public enum DirectionFacing { Up, Down, Left, Right }
    public DirectionFacing CurrentFacingDirection;

    public Sprite[] Sprites;
    SpriteRenderer MySprite;

    private float MoveDistance = 10;
    Vector3 MovePosition;

    Animator animator;
    bool Locked = false;

    BensLevelManager levelManager;

    public bool Teleporting = false;

    public bool Moving = false;
    float moveEndTime;
    float moveStartTime;
    public float moveDuration = .4f;

    void Start()
    {
        MovePosition = transform.position;
        MySprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        levelManager = FindObjectOfType<BensLevelManager>();
        switch (CurrentFacingDirection)
        {
            case DirectionFacing.Up:
             //   Debug.Log("Turn up");
                animator.SetBool("FacingDown", false);
                animator.SetBool("FacingSide", false);
                animator.SetBool("FacingUp", true);
               // animator.SetTrigger("TurnUp");
                break;
            case DirectionFacing.Down:
                animator.SetBool("FacingUp", false);
                
                animator.SetBool("FacingSide", false);
                animator.SetBool("FacingDown", true);
               // animator.SetTrigger("TurnDown");
                break;
            case DirectionFacing.Left:
                animator.SetBool("FacingUp", false);
                animator.SetBool("FacingDown", false);
                animator.SetBool("FacingSide", true);
              //  animator.SetTrigger("TurnSide");
             //   MySprite.flipX = true;
                break;
            case DirectionFacing.Right:
                animator.SetBool("FacingUp", false);
                animator.SetBool("FacingDown", false);
                animator.SetBool("FacingSide", true);
            //    animator.SetTrigger("TurnSide"); 
             //   MySprite.flipX = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (moveEndTime > Time.time)
        {

            Moving = true;
            float percent = Mathf.Min(1, (Time.time - moveStartTime) / moveDuration);
            transform.position = Vector3.Lerp(transform.position, MovePosition, percent);
            switch (CurrentFacingDirection)
            {
                case DirectionFacing.Down:
                    animator.SetBool("MoveUp", false);
                    animator.SetBool("MoveSide", false);
                    animator.SetBool("MoveDown", true);
                    break;
                case DirectionFacing.Up:
                    animator.SetBool("MoveDown", false);
                    animator.SetBool("MoveSide", false);
                    animator.SetBool("MoveUp", true);
                    break;
                case DirectionFacing.Left:
                    animator.SetBool("MoveUp", false);
                    animator.SetBool("MoveDown", false);
                    animator.SetBool("MoveSide", true);
                    break;
                case DirectionFacing.Right:
                    animator.SetBool("MoveUp", false);
                    animator.SetBool("MoveDown", false);
                    animator.SetBool("MoveSide", true);
                    break;
            }
            return;
        }
        Moving = false; ;
        animator.SetBool("MoveDown", false);
        animator.SetBool("MoveUp", false);
        animator.SetBool("MoveSide", false);
        //CheckForInput();

    }

    public void MoveTowards(DirectionFacing DirectionToMove)
    {
        if (Locked) { return; }
        switch (DirectionToMove)
        {
            case DirectionFacing.Right:
                MoveToMyRight();
                break;
            case DirectionFacing.Left:
                MoveToMyLeft();
                break;
            case DirectionFacing.Up:
                MoveForward(null);
                break;
            case DirectionFacing.Down:
                MoveBackWard();
                break;
        }
    }

    void MoveToMyLeft()
    {
        Vector3 MoveDirection = Vector3.zero;
        switch (CurrentFacingDirection)
        {
            case DirectionFacing.Up:
                //animator.SetTrigger("SignalRecieved_Up");
                FaceLeft(null);
                break;
            case DirectionFacing.Down:
                // animator.SetTrigger("SignalRecieved_Down");
                FaceRight(null);
                break;
            case DirectionFacing.Left:
                // animator.SetTrigger("SignalRecieved_Side");
                FaceDown(null);
                break;
            case DirectionFacing.Right:
                // animator.SetTrigger("SignalRecieved_Side");
                FaceUp(null);
                break;
        }
      //  MovePosition = transform.position + (MoveDirection * MoveDistance);
    }

    void MoveToMyRight()
    {
        Vector3 MoveDirection = Vector3.zero;
        switch (CurrentFacingDirection)
        {
            case DirectionFacing.Up:
                //  animator.SetTrigger("SignalRecieved_Up");
                FaceRight(null);
                break;
            case DirectionFacing.Down:
                //   animator.SetTrigger("SignalRecieved_Down");
                FaceLeft(null);
                break;
            case DirectionFacing.Left:
                //  animator.SetTrigger("SignalRecieved_Side");
                FaceUp(null);
                break;
            case DirectionFacing.Right:
                //  animator.SetTrigger("SignalRecieved_Side");
                FaceDown(null);
                // MySprite.sprite = Sprites[0];
                break;
        }
        //MovePosition = transform.position + (MoveDirection * MoveDistance);
    }

    void MoveBackWard()
    {
        Vector3 MoveDirection = Vector3.zero;
        switch (CurrentFacingDirection)
        {
            case DirectionFacing.Up:
                //   animator.SetTrigger("SignalRecieved_Up");
                FaceDown(null);
                //   MySprite.sprite = Sprites[0];
                break;
            case DirectionFacing.Down:
                //  animator.SetTrigger("SignalRecieved_Down");
                FaceUp(null);
                break;
            case DirectionFacing.Left:
                // animator.SetTrigger("SignalRecieved_Side");
                FaceRight(null);
                break;
            case DirectionFacing.Right:
                //  animator.SetTrigger("SignalRecieved_Side");
                FaceLeft(null);
                break;
        }
        //  MovePosition = transform.position + (MoveDirection * MoveDistance);
    }

    void MoveForward(Cell cell)
    {
        Vector3 MoveDirection = Vector3.zero;
        switch (CurrentFacingDirection)
        {
            case DirectionFacing.Up:
             //   animator.SetTrigger("SignalRecieved_Up");
                MoveDirection = Vector3.forward;
                break;
            case DirectionFacing.Down:
               // animator.SetTrigger("SignalRecieved_Down");
                MoveDirection = Vector3.back;
                break;
            case DirectionFacing.Left:
              //  animator.SetTrigger("SignalRecieved_Side");
                MoveDirection = Vector3.left;
                break;
            case DirectionFacing.Right:
             //   animator.SetTrigger("SignalRecieved_Side");
                MoveDirection = Vector3.right;
                break;
        }
       FaceStraight(MoveDirection, cell);

    }

    bool CheckForPillar(Vector3 Direction)
    {
        RaycastHit Hit;
        bool Move = true;
        if (Physics.Raycast(transform.position, Direction, out Hit, 10f))
        {
            if (Hit.transform.GetComponent<Cell>())
            {
                if (Hit.transform.GetComponent<Cell>().cellType == CellType.Pillar || Hit.transform.GetComponent<Cell>().cellType == CellType.Door)
                {
                    Move = false;
                }
            }
        }
        return (Move);
    }

    void FaceStraight(Vector3 Direcion, Cell arrowCell)
    {
        //yield return new WaitForSeconds(.5f);

        bool Move = CheckForPillar(Direcion);
        if (Move) {
            if (arrowCell == null) { MovePosition = transform.position + (Direcion * MoveDistance); }
            else { MovePosition = arrowCell.transform.position + (Direcion * MoveDistance); }
            moveEndTime = Time.time + moveDuration;
            moveStartTime = Time.time;
        }
        else { MovePosition = transform.position; }
       
    }


        void FaceRight(Cell arrowCell)
    {

      //  yield return new WaitForSeconds(.5f);
        CurrentFacingDirection = DirectionFacing.Right;
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingDown", false);
        animator.SetBool("FacingSide", true);
        MySprite.flipX = false;
        Vector3 MoveDirection = Vector3.right;
        bool Move = CheckForPillar(MoveDirection);
        if (Move)
        {
            if (arrowCell == null) { MovePosition = transform.position + (MoveDirection * MoveDistance); }
            else { MovePosition = arrowCell.transform.position + (MoveDirection * MoveDistance); }
            moveEndTime = Time.time + moveDuration;
            moveStartTime = Time.time;
        }
        else { animator.SetTrigger("TurnSide"); }
       
    }

    void FaceLeft(Cell arrowCell)
    {
      //  yield return new WaitForSeconds(.5f);
        CurrentFacingDirection = DirectionFacing.Left;
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingDown", false);
        animator.SetBool("FacingSide", true);
        MySprite.flipX = true;
        Vector3 MoveDirection = Vector3.left;
        bool Move = CheckForPillar(MoveDirection);
        if (Move)
        {
            if (arrowCell == null) { MovePosition = transform.position + (MoveDirection * MoveDistance); }
            else { MovePosition = arrowCell.transform.position + (MoveDirection * MoveDistance); }
            moveEndTime = Time.time + moveDuration;
            moveStartTime = Time.time;
        }
        else { animator.SetTrigger("TurnSide"); }
       
    }

    void FaceUp(Cell arrowCell)
    {
      //  yield return new WaitForSeconds(.5f);
        CurrentFacingDirection = DirectionFacing.Up;
        // MySprite.sprite = Sprites[1];
        animator.SetBool("FacingDown", false);
        animator.SetBool("FacingSide", false);
        animator.SetBool("FacingUp", true);
        Vector3 MoveDirection = Vector3.forward;
        bool Move = CheckForPillar(MoveDirection);
        if (Move)
        {
            if (arrowCell == null) { MovePosition = transform.position + (MoveDirection * MoveDistance); }
            else { MovePosition = arrowCell.transform.position + (MoveDirection * MoveDistance); }
            moveEndTime = Time.time + moveDuration;
            moveStartTime = Time.time;
        }
        else { animator.SetTrigger("TurnUp"); }
      
    }

    void FaceDown(Cell arrowCell)
    {
        //yield return new WaitForSeconds(.5f);
        CurrentFacingDirection = DirectionFacing.Down;
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingSide", false);
        animator.SetBool("FacingDown", true);
        Vector3 MoveDirection = Vector3.back;
        bool Move = CheckForPillar(MoveDirection);
        if (Move)
        {
            if (arrowCell == null) { MovePosition = transform.position + (MoveDirection * MoveDistance); }
            else { MovePosition = arrowCell.transform.position + (MoveDirection * MoveDistance); }
            moveEndTime = Time.time + moveDuration;
            moveStartTime = Time.time;
        }
        else { animator.SetTrigger("TurnDown"); }
     
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RobotsBen>())
        {
            Explode();
        }

        if (other.GetComponent<Cell>())
        {
            //Debug.Log(other.GetComponent<Cell>().cellType);
            switch (other.GetComponent<Cell>().cellType)
            {
                case CellType.Basic:
                    Teleporting = false;
                    break;
                case CellType.Goal:
                    other.GetComponent<Cell>().Goal();
                    other.GetComponent<Cell>().hasRobot = true;
                    levelManager.GoalReached();
                    Locked = true;
                    break;
                case CellType.Ledge:
                    Fall();
                    break;
                case CellType.MoveArrow:
                    Teleporting = false;
                    MoveFromArrow(other.GetComponent<Cell>().cellDirection, other.GetComponent<Cell>());
                    break;
                case CellType.Trap:
                    Explode();
                    break;
                case CellType.Teleporter:
                    if (!Teleporting)
                    {
                 //       Debug.Log("Teleport");
                        other.GetComponent<Cell>().Teleport();
                        other.GetComponent<Cell>().teleportLink.Teleport();
                        transform.position = other.GetComponent<Cell>().teleportLink.transform.position;
                        MovePosition = transform.position;
                   //     Debug.Log(other.GetComponent<Cell>().teleportLink);
                   //   MoveForward(other.GetComponent<Cell>().teleportLink);
                    }
                    Teleporting = true;
                    break;
            }
        }
    }

    void MoveFromArrow(DirectionFacing Direction, Cell cell)
    {
       // yield return new WaitForSeconds(1f);
        if (Direction == CurrentFacingDirection)
        {
            MoveForward(cell);
        }
        else if (Direction == DirectionFacing.Up)
        {
            FaceUp(cell);
        }
        else if (Direction == DirectionFacing.Down)
        {
            FaceDown(cell);
        }
        else if (Direction == DirectionFacing.Right)
        {
            FaceRight(cell);
        }
        else if (Direction == DirectionFacing.Left)
        {
            FaceLeft(cell);
        }
    }

    public void Fall()
    {
        animator.SetTrigger("Fall");
        GetComponent<BoxCollider>().enabled = false;
        //   levelManager.BotDied();
        //    Destroy(gameObject, 2f);
    }

    public void Explode()
    {
       
        animator.SetTrigger("Death");
        GetComponent<BoxCollider>().enabled = false;
     //   levelManager.BotDied();
     //   Destroy(gameObject, .8f);
    }

    public void BotDied()
    {
        Debug.Log("Bot Died");
        Destroy(gameObject);
        levelManager.BotDied();
        
    }

}
