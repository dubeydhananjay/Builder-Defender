using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveMessageText;
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    [SerializeField] private RectTransform enemyWaveSpawnPositionIndicator;
    [SerializeField] private RectTransform closestEnemyIndicator;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        SetWaveNumberText("Wave " + enemyWaveManager.WaveNumber);
        SetWaveMessageText("Next Wave in " + enemyWaveManager.NextSpawnWaveTimer.ToString("F1") + "s");
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManagerOnWaveNumberChanged;
    }


    void Update()
    {
        HandleWaveMessages();
        HandleWaveSpawnPositionIndicator();
        LookForClosestEnemy();
    }

    private void HandleWaveMessages()
    {
        if (enemyWaveManager.NextSpawnWaveTimer > 0)
            SetWaveMessageText("Next Wave in " + enemyWaveManager.NextSpawnWaveTimer.ToString("F1") + "s");
        else SetWaveMessageText("");
    }

    private void HandleWaveSpawnPositionIndicator()
    {
        Vector3 dirVector = (enemyWaveManager.WaveSpawnPosition - mainCamera.transform.position);
        Vector3 dirToSpawnPos = dirVector.normalized;
        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToSpawnPos * 300f;
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetRotationAngle(dirToSpawnPos));

        float distanceToSpawnPos = dirVector.sqrMagnitude;
        float orthoSize = mainCamera.orthographicSize;
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToSpawnPos > (orthoSize * orthoSize));
    }

    private void LookForClosestEnemy()
    {
        float maxRadius = 999f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mainCamera.transform.position, maxRadius);
        Enemy targetEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy)
            {
                if (targetEnemy)
                {
                    float a = (targetEnemy.transform.position - mainCamera.transform.position).sqrMagnitude;
                    float b = (enemy.transform.position - mainCamera.transform.position).sqrMagnitude;
                    if (Mathf.Abs(b) < Mathf.Abs(a)) targetEnemy = enemy;
                }
                else targetEnemy = enemy;
            }
        }

        HandleClosestEnemyIndicator(targetEnemy);
    }

    private void HandleClosestEnemyIndicator(Enemy targetEnemy)
    {
        if (targetEnemy)
        {
            Vector3 dirVector = (targetEnemy.transform.position - mainCamera.transform.position);
            Vector3 dirToClosestEnemy = dirVector.normalized;
            closestEnemyIndicator.anchoredPosition = dirToClosestEnemy * 250f;
            closestEnemyIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetRotationAngle(dirToClosestEnemy));

            float distToClosestEnemy = dirVector.sqrMagnitude;
            float orthoSize = mainCamera.orthographicSize;
            closestEnemyIndicator.gameObject.SetActive(distToClosestEnemy > (orthoSize * orthoSize * 3));
        }
        else closestEnemyIndicator.gameObject.SetActive(false);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.text = text;
    }

    private void SetWaveMessageText(string text)
    {
        waveMessageText.text = text;
    }

    private void EnemyWaveManagerOnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.WaveNumber);
    }
}
