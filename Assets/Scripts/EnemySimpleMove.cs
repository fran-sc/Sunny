using UnityEngine;

/*
EnemySimpleMove
Responsabilidad:

Implementa un patrón de patrulla simple para enemigos que se mueven horizontalmente de forma lineal.
El enemigo recorre una distancia configurada en una dirección y luego invierte su rumbo, repitiendo el ciclo.
Voltea el sprite automáticamente al cambiar de dirección para reflejar visualmente su movimiento.

Estructuras de datos internas:

speed: velocidad de desplazamiento horizontal del enemigo (unidades por segundo).
maxDistance: distancia total que recorre en una dirección antes de invertir su rumbo.
moveRight: indica la dirección inicial del patrullaje (true = derecha, false = izquierda).
initialPosition: coordenada X de la posición de inicio para futuras referencias si fuese necesario.
direction: multiplicador de dirección actual (+1 derecha, -1 izquierda).
distanceTravelled: acumulador de la distancia recorrida en la dirección actual.
sr: referencia al SpriteRenderer para voltear el sprite con flipX.
*/
public class EnemySimpleMove : MonoBehaviour
{
    [Header("Settings")]
    // speed: velocidad de movimiento del enemigo.
    [SerializeField] float speed;
    // maxDistance: distancia máxima que recorre antes de cambiar de dirección.
    [SerializeField] float maxDistance;
    // moveRight: dirección inicial del movimiento (true para derecha, false para izquierda).
    [SerializeField] bool moveRight;

    // initialPosition: posición X inicial del enemigo guardada para referencia.
    float initialPosition;
    // direction: multiplicador de dirección actual (1 o -1).
    float direction;
    // distanceTravelled: contador de la distancia recorrida en la dirección actual.
    float distanceTravelled;
    // sr: referencia al SpriteRenderer para voltear visualmente al enemigo.
    SpriteRenderer sr;

    /*
    Start
    Inicializa las referencias y configura la dirección inicial del patrullaje.
    Guarda la posición de partida y establece el multiplicador de dirección según moveRight.
    */
    void Start()
    {
        // Obtener el SpriteRenderer para poder voltear el sprite cuando cambie de dirección.
        sr = GetComponent<SpriteRenderer>();

        // Guardar la posición X inicial del enemigo como referencia.
        initialPosition = transform.position.x;

        // Establecer dirección inicial: 1 para derecha, -1 para izquierda.
        direction = moveRight ? 1 : -1;
    }

    /*
    Update
    Mueve al enemigo horizontalmente cada frame y acumula la distancia recorrida.
    Cuando alcanza la distancia máxima, invierte la dirección y voltea el sprite.
    */
    void Update()
    {
        // Calcular desplazamiento del frame actual multiplicando velocidad por deltaTime y dirección.
        float movement = speed * Time.deltaTime * direction;   
        // Trasladar al enemigo en el eje X usando el desplazamiento calculado.
        transform.Translate(new Vector2(movement, 0));    

        // Acumular la distancia recorrida en valor absoluto para detectar cuándo invertir.
        distanceTravelled += Mathf.Abs(movement);

        // Si alcanzó la distancia máxima configurada, invertir dirección y voltear sprite.
        if (distanceTravelled >= maxDistance)
        {
            // Invertir el multiplicador de dirección.
            direction *= -1;
            // Resetear el contador de distancia para empezar el nuevo tramo.
            distanceTravelled = 0;
            // Voltear el sprite horizontalmente.
            sr.flipX = !sr.flipX;
        } 
    }
}
