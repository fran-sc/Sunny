using UnityEngine;

/*
ItemCollectAnim
Responsabilidad:

Controla el ciclo de vida de GameObjects temporales que representan efectos visuales de recolección.
Se instancian cuando el jugador recoge items y se autodestruyen tras un tiempo breve para no saturar la escena.

Estructuras de datos internas:

Este script no mantiene datos adicionales; solo programa la destrucción automática del GameObject al que está adjunto.
*/
public class ItemCollectAnim : MonoBehaviour
{
    /*
    Start
    Programa la destrucción automática del GameObject tras 0.5 segundos.
    Este patrón se usa para efectos de partículas o animaciones breves que no requieren limpieza manual.
    */
    void Start()
    {
        // Destruir este GameObject después de 0.5 segundos para liberar memoria y mantener la escena limpia.
        Destroy(gameObject, 0.5f);        
    }
}
