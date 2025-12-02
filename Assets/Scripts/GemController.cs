using UnityEngine;

/*
GemController
Responsabilidad:

Gestiona la recolección de gemas por parte del jugador mediante detección de triggers.
Reproduce efectos auditivos y visuales antes de eliminar la gema de la escena.
Cada gema destruida afecta al contador global de items en el GameController.

Estructuras de datos internas:

gemCollect: clip de audio reproducido cuando el jugador recoge la gema.
animCollect: referencia al Animator del efecto visual de recolección instanciado al recoger la gema.
*/
public class GemController : MonoBehaviour
{
    // gemCollect: sonido reproducido al recolectar la gema.
    [SerializeField] AudioClip gemCollect;
    // animCollect: Animator del efecto de partículas o animación de recolección.
    [SerializeField] Animator animCollect;

    /*
    OnTriggerEnter2D
    Callback de Unity invocado cuando otro collider entra en el trigger de la gema.
    Si el collider pertenece al jugador, activa la secuencia de recolección.
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el GameObject que tocó el trigger tiene el tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reproducir sonido de recolección en la posición de la gema.
            AudioSource.PlayClipAtPoint(gemCollect, transform.position);

            // Instanciar el Animator del efecto de recolección en la posición de la gema.
            Instantiate(animCollect, transform.position, Quaternion.identity);
            
            // Destruir el GameObject de la gema para eliminarla de la escena y actualizar el contador.
            Destroy(gameObject);
        }
    }
}
