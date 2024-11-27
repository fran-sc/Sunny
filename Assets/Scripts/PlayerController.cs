using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    
    Rigidbody2D rb;
    Collider2D col;
    Animator anim;

    float moveX;
    bool jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Run();
        Flip();
        Jump();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");    
        if (!jump && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void Run()
    {
        // calculamos la velocidad en el eje X
        Vector2 vel = new Vector2(moveX * speed * Time.fixedDeltaTime, rb.linearVelocityY);
        rb.linearVelocity = vel;

        // establecemos la animación de correr
        anim.SetBool("isRunning", Math.Abs(rb.linearVelocity.x) > Mathf.Epsilon);
    }

    void Flip()
    {
        float vx = rb.linearVelocity.x;
        
        if (Math.Abs(vx) < Mathf.Epsilon) return;

        transform.localScale = new Vector2(Mathf.Sign(vx), 1);
    }

    void Jump()
    {
        if (!jump) return;

        jump = false;
        
        // detectamos si estamos sobre una de las capas de colisión
        var mask = LayerMask.GetMask("Terrain", "Platforms");
        if (!col.IsTouchingLayers(mask)) return;
        
        rb.linearVelocityY += jumpSpeed;

        // establecemos la animación de saltar
        anim.SetTrigger("isJumping");
    }
}
