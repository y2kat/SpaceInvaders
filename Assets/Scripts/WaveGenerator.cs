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

    private float waveMultiplier = 1f;

    public PanelManager panelManager;

    void Start()
    {
        GenerateWave();
    }

    void Update()
    {
        if (transform.childCount == 3)
        {
            waveMultiplier += 0.1f;
            GenerateWave();
        }
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
                GameObject enemyInstance = Instantiate(enemyPrefab, position, Quaternion.identity, transform);
                enemyInstance.GetComponent<EnemyController>().columns = columns;
                enemyInstance.GetComponent<EnemyController>().waveMultiplier = waveMultiplier;
                enemyInstance.GetComponent<EnemyController>().speed *= waveMultiplier;
                enemyInstance.GetComponent<EnemyController>().health *= waveMultiplier;

                if (i == 0)
                {
                    enemyInstance.GetComponent<EnemyController>().points = 50;
                }
                else if (i == 1)
                {
                    enemyInstance.GetComponent<EnemyController>().points = 30;
                }
                else
                {
                    enemyInstance.GetComponent<EnemyController>().points = 20;
                }
            }
        }
    }
}
