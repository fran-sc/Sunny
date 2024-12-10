using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] AudioClip killSound;
    [SerializeField] GameObject killAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lanzamos el sonido de recogida de gemas
            AudioSource.PlayClipAtPoint(killSound, transform.position);

            // Lanzamos la animación de recogida de gemas
            Instantiate(killAnim, transform.position, Quaternion.identity);

            // Destruimos la gema
            Destroy(gameObject);
        }
    }
}
