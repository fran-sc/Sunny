using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Gem").Length == 0)
        {
            // Recargamos la escena
            Invoke("ReloadScene", 1);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
