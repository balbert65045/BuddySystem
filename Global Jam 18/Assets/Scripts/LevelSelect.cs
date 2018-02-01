using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    GameSoundManager musicManager;

    private void Start()
    {
        musicManager = FindObjectOfType<GameSoundManager>();
      //  OptionsMenu.SetActive(false);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        musicManager.FindSlider();
        OptionsMenu.GetComponentInChildren<Slider>().value = musicManager.GetComponent<AudioSource>().volume;
    }

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

}
