using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIVida : MonoBehaviour
{
    [Header("Referencias")]
    public Slider barraVida;
    public TextMeshProUGUI textoVidas;
    public Image fillBarra; // arrastra el Fill aquí

    private JugadorVida jugadorVida;

    /*void Start()
    {
        jugadorVida = FindFirstObjectByType<JugadorVida>();

        if (jugadorVida == null)
            Debug.LogError("No se encontró JugadorVida");

        if (barraVida != null)
            barraVida.maxValue = 100;
    }*/

    

    void Update()
    {
        if (jugadorVida == null) return;

        Debug.Log("Vida actual: " + jugadorVida.vidaActual + " Vidas: " + jugadorVida.vidas);

        if (barraVida != null)
            barraVida.value = jugadorVida.vidaActual;

        if (textoVidas != null)
            textoVidas.text = "Vidas: " + jugadorVida.vidas;

        if (fillBarra != null)
        {
            float porcentaje = jugadorVida.vidaActual / 100f;
            fillBarra.color = Color.Lerp(Color.red, Color.green, porcentaje);
        }
    }

    void Start()
    {
        jugadorVida = FindFirstObjectByType<JugadorVida>();

        if (jugadorVida == null)
            Debug.LogError("No se encontró JugadorVida");

        if (barraVida != null)
            barraVida.maxValue = 100;

        // Verifica el fill
        if (fillBarra == null)
            Debug.LogError("Fill Barra NO está asignado en el Inspector");
        else
            Debug.Log("Fill Barra asignado correctamente: " + fillBarra.name);
    }
}