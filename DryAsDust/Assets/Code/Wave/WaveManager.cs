using NUnit.Framework;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] m_waves;

    [SerializeField] private int m_currentWave;

    public Transform m_bigInsectList;
    public Transform m_shooterList;

    [SerializeField] private float m_spawnRadius = 10f;
    [SerializeField] private float m_spawnInnerRadius = 5f;

    private Vector3 GetRandomSpawnPosition()
    {
        float innerRadius = Mathf.Max(0f, m_spawnInnerRadius);
        float outerRadius = Mathf.Max(innerRadius, m_spawnRadius);

        float angle = Random.Range(0f, Mathf.PI * 2f);

        
        float radius = Mathf.Sqrt(Random.Range(
            innerRadius * innerRadius,
            outerRadius * outerRadius
        ));

        return transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius,0f );
    }
    private void OnDrawGizmosSelected()
    {
        DrawSpawnCircle(m_spawnRadius, Color.green);
        DrawSpawnCircle(m_spawnInnerRadius, Color.red);
      
    }

    private void DrawSpawnCircle(float radius, Color color)
    {
        const int segments = 64;
        Gizmos.color = color;

        Vector3 previousPoint = transform.position
            + new Vector3(radius, 0f, 0f);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;

            Vector3 point = transform.position + new Vector3(
                Mathf.Cos(angle) * radius,
                0f,
                Mathf.Sin(angle) * radius
            );

            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
    private void Start()
    {
        m_currentWave = 0;
        m_bigInsectList = GameManager.Instance.transform.Find("Pool_BigInsectEnemy");
        m_shooterList = GameManager.Instance.transform.Find("Pool_EnemyShooter");
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (m_waves == null ||
            m_currentWave < 0 ||
            m_currentWave >= m_waves.Length)
        {
            return;
        }

        ShooterEntity[] shooters = m_shooterList.GetComponentsInChildren<ShooterEntity>(true);

        int shootersSpawned = 0;

        foreach (ShooterEntity shooter in shooters)
        {
            if (shootersSpawned >= m_waves[m_currentWave].shooterCount)
                break;

            if (shooter.gameObject.activeSelf)
                continue;

            shooter.transform.position = GetRandomSpawnPosition();
            shooter.gameObject.SetActive(true);
            shootersSpawned++;
        }

        BigInsectEntity[] insects =
            m_bigInsectList.GetComponentsInChildren<BigInsectEntity>(true);

        int insectsSpawned = 0;

        foreach (BigInsectEntity insect in insects)
        {
            if (insectsSpawned >= m_waves[m_currentWave].bigInsectCount)
                break;

            if (insect.gameObject.activeSelf)
                continue;

            insect.transform.position = GetRandomSpawnPosition();
            insect.gameObject.SetActive(true);
            insectsSpawned++;
        }
    }

}
