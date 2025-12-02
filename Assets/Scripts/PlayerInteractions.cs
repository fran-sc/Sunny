using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

/*
PlayerInteractions
Responsabilidad:

Gestiona las interacciones del jugador con elementos de peligro (trampas y enemigos) detectando colisiones.
Implementa la secuencia de muerte y reaparición del jugador mediante una corrutina.
Coordina efectos visuales (animación de muerte), auditivos (sonido de muerte) y de cámara durante el proceso.

Estructuras de datos internas:

dieFX: clip de audio que se reproduce cuando el jugador muere.
followCamera: referencia a la cámara de Cinemachine que sigue al jugador, se desactiva temporalmente al morir.
anim: controlador de animaciones del jugador para reproducir muerte y reaparición.
initialPosition: posición de spawn donde el jugador reaparece tras morir.
rb: referencia al Rigidbody2D para controlar velocidades y aplicar impulso de muerte.
col: referencia al Collider2D para desactivar colisiones durante la secuencia de muerte.
*/
public class PlayerInteractions : MonoBehaviour
{
    // dieFX: sonido reproducido al momento de la muerte del jugador.
    [SerializeField] AudioClip dieFX;
    // followCamera: cámara virtual de Cinemachine que debe desactivarse al morir para evitar seguimiento errático.
    [SerializeField] CinemachineCamera followCamera;
    // anim: referencia al Animator del jugador para disparar triggers de animación.
    Animator anim;
    // initialPosition: punto de respawn guardado al inicio del nivel.
    Vector3 initialPosition;
    // rb: componente Rigidbody2D usado para resetear velocidad y aplicar impulso visual de muerte.
    Rigidbody2D rb;
    // col: componente Collider2D desactivado temporalmente para evitar colisiones múltiples durante muerte.
    Collider2D col;

    /*
    Start
    Inicializa las referencias a los componentes del jugador y guarda la posición inicial de spawn.
    Se ejecuta una vez al cargar la escena.
    */
    void Start()
    {
        // Obtener componentes del GameObject del jugador para su uso posterior.
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Guardar la posición inicial como punto de respawn tras morir.
        initialPosition = transform.position;
    }

    /*
    OnCollisionEnter2D
    Callback de Unity invocado cuando el jugador colisiona con otro objeto con collider físico.
    Detecta colisiones con trampas o enemigos y activa la secuencia de muerte.
    */
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto que colisionó tiene tag "Trap" o "Enemy", iniciar secuencia de muerte.
        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(DieAndReborn());       
        }
    }

    /*
    DieAndReborn
    Corrutina que gestiona la secuencia completa de muerte y reaparición del jugador.
    Pasos:
    1. Detiene el movimiento del jugador y reproduce efectos visuales/auditivos.
    2. Desactiva la cámara de seguimiento y el collider para evitar interacciones.
    3. Aplica un impulso hacia arriba para efecto visual de "muerte".
    4. Espera 3 segundos antes de reposicionar al jugador en el punto de spawn.
    5. Reactiva animación de reaparición, collider y cámara.
    */
    IEnumerator DieAndReborn()
    {
        // Detener completamente el movimiento del jugador antes de iniciar la secuencia.
        rb.linearVelocity = Vector2.zero;

        // Reproducir efecto de sonido de muerte en la posición actual del jugador.
        AudioSource.PlayClipAtPoint(dieFX, transform.position);

        // Disparar animación de muerte en el Animator.
        anim.SetTrigger("die");

        // Desactivar la cámara para que no siga al jugador durante la animación de muerte.
        followCamera.enabled = false;

        // Aplicar impulso hacia arriba para efecto visual dramático de muerte.
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        // Desactivar collider para evitar colisiones adicionales durante la secuencia.
        col.enabled = false;

        // Esperar 3 segundos antes de reaparecer (tiempo para animación y transición).
        yield return new WaitForSeconds(3f);

        // Reposicionar al jugador en el punto de spawn original.
        transform.position = initialPosition;
        // Activar animación de reaparición.
        anim.SetTrigger("reborn");
        // Reactivar collider para que el jugador vuelva a interactuar con el mundo.
        col.enabled = true;
        // Reactivar cámara para que vuelva a seguir al jugador.
        followCamera.enabled = true;
    }
}
