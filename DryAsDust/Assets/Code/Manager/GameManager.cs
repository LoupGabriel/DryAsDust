using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Manager")]
    [SerializeField] private SceneTransitionManager m_sceneTransitionManager;
    [SerializeField] private BulletPool m_bulletPool;
    [SerializeField] private PoolManager m_poolManager;

    //Getter
    public SceneTransitionManager SceneTransitionManager => m_sceneTransitionManager;
    public BulletPool BulletPool => m_bulletPool;
    public PoolManager PoolManager => m_poolManager;


    protected override void Awake()
    {
        base.Awake();
        InitManager();
    }

    private void Start()
    {
        m_sceneTransitionManager.LoadScene("MainMenu");
        SoundtrackManager.Instance.PlayMusic("Ambience");
    }

    private void InitManager()
    {
        m_poolManager.Init();
    }
}


 
