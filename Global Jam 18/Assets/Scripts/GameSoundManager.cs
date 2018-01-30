using UnityEngine;

public class GameSoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] jumpSounds;
    public AudioClip jumpLandSound;

    private static GameSoundManager m_instance = null;
    public static GameSoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                var prefab = Resources.Load<GameObject>("GameSoundManager");
                if (prefab == null) Debug.LogError("Can't load GameSoundManager from Resources");
                var instance = Instantiate(prefab);
                if (instance == null) Debug.LogError("Instance of GameSoundManager prefab is null");
                m_instance = instance.GetComponent<GameSoundManager>();
                if (m_instance == null) Debug.LogError("No GameSoundManager found on prefab instance.");
            }
            return m_instance;
        }
    }

    public void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            DestroyObject(gameObject);
            return;
        }
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }
}