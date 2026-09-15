using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Tooltip("0 = fondo totalmente fijo, 1 = se mueve igual que la cámara")]
    [Range(0f, 1f)]
    public float factorParallax = 0.3f;

    private Transform camara;
    private Vector3 posicionInicialFondo;
    private Vector3 posicionInicialCamara;

    void Start()
    {
        if (Camera.main != null)
        {
            camara = Camera.main.transform;
            posicionInicialCamara = camara.position;
        }
        else
        {
            Debug.LogWarning("ParallaxLayer: No se encontró una cámara con la etiqueta MainCamera.");
        }

        posicionInicialFondo = transform.position;
    }

    void LateUpdate()
    {
        if (camara == null) return;

        Vector3 desplazamientoCamara = camara.position - posicionInicialCamara;
        Vector3 nuevaPosicion = posicionInicialFondo + desplazamientoCamara * factorParallax;

        // Mantenemos la Z original del fondo para no romper el orden de capas
        nuevaPosicion.z = posicionInicialFondo.z;

        transform.position = nuevaPosicion;
    }
}
