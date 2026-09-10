using UnityEngine;

public class RunManager : Singleton<RunManager>
{
    [SerializeField] PlayerController m_playerController;



    private PlayerEntity m_player;



    public PlayerEntity Player => m_player;




    public void SetPlayer(PlayerEntity player)
    {
        m_player = player;
    }
}
