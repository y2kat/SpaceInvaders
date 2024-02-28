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

    void Start()
    {
        despawnTimer = 0;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        despawnTimer += Time.deltaTime;
        if (despawnTimer >= timeToDespawn) 
        {
            transform.parent.GetComponent<PoolScript>().DespawnObject(gameObject);
            despawnTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Mensaje de depuración para verificar qué objeto colisionó con la bala

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy damaged by: " + damage); // Mensaje de depuración para verificar que el enemigo recibió daño
            }
            else
            {
                Debug.LogWarning("Enemy controller not found!"); // Mensaje de advertencia si no se encuentra el controlador del enemigo
            }

            transform.parent.GetComponent<PoolScript>().DespawnObject(gameObject);
            Debug.Log("Bullet despawned."); // Mensaje de depuración para verificar que la bala fue despawned
        }
    }
}
