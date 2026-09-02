using System.Collections;
using System.Timers;
using UnityEngine;

public class BulletItem : MonoBehaviour
{

    [SerializeField] private float m_bulletLifetime = 1f;

    private void Awake()
    {
        StartCoroutine(BulletLife());
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        gameObject.SetActive(false);

    }

    private IEnumerator BulletLife()
    {
        float elapse = 0;
        while (elapse < m_bulletLifetime)
        {
            elapse += Time.deltaTime;
            yield   return null;
        }
        gameObject.SetActive(false);
    }
}

