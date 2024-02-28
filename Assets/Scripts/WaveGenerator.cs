using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array de prefabs de enemigos
    public int rows = 5; // N�mero de filas de enemigos
    public int columns = 11; // N�mero de columnas de enemigos
    public float horizontalSpacing = 1.5f; // Espaciado horizontal entre enemigos
    public float verticalSpacing = 1.5f; // Espaciado vertical entre enemigos
    public Transform waveStartTransform; // Transform para la posici�n inicial de la oleada

    void Start()
    {
        GenerateWave();
    }

    void GenerateWave()
    {
        for (int i = 0; i < rows; i++)
        {
            // Seleccionar un prefab de enemigo basado en el �ndice de la fila
            GameObject enemyPrefab = enemyPrefabs[i % enemyPrefabs.Length];

            for (int j = 0; j < columns; j++)
            {
                // Calcular la posici�n del enemigo
                Vector3 position = new Vector3(j * horizontalSpacing, -i * verticalSpacing, 0) + waveStartTransform.position;

                // Instanciar el enemigo
                Instantiate(enemyPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
