using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float pushForce;
    [SerializeField] private int damage;
    [SerializeField] private float timeToDespawn;

    private float despawnTimer;
    public bool isEnemyBullet;

    void Start()
    {
        despawnTimer = 0;
    }

    void Update()
    {
        Vector2 direction = isEnemyBullet ? Vector2.down : Vector2.up;
        transform.Translate(direction * speed * Time.deltaTime);

        despawnTimer += Time.deltaTime;
        if (despawnTimer >= timeToDespawn) 
        {
            transform.parent.GetComponent<PoolScript>().DespawnObject(gameObject);
            despawnTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isEnemyBullet)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            transform.parent.GetComponent<PoolScript>().DespawnObject(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && isEnemyBullet)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            transform.parent.GetComponent<PoolScript>().DespawnObject(gameObject);
        }
    }
}
