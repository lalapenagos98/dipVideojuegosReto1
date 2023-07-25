using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public AudioSource run;
    public AudioSource jump;

    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]
    private bool isGrounded;
    private bool isCrouching;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Comprobar si el personaje está en el suelo
        

        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        Debug.Log(horizontalInput);
        if (horizontalInput < 0f) 
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalInput > 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        // Animación de correr
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Saltar
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Animación de salto
        animator.SetBool("IsJumping",!isGrounded);

        // Agacharse
        if (Input.GetKey(KeyCode.S))
        {
            isCrouching = true;
            // Aquí puedes ajustar el tamaño del collider para que el personaje se agache
            // Ejemplo:
            // GetComponent<BoxCollider2D>().size = new Vector2(originalWidth, originalHeight * 0.5f);
        }
        else
        {
            isCrouching = false;
            // Aquí debes restaurar el tamaño original del collider cuando el personaje deja de agacharse
            // Ejemplo:
            // GetComponent<BoxCollider2D>().size = new Vector2(originalWidth, originalHeight);
        }

        // Animación de agacharse
        animator.SetBool("IsCrouching", isCrouching);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
    }
    private void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f,groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }
    private void FixedUpdate()
    {
        GroundCheck();

    }
    private void RunSound()
    {
        run.Play();
    }
    private void JumpSound()
    {
        jump.Play();
    }
}