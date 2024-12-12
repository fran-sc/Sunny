using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] AudioClip gemCollect;
    [SerializeField] Animator animCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lanzamos el sonido de recogida de las gemas
            AudioSource.PlayClipAtPoint(gemCollect, transform.position);

            // Lanzamos la animación de recogida de las gemas
            Instantiate(animCollect, transform.position, Quaternion.identity);
            
            // Destruir la gema
            Destroy(gameObject);
        }
    }
}
