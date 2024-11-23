using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallax;
    Material mat;
    Transform cam;
    Vector3 initialPos;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        
        cam = Camera.main.transform;

        initialPos = transform.position;
    }

    
    void Update()
    {
        // la capa de parallax se posiciona en X con la cámara (Y, Z se mantienen a su posición inicial)
        transform.position = new Vector3(cam.position.x, initialPos.y, initialPos.z);

        // la textura de la capa de parallax se desplaza en X en función del desplazamiento de la cámara
        mat.mainTextureOffset = new Vector2(cam.position.x * parallax, 0);
    }
}
