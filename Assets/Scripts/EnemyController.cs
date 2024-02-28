using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo
    public float horizontalDistance = 1f; // Distancia horizontal que recorre antes de cambiar de dirección
    public float verticalDistance = 1f; // Distancia vertical que desciende cuando cambia de dirección
    public bool moveRight = true; // Dirección inicial del movimiento

    private float leftEdge; // Coordenada x del borde izquierdo de la pantalla
    private float rightEdge; // Coordenada x del borde derecho de la pantalla
    private Vector3 startPosition; // Posición inicial del enemigo

    void Start()
    {
        // Obtener los límites de la pantalla en coordenadas del mundo
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        startPosition = transform.position; // Guardar la posición inicial
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float horizontalMovement = moveRight ? speed : -speed; // Determinar dirección del movimiento
        transform.Translate(Vector3.right * horizontalMovement * Time.fixedDeltaTime); // Mover horizontalmente

        // Si el enemigo alcanza el borde izquierdo o derecho de la pantalla, cambiar de dirección y descender
        if (transform.position.x <= leftEdge || transform.position.x >= rightEdge)
        {
            moveRight = !moveRight; // Cambiar de dirección
            transform.Translate(Vector3.down * verticalDistance); // Descender
        }
    }
}
