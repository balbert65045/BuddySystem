using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    [SerializeField]
    public CellGrid cells;

    //public Tile[] GoalTiles;
    int botsInGoal = 0;
    public int MinGoalTilesToPass = 2;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    public float transitionTime = 1f;
    //float WinTime;

    private List<Robot> deadBots;
    public List<Robot> botNet;

	// Use this for initialization
	void Start () {
        deadBots = new List<Robot>();
    }

    private void Update()
    {
        CheckForWinOrLose();
    }

    public void CheckForWinOrLose()
    {
        if (botsInGoal >= MinGoalTilesToPass && botNet.Count == 0)
        {
            Win();
        }
        //else if (deadBots.Count < MinGoalTilesToPass && botsInGoal < MinGoalTilesToPass)
        //{
        //    Lose();
        //}
    }

    void Win()
    {
        WinScreen.SetActive(true);
        //Debug.Log(SceneManager.sceneCountInBuildSettings);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Lose()
    {
        LoseScreen.SetActive(true);
    }

    public void ValidateMove(Direction targetDirection)
    {
        int index = 0;

        int warningID = -1;
        int previousCellID = -1;
        Robot previousCellBot = null;


        List<int> destinationIDs = new List<int>();

        foreach (Robot bot in botNet)
        {
            if (deadBots.Contains(bot) || bot.locked)
            {
                return;
            }
            //1. Ensure the bot is facing the target direction
            if(targetDirection != Direction.UP)
            {
                bot.Turn(targetDirection);
            }

            //2. Get the target cell
            Cell targetCell = cells.getTargetCell(bot.currentCell, bot.facingDirection);
           
            //3. If the returned cell is null, have the robot fall off of the screen, else check the cell
            if (targetCell == null)
            {
                //Kill robot(falling off of the screen)
                KillBot(bot);
            }
            else
            {
                //4. Check whether the cell is a valid cell to move to. If it is, move
                if (targetCell.CheckIsMovableCell())
                {
                    //Clear current cell so it's unoccupied, move, then update the new cell
                    bot.Move(targetCell.transform.position);
                    if (targetCell.hasRobot)
                    {
                        warningID = targetCell.cellID;
                        previousCellBot = bot;
                        previousCellID = bot.currentCell.cellID;
                    }
                    if (bot.currentCell.cellID == warningID && targetCell.cellID == previousCellID)
                    {
                        KillBot(bot);
                        KillBot(previousCellBot);
                    }

                    if (!bot.dead)
                    {
                        destinationIDs.Add(targetCell.cellID);

                        bot.currentCell.ClearCellOccupation();
                        targetCell.UpdateCellOccupation(bot);

                        SpecialCellCheck(targetCell, bot);
                    }

                }
            }

            index += 1;
        }

        

        List<int> checkedIDs = new List<int>();
        List<int> badIDs = new List<int>();

        foreach (int id in destinationIDs)
        {
            if (!checkedIDs.Contains(id))
            {
                checkedIDs.Add(id);
            }
            else
            {
                badIDs.Add(id);
            }
        }
        if (badIDs.Count > 0)
        {
            foreach (Robot bot in botNet)
            {
                if (badIDs.Contains(bot.currentCell.cellID))
                {
                    KillBot(bot);
                }
            }
        }
        

        botNet.RemoveAll(bot => bot.dead);
        botNet.RemoveAll(bot => bot.locked);
    }

    void KillBot(Robot bot)
    {
        bot.currentCell.ClearCellOccupation();
        bot.dead = true;
        bot.currentCell = null;
        //bot.Explode();
        deadBots.Add(bot);
        //botNet.Remove(bot);
        Destroy(bot.gameObject, .1f);
    }

    void SpecialCellCheck(Cell newCell, Robot bot)
    {
        Cell targetCell;

        switch (newCell.cellType)
        {
            case CellType.Goal:
                botsInGoal += 1;
                bot.locked = true;
                newCell.occupiedGoal = true;
                break;

            case CellType.MoveArrow:
                bot.ForceDirection(newCell.cellDirection);
                targetCell = cells.getTargetCell(newCell, newCell.cellDirection);
                bot.Move(targetCell.transform.position);
                bot.currentCell.ClearCellOccupation();
                targetCell.UpdateCellOccupation(bot);
                break;

            case CellType.Teleporter:
                targetCell = newCell.teleportLink;
                bot.gameObject.SetActive(false);
                bot.Move(targetCell.transform.position);
                bot.gameObject.SetActive(true);
                bot.currentCell.ClearCellOccupation();
                targetCell.UpdateCellOccupation(bot);
                break;

            case CellType.Trap:
                KillBot(bot);
                break;
        }
    }
}
