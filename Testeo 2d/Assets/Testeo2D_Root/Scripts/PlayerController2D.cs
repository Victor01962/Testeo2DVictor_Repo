using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements; //Libreria para que funcione el New Input System


public class PlayerController2D : MonoBehaviour
{

    //Referencias generales
    [SerializeField]Rigidbody2D playerRb; // Ref al rigidbody del player
    [SerializeField] PlayerInput playerInput; //Ref al gestor del Input del jugador
    [SerializeField] Animator playerAnim; //Ref al animator para gestionar las transiciones de animación 

    [Header("Movement Parameters")]
    private Vector2 moveInput; //Almacén del input del player
    public float speed;
    [SerializeField] bool isFacingRight;
  
    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    //Variables para el Groundcheck
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;




    // Start is called before the first frame update
    void Start()
    {
        //Autoreferenciar componentes: nombre de variable = GetComponent()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        isFacingRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimations();
        Groundcheck();

      //Flip
      if (moveInput.x > 0)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        if (moveInput.x < 0)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }



    }
    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
    }

    void Flip()
    {
        Vector3 currentScale = transform.transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight; //nombre de bool = !nombre de bool (cambio al estado contrario)


    }


    void Groundcheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

   void HandleAnimations()
    {
        //Conector de valores generales con parametros de cambio de animación
        playerAnim.SetBool("IsJumping", !isGrounded);
        if (moveInput.x > 0 || moveInput.x < 0) playerAnim.SetBool("IsRunning", true);
        else playerAnim.SetBool("IsRunning", false);
    }

  
    #region Input Events
    //Para crear un evento:
    //se define public sin tipo de daTO (VOID) Y CON UNA REFERENCIA AL input (Callback.Context)
    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
           
        }
        
    }
    
    
    
    #endregion






}
