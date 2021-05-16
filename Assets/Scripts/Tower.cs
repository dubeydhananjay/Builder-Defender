using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float lookForTargetTimer;
    private float lookForTargetTimermax = 0.2f;
    private Enemy targetEnemy;
    private Vector3 projectileSpawnPosition;
    [SerializeField] private float shootTimerMax;
    private float shootTimer;
    private void Awake()
    {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }


    private void Update()
    {
        HandleLookForTarget();
        HandleShooting();
    }

    private void HandleLookForTarget()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0)
        {
            lookForTargetTimer += lookForTargetTimermax;
            LookForTarget();
        }
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy)
                ArrowProjectile.CreateArrowProjectile(projectileSpawnPosition, targetEnemy);
        }
    }
    private void LookForTarget()
    {
        float maxRadius = 20f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxRadius);
        
        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            
            if (enemy)
            {
                if (targetEnemy)
                {
                    float a = (targetEnemy.transform.position - transform.position).sqrMagnitude;
                    float b = (enemy.transform.position - transform.position).sqrMagnitude;
                    if (Mathf.Abs(b) < Mathf.Abs(a)) targetEnemy = enemy;
                }
                else targetEnemy = enemy;
            }
        }


    }
}
