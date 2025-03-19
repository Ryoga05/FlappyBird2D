using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f; // Fuerza del salto
    private Rigidbody2D rb;
    private Animator animator;
    private GameManager gameManager;
    public AudioClip flapSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = gameManager.audioSource;
    }

    // Update is called once per frame
    void Update()
    {
        // Detectar cuando se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // Aplicar una fuerza hacia arriba
            rb.velocity = Vector2.up * jumpForce;
            audioSource.PlayOneShot(flapSound);
        }
        float angle = Mathf.Clamp(rb.velocity.y * 5f, -45f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDeath"))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            audioSource.PlayOneShot(hitSound);
            animator.SetTrigger("Die");
            Invoke("DestroyPlayer", 2);
            gameManager.GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerDeath"))
        {
            gameManager.AddScore();    
        }
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}