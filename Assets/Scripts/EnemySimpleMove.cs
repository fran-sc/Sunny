using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemySimpleMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float maxDistance;
    [SerializeField] bool moveRight;

    float initialPosition;
    float direction;    
    float distanceTraveled;
    SpriteRenderer sr;

    void Start()
    {
        // posición horizontal inicial del enemigo
        initialPosition = transform.position.x;

        // dirección de movimiento inicial
        direction = moveRight ? 1 : -1;

        // referencia al componente SpriteRenderer
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // nuevo incremento de posición
        float movement = speed * Time.deltaTime * direction;  
        
        // movemos el enemigo
        transform.Translate(new Vector2(movement, 0));

        // actualizamos la distancia recorrida
        distanceTraveled += Mathf.Abs(movement);

        if (distanceTraveled >= maxDistance)
        {
            distanceTraveled = 0;
            direction *= -1;
            sr.flipX = !sr.flipX;
        }
    }
}
