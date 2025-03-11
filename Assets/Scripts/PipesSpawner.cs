using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesSpawner : MonoBehaviour
{
    public GameObject pipesPrefab; // Prefab de Pipes a instanciar
    public float spawnRate = 2f;   // Intervalo de tiempo entre cada generación
    public float spawnHeightRange = 2f; // Rango de altura para variar la posición vertical
    private bool isGameOver = false;
    private bool isGamePaused = true;

    // Start is called before the first frame update
    void Start()
    {
        // Comienza a invocar el método SpawnPipes repetidamente
        InvokeRepeating("SpawnPipes", 0f, spawnRate);
    }

    void Update()
    {
        if (isGameOver)
        {
            // Detener la generación de pipes
            CancelInvoke("SpawnPipes");
        }
    }

    void SpawnPipes()
    {
        if (!isGameOver && !isGamePaused)
        {
            // Generar una posición aleatoria en el eje Y dentro del rango especificado
            float randomY = Random.Range(-spawnHeightRange, spawnHeightRange);

            // Crear los Pipes en la posición actual del Spawner pero con altura modificada
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomY, 0);

            // Instanciar el prefab
            GameObject newPipe = Instantiate(pipesPrefab, spawnPosition, Quaternion.identity);

            Pipes pipeScript = newPipe.GetComponent<Pipes>();
            FindObjectOfType<GameManager>().RegisterPipe(pipeScript);
        }
    }

    public void StopPipesGeneration()
    {
        isGameOver = true;
    }

    public void PausePipesGeneration()
    {
        isGamePaused = true;
    }

    public void ResumePipesGeneration()
    {
        isGamePaused = false;
    }
}
