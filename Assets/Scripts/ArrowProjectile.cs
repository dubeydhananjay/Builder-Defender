using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{

    public static ArrowProjectile CreateArrowProjectile(Vector3 position, Enemy enemy)
    {
        ArrowProjectile pfArrowProjectile = GameAssets.Instance.pfArrowProjectile;
        ArrowProjectile arrowProjectile = Instantiate(pfArrowProjectile, position, Quaternion.identity);
        arrowProjectile.SetTargetEnemy(enemy);
        return arrowProjectile;
    }
    private Enemy targetEnemy;
    private Rigidbody2D arrowRigidbody2D;
    private float timeToDie = 2f;
    private Vector3 lastMoveDir;
    private void Awake()
    {
        arrowRigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDir;

        if (targetEnemy)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else moveDir = lastMoveDir;


        float moveSpeed = 20f;
        arrowRigidbody2D.velocity = moveDir * moveSpeed;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetRotationAngle(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0)
            Destroy(gameObject);
    }

    private void SetTargetEnemy(Enemy enemy)
    {
        targetEnemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
