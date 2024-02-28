using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo
    public float horizontalDistance = 1f; // Distancia horizontal que recorre antes de cambiar de direcci�n
    public float verticalDistance = 1f; // Distancia vertical que desciende cuando cambia de direcci�n
    public bool moveRight = true; // Direcci�n inicial del movimiento

    private float leftEdge; // Coordenada x del borde izquierdo de la pantalla
    private float rightEdge; // Coordenada x del borde derecho de la pantalla
    private Vector3 startPosition; // Posici�n inicial del enemigo

    void Start()
    {
        // Obtener los l�mites de la pantalla en coordenadas del mundo
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        startPosition = transform.position; // Guardar la posici�n inicial
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float horizontalMovement = moveRight ? speed : -speed; // Determinar direcci�n del movimiento
        transform.Translate(Vector3.right * horizontalMovement * Time.fixedDeltaTime); // Mover horizontalmente

        // Si el enemigo alcanza el borde izquierdo o derecho de la pantalla, cambiar de direcci�n y descender
        if (transform.position.x <= leftEdge || transform.position.x >= rightEdge)
        {
            moveRight = !moveRight; // Cambiar de direcci�n
            transform.Translate(Vector3.down * verticalDistance); // Descender
        }
    }
}
