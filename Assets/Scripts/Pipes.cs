using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 2f; // Velocidad del movimiento
    private bool isGameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            // Mover el GameObject hacia la izquierda solo si el juego no ha terminado
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto con el que chocó es PipesDeath
        if (collision.gameObject.CompareTag("PipesDeath"))
        {
            Pipes pipeScript = gameObject.GetComponent<Pipes>();
            FindObjectOfType<GameManager>().UnregisterPipe(pipeScript);

            // Destruir este GameObject
            Destroy(gameObject);
            
        }
    }

    public void StopPipesMovement()
    {
        isGameOver = true;
    }

}