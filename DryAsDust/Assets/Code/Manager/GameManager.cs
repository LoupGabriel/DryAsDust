using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public SceneTransitionManager m_sceneTransitionManager;
    public static GameManager GetInstance()
    {
        return m_instance;
    }
    private void Awake()
    {
       if(m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        LoadManager();


    }

    private void Start()
    {
        m_sceneTransitionManager.LoadScene("MainMenu");
    }


    private void LoadManager()
    {
        m_sceneTransitionManager = GetComponent<SceneTransitionManager>();
    }
}
