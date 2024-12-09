using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] CinemachineCamera followCamera;

    Rigidbody2D rb;
    Animator anim;
    Collider2D col;
    Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        initialPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
            StartCoroutine(DieAndReborn());
        }
    }

    IEnumerator DieAndReborn()
    {
        // Eliminar la velocidad del jugador
        rb.linearVelocity = Vector2.zero;
        
        // Activar la animación de morir
        anim.SetTrigger("die");
        
        // Desactivar el seguimiento de cámara
        followCamera.Follow = null;

        // Aplicar una fuerza para la simulación de la muerte
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        col.enabled = false;

        // Esperar unos segundos
        yield return new WaitForSeconds(3f);
        
        // Reaparecer al jugador en la posición inicial y activar el seguimiento de cámara
        anim.SetTrigger("reborn");
        col.enabled = true;
        transform.position = initialPosition;
        followCamera.Follow = transform;
    }
}
