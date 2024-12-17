using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] AudioClip dieFX;
    [SerializeField] CinemachineCamera followCamera;
    Animator anim;
    Vector3 initialPosition;
    Rigidbody2D rb;
    Collider2D col;

    void Start()
    {
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        initialPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(DieAndReborn());       
        }
    }

    IEnumerator DieAndReborn()
    {
        // eliminar la velocidad del jugador
        rb.linearVelocity = Vector2.zero;

        // reproducir el sonido de muerte
        AudioSource.PlayClipAtPoint(dieFX, transform.position);

        // activar la animación de muerte del jugador
        anim.SetTrigger("die");

        // desactivar la cámara
        followCamera.enabled = false;

        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        col.enabled = false;

        yield return new WaitForSeconds(3f);

        // reiniciar la posición del jugador
        transform.position = initialPosition;
        anim.SetTrigger("reborn");
        col.enabled = true;
        followCamera.enabled = true;
    }
}
