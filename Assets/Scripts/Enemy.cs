using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static Enemy CreateEnemy(Vector3 position)
    {
        Enemy pfEnemy = GameAssets.Instance.pfEnemy;
        Enemy enemy = Instantiate(pfEnemy, position, Quaternion.identity);
        return enemy;

    }
    private Transform targetTransform;
    private Rigidbody2D enemyRigidbody2D;
    private float lookForTargetTimer;
    private float lookForTargetTimermax = 0.2f;
    private HealthSystem healthSystem;



    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();

        lookForTargetTimer = Random.Range(0, lookForTargetTimermax);
    }
    private void Start()
    {
        healthSystem.OnDamaged += HealthSystemOnDamaged;
        healthSystem.OnDied += HealthSystemOnDied;
        
        SetTargetTransform(BuildingManager.Instance.GetHQBuilding);
    }

    private void HealthSystemOnDamaged(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.ShakeCamera(2f,0.1f);
        ChromaticAberrationEffect.Instance.SetVolumeWeight(0.5f);
        SoundManager.Instance.PlayAudio(SoundManager.Sound.EnemyHit);
        
    }

    private void HealthSystemOnDied(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.ShakeCamera(3f,0.15f);
        ChromaticAberrationEffect.Instance.SetVolumeWeight(0.5f);
        Instantiate(GameAssets.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
        
        SoundManager.Instance.PlayAudio(SoundManager.Sound.EnemyDie);
        Destroy(gameObject);

    }
    private void Update()
    {
        HandleMovement();
        HandleLookForTarget();
    }

    private void HandleMovement()
    {
        if (targetTransform)
        {
            Vector3 movePos = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;
            enemyRigidbody2D.velocity = movePos * moveSpeed;
        }
    }

    private void HandleLookForTarget()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimermax;
            LookForTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Building building = other.gameObject.GetComponent<Building>();
        if (building)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            this.healthSystem.Damage(999);
        }
    }

    private void LookForTarget()
    {
        float maxRadius = 10f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxRadius);

        foreach (Collider2D collider in colliders)
        {
            Building building = collider.GetComponent<Building>();
            if (building)
            {
                Transform buildingTransform = building.transform;
                if (targetTransform)
                {
                    float a = (targetTransform.position - transform.position).sqrMagnitude;
                    float b = (buildingTransform.position - transform.position).sqrMagnitude;
                    if (Mathf.Abs(b) < Mathf.Abs(a)) SetTargetTransform(building);
                }
                else SetTargetTransform(building);
            }
        }

        if (!targetTransform)
            SetTargetTransform(BuildingManager.Instance.GetHQBuilding);

    }

    private void SetTargetTransform(Building building)
    {
        if (building)
            targetTransform = building.transform;
    }


}
