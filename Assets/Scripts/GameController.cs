using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
GameController
Responsabilidad:

Gestiona el estado general del juego monitorizando el temporizador de cuenta regresiva y el número de gemas restantes.
Actualiza la interfaz de usuario (UI) cada frame con el tiempo restante y el contador de gemas.
Detecta condiciones de finalización (tiempo agotado o todas las gemas recolectadas) y reinicia la escena.

Estructuras de datos internas:

itemsText: referencia al componente TextMeshPro para mostrar el número de gemas restantes en pantalla.
timerText: referencia al componente TextMeshPro para mostrar el tiempo restante en segundos.
totalSeconds: duración total del nivel en segundos configurada desde el Inspector.
elapsedTime: tiempo transcurrido desde el inicio del nivel (acumulador de deltaTime).
*/
public class GameController : MonoBehaviour
{
    // itemsText: texto UI que muestra cuántas gemas quedan por recoger.
    [SerializeField] TMP_Text itemsText;
    // timerText: texto UI que muestra los segundos restantes de la partida.
    [SerializeField] TMP_Text timerText;
    // totalSeconds: límite de tiempo en segundos para completar el nivel.
    [SerializeField] int totalSeconds;

    // elapsedTime: acumulador del tiempo transcurrido usado para calcular el countdown.
    float elapsedTime;

    /*
    Update
    Actualiza cada frame el temporizador y el contador de gemas en la UI.
    Verifica las condiciones de fin de nivel (tiempo agotado o sin gemas) y programa el reinicio de la escena.
    */
    void Update()
    {
        // Incrementar el tiempo transcurrido con el delta del frame actual.
        elapsedTime += Time.deltaTime;
        // Calcular segundos restantes restando el tiempo transcurrido del total, evitando valores negativos.
        int secondsLeft = totalSeconds - (int)elapsedTime;
        if (secondsLeft < 0) secondsLeft = 0;
        // Actualizar el texto del temporizador en pantalla.
        timerText.text = secondsLeft.ToString();

        // Contar las gemas restantes en la escena mediante su tag.
        int items = GameObject.FindGameObjectsWithTag("Gem").Length;
        // Actualizar el texto del contador de gemas.
        itemsText.text = items.ToString();

        // Si no quedan gemas o el tiempo se agotó, programar reinicio del nivel con 1 segundo de retraso.
        if (items == 0 || secondsLeft == 0)
        {
            Invoke("RestartGame", 1f);
        }
    }

    /*
    RestartGame
    Recarga la escena actual (índice 0 en Build Settings) para reiniciar el nivel desde el principio.
    Este método se invoca con retraso desde Update cuando se cumplen las condiciones de fin de partida.
    */
    public void RestartGame()
    {
        // Cargar la primera escena del proyecto para reiniciar el nivel.
        SceneManager.LoadScene(0);
    }
}

