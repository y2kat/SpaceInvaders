using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; 
    public float horizontalDistance = 1f; 
    public float verticalDistance = 1f;
    public bool moveRight = true; 
    public float health = 100f;

    private float leftEdge; 
    private float rightEdge;
    private Vector3 startPosition; 

    void Start()
    {
        // Obtener los límites de la pantalla en coordenadas del mundo
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        startPosition = transform.position;
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
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
