using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public enum State
    {
        WaitingForNextSpawnWave,
        SpawningWave
    }

    public event System.EventHandler OnWaveNumberChanged;
    private State state;
    private float nextSpawnWaveTimer;
    public float NextSpawnWaveTimer { get { return nextSpawnWaveTimer; } }
    private float nextEnemySpawnTimer;
    private int remainingEnemyAmount;
    private int waveNumber;
    public int WaveNumber { get { return waveNumber; } }
    [SerializeField] private List<Transform> spawnPositionList;
    [SerializeField] private Transform spawnWaveHighlighter;
    public Vector3 WaveSpawnPosition { get { return spawnWaveHighlighter.position; } }
    public static EnemyWaveManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        nextSpawnWaveTimer = 3f;
        state = State.WaitingForNextSpawnWave;
        SetNextSpawnPosition();
    }


    void Update()
    {
        switch (state)
        {
            case State.WaitingForNextSpawnWave:
                nextSpawnWaveTimer -= Time.deltaTime;
                if (nextSpawnWaveTimer < 0)
                    SpawnWave();
                break;
            case State.SpawningWave:
                if (remainingEnemyAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0)
                    {
                        nextEnemySpawnTimer = Random.Range(0, 0.2f);

                        Enemy.CreateEnemy(spawnWaveHighlighter.position + UtilsClass.GetRandomPos() * Random.Range(0, 10f));
                        remainingEnemyAmount--;
                    }
                }
                else
                {
                    state = State.WaitingForNextSpawnWave;
                    SetNextSpawnPosition();
                    nextSpawnWaveTimer = 10f;
                }
                break;
        }
    }

    private void SpawnWave()
    {
        remainingEnemyAmount = 5 + 3 * waveNumber;
        waveNumber++;
        state = State.SpawningWave;
        OnWaveNumberChanged?.Invoke(this, System.EventArgs.Empty);
        SoundManager.Instance.PlayAudio(SoundManager.Sound.EnemyWaveStarting);
    }

    private void SetNextSpawnPosition()
    {
        spawnWaveHighlighter.position = spawnPositionList[Random.Range(0, spawnPositionList.Count)].position;
    }
}
