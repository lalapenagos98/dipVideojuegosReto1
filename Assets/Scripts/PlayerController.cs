using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public AudioSource run;
    public AudioSource jump;
    public AudioSource damageSound;
    public AudioSource deadSound;

    private Rigidbody2D rb;
    public Animator animator;
    [SerializeField]
    private bool isGrounded;
    private bool isCrouching;
    private Vector3 puntoAparicion = new Vector3(-5.4f, -0.7f, 0);
    public bool damage_;
    public int empuje;
    public float empujeMagnitude = 3f;
    private bool alreadyDamaged = false;
    //public float HP_Min;
    //public float HP_Max;
    public Slider barra;
    public int dead;
    public GameObject panelMuerte;
    public Vector3 initialPosition;


    public void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    public void Damage()
    {
        if (damage_ && !alreadyDamaged)
        {
            //        Vector2 empujeDirection = (empuje > 0) ? Vector2.right : Vector2.left;
            //        rb.AddForce(empujeDirection * empujeMagnitude, ForceMode2D.Impulse);
            alreadyDamaged = true; // Marcar que ya se aplicó el empuje
        }
    }

    public void Finish_Damage()
    {
        animator.SetBool("damage0", false);
        
    }
  
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "FallDetector")
        {
            gameObject.transform.position = initialPosition; // El personaje aparece nuevamente al inicio de la escena
            float restaSize = 0.05f;
            barra.value = Mathf.Clamp(barra.value - restaSize, 0f, 1f);
        }

        if (collider.tag == "bateria")
        {
            Debug.Log("Se sumo la bateria");
            float sumaSize = 0.1f;
            barra.value = Mathf.Clamp(barra.value + sumaSize, 0f, 1f);

        }
        if (collider.CompareTag("enemy1"))
        {
            Debug.Log(barra.value); // Aquí accedemos a la propiedad "value" del Scrollbar
            float restaSize = 0.1f;
            barra.value = Mathf.Clamp(barra.value - restaSize, 0f, 1f);
            damageSound.Play();
        }
        if (collider.CompareTag("enemy2"))
        {
            Debug.Log(barra.value); // Aquí accedemos a la propiedad "value" del Scrollbar
            float restaSize = 0.2f;
            barra.value = Mathf.Clamp(barra.value - restaSize, 0f, 1f);
            damageSound.Play();
        }
        if (collider.CompareTag("enemy3"))
        {
            Debug.Log(barra.value); // Aquí accedemos a la propiedad "value" del Scrollbar
            float restaSize = 0.3f;
            barra.value = Mathf.Clamp(barra.value - restaSize, 0f, 1f);
            damageSound.Play();
        }
        if (collider.CompareTag("trampas"))
        {
            Debug.Log(barra.value); // Aquí accedemos a la propiedad "value" del Scrollbar
            float restaSize = 0.1f;
            barra.value = Mathf.Clamp(barra.value - restaSize, 0f, 1f);
            damageSound.Play();
        }
        if (collider.CompareTag("scena"))
        {
            // Cargar la nueva escena por su nombre
            SceneManager.LoadScene("Nivel2");
        }
        if (collider.CompareTag("scena2"))
        {
            // Cargar la nueva escena por su nombre
            SceneManager.LoadScene("Nivel3");
        }
        if (collider.CompareTag("scena3"))
        {
            // Cargar la nueva escena por su nombre
            SceneManager.LoadScene("menu");
        }
    }

    private void Update()
    {
        //Vida();
       
            //Damage();

            // Comprobar si el personaje está en el suelo
            if (!damage_)
            {
                // Movimiento horizontal
                float horizontalInput = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
                //Debug.Log(horizontalInput);
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
                animator.SetBool("IsJumping", !isGrounded);

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
        const float tolerance = 0.001f;
        if (barra.value <= tolerance)
        {
            deadSound.Play();
            animator.SetTrigger("dead");
            Time.timeScale = 0f;
            panelMuerte.SetActive(true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
    }
    private void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.8f,groundLayer);
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