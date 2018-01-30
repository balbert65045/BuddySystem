using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                var go = new GameObject("GameManager");
                m_instance = go.AddComponent<GameManager>();
            }
            return m_instance;
        }
    }


    void Awake()
    {
        if (m_instance != this && m_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        DontDestroyOnLoad(this.gameObject);
    } //End Awake



} //End class