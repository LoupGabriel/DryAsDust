using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GUI;
public class CheatManager : MonoBehaviour
{
    public static CheatManager Instance { get; private set; }

    private bool m_showWindow = false;
    private Rect m_winRect = new Rect(20, 20, 250, 500);

    private string m_Amount = "1";
    private bool m_giveHp;
    private bool m_removeHP;


    public Action<int> OnDamage;
    public Action<int> OnHeal;

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.f1Key.wasPressedThisFrame)
        {
            m_showWindow = !m_showWindow;

        }
    }


    private void OnGUI()
    {
        if (m_showWindow)
        {
            m_winRect = GUI.Window(0, m_winRect, WindowFunction, "CheatManager");
        }
    }

    private void WindowFunction(int windowID)
    {
        GUILayout.Label("Player");

        GUILayout.Label("Health");

        m_Amount = GUILayout.TextField(m_Amount);

        GUILayout.BeginHorizontal();


        if(GUILayout.Button("Heal"))
        {
            int amount = int.Parse(m_Amount);
            OnHeal?.Invoke(amount);
        }
        if (GUILayout.Button("Damage"))
        {
            int amount = int.Parse(m_Amount);
            OnDamage?.Invoke(amount);
        }


        GUILayout.EndHorizontal();

        


    }
}
