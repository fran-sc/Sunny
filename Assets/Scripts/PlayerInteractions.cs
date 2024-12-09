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
        rb.linearVelocity = Vector2.zero;
        
        anim.SetTrigger("die");
        
        followCamera.Follow = null;

        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        col.enabled = false;

        yield return new WaitForSeconds(3f);
        
        anim.SetTrigger("reborn");
        col.enabled = true;
        transform.position = initialPosition;
        followCamera.Follow = transform;

        /*
        followCamera.enabled = false;
        yield return new WaitForSeconds(0.25f);
        followCamera.enabled = true;
        */
    }
}
