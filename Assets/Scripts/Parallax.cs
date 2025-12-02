using UnityEngine;

/*
Parallax
Responsabilidad:

Gestiona el desplazamiento visual de las capas de fondo para simular profundidad cuando la cámara se mueve.
Sincroniza la posición del plano con la cámara en el eje X y aplica un offset de textura proporcional para lograr el efecto de paralaje.

Estructuras y datos clave:

parallax: factor escalar (float) que determina qué tanto se desplaza la textura respecto al movimiento de la cámara.
mat: referencia al material de la malla renderizada para modificar el offset UV en tiempo de ejecución.
cam: transform principal de la cámara en escena que actúa como origen del movimiento.
initialPos: posición original del plano para preservar sus valores Y y Z.
*/
public class Parallax : MonoBehaviour
{
    // parallax define la proporción del desplazamiento horizontal de la textura respecto al recorrido de la cámara.
    [SerializeField] private float parallax;
    // mat almacena el material del Renderer para poder ajustar su offset sin instanciar nuevos materiales.
    Material mat;
    // cam apunta al Transform de la cámara principal para leer su posición cada frame.
    Transform cam;
    // initialPos retiene la posición original del plano para reutilizar los valores Y y Z sin acumulación de errores.
    Vector3 initialPos;

    /*
    Start
    Inicializa las referencias necesarias para el efecto:
    - Obtiene el material actual del Renderer para modificar su textura.
    - Guarda la referencia al Transform de la cámara principal.
    - Captura la posición inicial del plano para anclarlo en Y y Z.
    */
    void Start()
    {
        // Obtener la instancia de material del Renderer asociado a este GameObject.
        mat = GetComponent<Renderer>().material;
        
        // Buscar la cámara principal de la escena y guardar su transform para lecturas rápidas.
        cam = Camera.main.transform;

        // Guardar la posición inicial del plano para reutilizar sus coordenadas verticales y de profundidad.
        initialPos = transform.position;
    }

    
    /*
    Update
    Recalcula cada frame la posición y el offset de la textura para mantener el efecto de paralaje coherente con el desplazamiento de la cámara.
    */
    void Update()
    {
        // Ajustar la posición del plano para que siga a la cámara en X conservando los valores Y y Z originales.
        transform.position = new Vector3(cam.position.x, initialPos.y, initialPos.z);

        // Aplicar un offset a la textura: cuanto mayor sea parallax, mayor será la diferencia respecto al movimiento de la cámara.
        mat.mainTextureOffset = new Vector2(cam.position.x * parallax, 0);
    }
}
