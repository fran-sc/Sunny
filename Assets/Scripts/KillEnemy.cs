using UnityEngine;

/*
KillEnemy
Responsabilidad:

Gestiona la eliminación de enemigos cuando el jugador entra en contacto con su zona de trigger (típicamente colocada sobre su cabeza).
Reproduce efectos visuales y auditivos antes de destruir al enemigo de la escena.

Estructuras de datos internas:

dieFX: clip de audio reproducido cuando el enemigo muere.
dieAnim: prefab de animación instanciado en la posición del enemigo al morir (efecto visual de explosión/desaparición).
*/
public class KillEnemy : MonoBehaviour
{
    // dieFX: sonido reproducido al eliminar al enemigo.
    [SerializeField] AudioClip dieFX;
    // dieAnim: GameObject con animación de muerte instanciado antes de destruir al enemigo.
    [SerializeField] GameObject dieAnim;
    
    /*
    OnTriggerEnter2D
    Callback de Unity invocado cuando otro collider entra en la zona de trigger de este GameObject.
    Si el collider pertenece al jugador, elimina al enemigo con efectos visuales y auditivos.
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró al trigger es el jugador.
        if (other.gameObject.tag == "Player")
        {
            // Reproducir sonido de muerte del enemigo en su posición actual.
            AudioSource.PlayClipAtPoint(dieFX, transform.position);
            // Instanciar prefab de animación de muerte en la posición del enemigo.
            Instantiate(dieAnim, transform.position, Quaternion.identity);
            // Destruir el GameObject del enemigo para eliminarlo de la escena.
            Destroy(gameObject);
        }
    }
}
