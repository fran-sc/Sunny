using UnityEngine;

public class GemCollect : MonoBehaviour
{
    [SerializeField] AudioClip gemCollect;
    [SerializeField] GameObject collectAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lanzamos el sonido de recogida de gemas
            AudioSource.PlayClipAtPoint(gemCollect, transform.position);

            // Lanzamos la animación de recogida de gemas
            Instantiate(collectAnim, transform.position, Quaternion.identity);

            // Destruimos la gema
            Destroy(gameObject);
        }
    }
}
