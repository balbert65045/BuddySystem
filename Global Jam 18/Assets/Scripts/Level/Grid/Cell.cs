using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public Coordinates coordinates;

    public bool hasRobot;

    public Robot occupyingBot;

    public Robot previousOccupant;

    public int cellID;

    public Cell teleportLink;

    public bool occupiedGoal;

    
    public CellType cellType;

    public Direction cellDirection; 

    public Sprite OpenSprite;
    Animator animator;

    private void Awake()
    {
        if (hasRobot)
        {
            occupyingBot.currentCell = this;
            occupyingBot.transform.position = this.transform.position + new Vector3(0f, 0.02f, 0f);
        }
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (cellType == CellType.Door)
        {
            animator.SetTrigger("Open");
        }
    }

    public void Teleport()
    {
        if (cellType == CellType.Teleporter)
        {
            animator.SetTrigger("Teleport");
        }
    }

    public void Goal()
    {
        if (cellType == CellType.Goal)
        {
            animator.SetTrigger("Goal");
        }
    }

    //Return can change based on various cell properties as more are added
    public bool CheckIsMovableCell()
    {
        if(cellType == CellType.Pillar || cellType == CellType.Door || 
            (cellType == CellType.Goal && occupiedGoal))
        {
            return false;
        } else 
        {
            return true;
        }
    }

    public void ClearCellOccupation()
    {
        hasRobot = false;
        occupyingBot = null;
    }

    public void UpdateCellOccupation(Robot bot)
    {
        if (hasRobot)
        {
            previousOccupant = occupyingBot;
        } else
        {
            hasRobot = true;
        }
        if (cellType == CellType.Goal) {Goal();}
        hasRobot = true;
        occupyingBot = bot;
        bot.currentCell = this;
    }

}

public enum CellType { Basic, Goal, Pillar, Trap, Door, Teleporter, Ledge, MoveArrow }