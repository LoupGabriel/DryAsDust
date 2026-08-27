using UnityEngine;

public class BulletItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        gameObject.SetActive(false);

    }
}

