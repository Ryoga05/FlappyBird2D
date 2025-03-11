using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int points;
    private int highscore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject gameOverMenu;
    public GameObject startMenu;
    public GameObject pipesSpawner;
    public GameObject player;
    private PipesSpawner pipesSpawnerScript; // Referencia al script de PipesSpawner
    private List<Pipes> activePipes = new List<Pipes>(); // Lista de pipes activas
    private bool gameStarted = false;
    private bool gameOver = false;
    public AudioClip pointSound;
    public AudioSource audioSource;

    
    // Start is called before the first frame update
    void Start()
    {
        pipesSpawnerScript = pipesSpawner.GetComponent<PipesSpawner>();
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.X))
        {
            gameStarted = true;
            startMenu.SetActive(false);
            player.SetActive(true);
            pipesSpawnerScript.ResumePipesGeneration();
            UpdateScoreText();
            highscoreText.text = "";
        }

        if (gameOver && Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            if (gameOver)
            {
                scoreText.text = "";
            }
            else
            {
                scoreText.text = "Score: " + points;
            }
        }
    }

    public void AddScore()
    {
        points++;
        UpdateScoreText();
        audioSource.PlayOneShot(pointSound);
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverMenu.SetActive(true);
        UpdateScoreText();
        pipesSpawnerScript.StopPipesGeneration(); // Detener la generación de pipes

        foreach (Pipes pipe in activePipes)
        {
            pipe.StopPipesMovement();
        }
        if (points > highscore){
            highscore = points;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
        }
        highscoreText.text = "Highscore: " + highscore;
    }

    public void RegisterPipe(Pipes pipe)
    {
        activePipes.Add(pipe);
    }

    // Método para eliminar una pipe activa de la lista
    public void UnregisterPipe(Pipes pipe)
    {
        activePipes.Remove(pipe);
    }
}
