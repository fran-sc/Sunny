using System;
using UnityEditor.Tilemaps;
using UnityEngine;

/*
PlayerController
Responsabilidad:

Controla el movimiento horizontal, salto y volteo del jugador respondiendo a entradas del teclado/gamepad.
Sincroniza las animaciones de correr y saltar con el estado físico del personaje.
Utiliza FixedUpdate para el movimiento físico y Update para capturar entradas.

Estructuras de datos internas:

speed: velocidad de desplazamiento horizontal (unidades/segundo).
jumpSpeed: velocidad vertical aplicada al saltar (impulso).
rb: referencia al Rigidbody2D para modificar velocidades lineales.
col: referencia al Collider2D para detectar si el jugador toca el suelo.
anim: referencia al Animator para sincronizar estados de animación.
moveX: entrada horizontal del jugador (-1, 0, 1) leída desde el Input Manager.
jump: bandera que indica si el jugador presionó el botón de salto en este frame.
*/
public class PlayerController : MonoBehaviour
{
    // speed: velocidad de movimiento horizontal del personaje.
    // jumpSpeed: impulso vertical aplicado cuando el personaje salta.
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    
    // rb: acceso directo al Rigidbody2D para manipular velocidades sin buscar el componente cada frame.
    Rigidbody2D rb;
    // col: referencia al Collider2D para verificar contacto con capas de suelo.
    Collider2D col;
    // anim: controlador de animaciones del personaje para reflejar estado visual.
    Animator anim;

    // moveX: valor del eje Horizontal capturado en Update (-1 para izquierda, 1 para derecha, 0 sin movimiento).
    float moveX;
    // jump: indica si el jugador solicitó un salto este frame; se procesa en FixedUpdate y se resetea.
    bool jump;

    /*
    Start
    Inicializa las referencias a componentes del personaje necesarios para controlar el movimiento y las animaciones.
    Se ejecuta una vez al instanciar el GameObject.
    */
    void Start()
    {
        // Obtener componentes del mismo GameObject para evitar búsquedas repetidas en cada frame.
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    /*
    FixedUpdate
    Ejecuta la lógica de movimiento físico a intervalos fijos (50Hz por defecto) para mantener consistencia en física.
    Llama a los métodos Run, Flip y Jump en orden para aplicar movimiento, volteo de sprite y salto.
    */
    void FixedUpdate()
    {
        Run();
        Flip();
        Jump();
    }

    /*
    Update
    Captura las entradas del jugador cada frame visual para responder rápidamente a los controles.
    Lee el eje horizontal y detecta la pulsación del botón de salto.
    */
    void Update()
    {
        // Leer entrada del eje horizontal (teclas A/D, flechas izquierda/derecha, joystick).
        moveX = Input.GetAxisRaw("Horizontal");    
        // Activar bandera de salto si el botón se presiona y no está ya activada.
        if (!jump && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    /*
    Run
    Aplica la velocidad horizontal basada en la entrada del jugador y actualiza la animación de correr.
    Conserva la velocidad vertical actual para no interferir con gravedad o saltos.
    */
    void Run()
    {
        // Calcular nueva velocidad horizontal multiplicando entrada por velocidad configurada y delta de tiempo fijo.
        Vector2 vel = new Vector2(moveX * speed * Time.fixedDeltaTime, rb.linearVelocityY);
        rb.linearVelocity = vel;

        // Activar animación de correr si hay movimiento horizontal significativo (evitar ruido numérico con Epsilon).
        anim.SetBool("isRunning", Math.Abs(rb.linearVelocity.x) > Mathf.Epsilon);
    }

    /*
    Flip
    Voltea el sprite del personaje horizontalmente según la dirección del movimiento.
    Solo se ejecuta si hay movimiento horizontal para evitar volteos innecesarios.
    */
    void Flip()
    {
        float vx = rb.linearVelocity.x;
        
        // No hacer nada si la velocidad horizontal es despreciable.
        if (Math.Abs(vx) < Mathf.Epsilon) return;

        // Cambiar la escala en X al signo de la velocidad: -1 mira izquierda, +1 mira derecha.
        transform.localScale = new Vector2(Mathf.Sign(vx), 1);
    }

    /*
    Jump
    Aplica impulso vertical si el jugador solicitó saltar y está tocando el suelo.
    Verifica contacto con capas "Terrain" y "Platforms" antes de permitir el salto.
    */
    void Jump()
    {
        // No procesar salto si no se activó la bandera.
        if (!jump) return;

        // Resetear bandera inmediatamente para evitar saltos múltiples con una sola pulsación.
        jump = false;
        
        // Crear máscara con las capas que representan superficies pisables.
        var mask = LayerMask.GetMask("Terrain", "Platforms");
        // Solo permitir salto si el collider del jugador toca alguna de esas capas.
        if (!col.IsTouchingLayers(mask)) return;
        
        // Incrementar velocidad vertical para simular impulso de salto.
        rb.linearVelocityY += jumpSpeed;

        // Activar trigger de animación de salto en el Animator.
        anim.SetTrigger("isJumping");
    }
}
