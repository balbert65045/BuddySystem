using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BensLevelManager : MonoBehaviour
{

    // Use this for initialization
    public Cell[] GoalTiles;
    public Cell DoorTile;

    public int MaxGoalTilesToPass = 2;
    public int MinGoalTilesToPass = 2;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private RobotsBen[] BotsLeft;
    public RobotsBen[] botNet;

    //bool BotRecentlyDied = false;
    //float TimeBotDied;
    //bool BotReachedGoal = false;
    //float TimeBotReachedGoal;
    //float CheckForWinTime = 2.5f;

    void Start()
    {
        Time.timeScale = 1;
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        botNet = FindObjectsOfType<RobotsBen>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (BotRecentlyDied)
        //{
        //    if (TimeBotDied + CheckForWinTime < Time.time)
        //    {
        //        BotRecentlyDied = false;
        //        CheckForWinOrLose();
        //    }
        //}
        //else if (BotReachedGoal)
        //{
        //    if (TimeBotReachedGoal + CheckForWinTime < Time.time)
        //    {
        //        BotReachedGoal = false;
        //        CheckForWin();
        //    }
        //}
    }

    public void GoalReached()
    {
        StartCoroutine("CheckForWin");
    }

    IEnumerator CheckForWin()
    {
        int Goal = 0;
        BotsLeft = FindObjectsOfType<RobotsBen>();
        int NumberLoss = botNet.Length - BotsLeft.Length;
        foreach (Cell cell in GoalTiles)
        {
            if (cell.hasRobot) { Goal++; }
            if (Goal == MinGoalTilesToPass)
            {
                DoorTile.Open();
            }

            if (Goal >= MaxGoalTilesToPass - NumberLoss)
            {
                yield return new WaitForSeconds(.5f);
                Win();
            }
        }
        yield return null;
    }

    public void BotDied()
    {
        StartCoroutine("CheckForWinOrLose");
    }

    IEnumerator CheckForWinOrLose()
    {
        Debug.Log("Checking for lose");
        yield return new WaitForSeconds(2.5f);
        BotsLeft = FindObjectsOfType<RobotsBen>();
        if (BotsLeft.Length < MinGoalTilesToPass)
        {

            Lose();
            yield return null;
        }
        Debug.Log("Looking for win");
        StartCoroutine("CheckForWin");
        yield return null;
    }



    void Win()
    {

        Time.timeScale = 0;
        WinScreen.gameObject.SetActive(true);
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Lose()
    {
        Time.timeScale = 0;
        LoseScreen.gameObject.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
