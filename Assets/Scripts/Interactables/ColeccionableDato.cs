using UnityEngine;
using TMPro;

public class ColeccionableDato : MonoBehaviour
{
    [Header("Dato educativo")]
    [TextArea(2, 4)]
    public string titulo = "¿Sabías que...";
    [TextArea(3, 6)]
    public string dato = "307.861 habitantes de Cuenca dependen del agua de Quimsacocha.";

    [Header("Rotacion")]
    public float velocidadRotacion = 90f;

    [Header("UI — asigna desde el Inspector")]
    public GameObject panelDato;        // el panel que se muestra
    public TextMeshProUGUI textoTitulo; // texto del título
    public TextMeshProUGUI textoDato;   // texto del dato
    public float tiempoMostrar = 4f;    // segundos visible

    void Start()
    {
        // Aplica colores desde código
        if (textoTitulo != null)
            textoTitulo.color = new Color(1f, 0.86f, 0f); // amarillo

        if (textoDato != null)
            textoDato.color = Color.white; // blanco
    }

    void Update()
    {
        Debug.Log("Rotando");
        transform.Rotate(
            velocidadRotacion * Time.deltaTime,
            velocidadRotacion * Time.deltaTime,
            0f
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Jugador")) return;

        // Muestra el dato en pantalla
        if (panelDato != null)
        {
            if (textoTitulo != null) textoTitulo.text = titulo;
            if (textoDato != null) textoDato.text = dato;
            panelDato.SetActive(true);
        }

        // Desactiva el coleccionable (ya fue recogido)
        gameObject.SetActive(false);

        // Oculta el panel después de unos segundos
        // Buscamos el GameManager o usamos un FindObjectOfType
        ColeccionableUI ui = FindFirstObjectByType<ColeccionableUI>();
        if (ui != null)
            ui.OcultarDespues(tiempoMostrar);

        Debug.Log("Coleccionable recogido: " + titulo);
    }
}