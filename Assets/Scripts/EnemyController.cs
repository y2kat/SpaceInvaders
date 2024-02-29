using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 20f;
    public float horizontalDistance = 1f;
    public float verticalDistance = 1f;
    public bool moveRight = true;
    public float health = 100f;

    private float leftEdge;
    private float rightEdge;
    private float bottomEdge;
    private Vector3 startPosition;

    private PoolScript bulletPool;
    private GameObject player;

    private static int shootCounter = 0;
    public int points = 20;

    public int columns;
    public float waveMultiplier;

    public PanelManager panelManager;


    void Start()
    {
        // Obtener los límites de la pantalla en coordenadas del mundo
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        startPosition = transform.position;

        bulletPool = GameObject.Find("EnemyBulletPool").GetComponent<PoolScript>();
        player = GameObject.FindWithTag("Player");

        panelManager = FindObjectOfType<PanelManager>();
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float horizontalMovement = moveRight ? speed : -speed;
        transform.Translate(Vector3.right * horizontalMovement * Time.fixedDeltaTime); // Mover horizontalmente

        // Si el enemigo alcanza el borde izquierdo o derecho de la pantalla, cambia de dirección y desciende
        if (transform.position.x <= leftEdge || transform.position.x >= rightEdge)
        {
            moveRight = !moveRight; // Cambia de dirección
            transform.Translate(Vector3.down * verticalDistance); // Desciende
        }

        Shoot();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddScore(points);
            }
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f)
        {
            // Solo dispara si este enemigo es el "elegido" para disparar y se cumple la condición aleatoria
            if (shootCounter % columns == 0 && Random.value < 0.08f * waveMultiplier) // Asume que 'columns' es el número de enemigos en una fila
            {
                GameObject bullet = bulletPool.RequestObject();
                bullet.GetComponent<Bullet>().isEnemyBullet = true;
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
            }

            shootCounter++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GameOver();
                panelManager.EnablePanel(3);
            }
        }
    }
}
