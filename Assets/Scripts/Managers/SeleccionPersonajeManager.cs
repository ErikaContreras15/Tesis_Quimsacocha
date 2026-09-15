using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SeleccionPersonajeManager : MonoBehaviour
{
    [Header("Panel del personaje")]
    public Image imagenPersonaje;
    public TMP_Text tituloPersonaje;
    public TMP_Text subtituloPersonaje;
    public TMP_Text descripcionPersonaje;
    public Slider barraNivel;
    public TMP_Text textoNivel;

    [Header("Lista de personajes")]
    public PersonajeData[] personajes;

    [Header("Botones")]
    public Button botonAnterior;
    public Button botonSiguiente;
    public Button botonSeleccionar;

    int indiceActual = 0;

    void Start()
    {
        // Busca automáticamente si están vacíos
        if (imagenPersonaje == null)
            imagenPersonaje = GameObject.Find("ImagenPersonaje")
                ?.GetComponent<Image>();

        if (tituloPersonaje == null)
            tituloPersonaje = GameObject.Find("TituloPersonaje")
                ?.GetComponent<TMP_Text>();

        if (subtituloPersonaje == null)
            subtituloPersonaje = GameObject.Find("SubtituloPersonaje")
                ?.GetComponent<TMP_Text>();

        if (descripcionPersonaje == null)
            descripcionPersonaje = GameObject.Find("DescripcionPersonaje")
                ?.GetComponent<TMP_Text>();

        if (barraNivel == null)
            barraNivel = GameObject.Find("BarraNivel")
                ?.GetComponent<Slider>();

        if (textoNivel == null)
            textoNivel = GameObject.Find("TextoNumeroNivel")
                ?.GetComponent<TMP_Text>();

        if (botonAnterior == null)
            botonAnterior = GameObject.Find("BotonAnterior")
                ?.GetComponent<Button>();

        if (botonSiguiente == null)
            botonSiguiente = GameObject.Find("BotonSiguiente")
                ?.GetComponent<Button>();

        if (botonSeleccionar == null)
            botonSeleccionar = GameObject.Find("BotonSeleccionar")
                ?.GetComponent<Button>();

        // Conecta los botones automáticamente
        if (botonAnterior != null)
            botonAnterior.onClick.AddListener(BotonAnterior);

        if (botonSiguiente != null)
            botonSiguiente.onClick.AddListener(BotonSiguiente);

        if (botonSeleccionar != null)
            botonSeleccionar.onClick.AddListener(BotonSeleccionar);

        // Verifica que hay personajes asignados
        if (personajes == null || personajes.Length == 0)
        {
            Debug.LogError("No hay personajes asignados " +
                "en el Inspector!");
            return;
        }

        MostrarPersonaje(0);
    }

    public void BotonSiguiente()
    {
        indiceActual++;
        if (indiceActual >= personajes.Length)
            indiceActual = 0;
        MostrarPersonaje(indiceActual);
    }

    public void BotonAnterior()
    {
        indiceActual--;
        if (indiceActual < 0)
            indiceActual = personajes.Length - 1;
        MostrarPersonaje(indiceActual);
    }

    void MostrarPersonaje(int indice)
    {
        if (personajes == null || personajes.Length == 0)
        {
            Debug.LogError("No hay personajes asignados!");
            return;
        }

        PersonajeData p = personajes[indice];

        if (p == null)
        {
            Debug.LogError("Personaje " + indice + " es null!");
            return;
        }

        // Actualiza la UI con verificaciones
        if (imagenPersonaje != null)
        {
            if (p.imagenPersonaje != null)
                imagenPersonaje.sprite = p.imagenPersonaje;
            imagenPersonaje.color = p.colorElemento;
        }

        if (tituloPersonaje != null)
            tituloPersonaje.text = p.nombrePersonaje;

        if (subtituloPersonaje != null)
            subtituloPersonaje.text = p.subtitulo;

        if (descripcionPersonaje != null)
            descripcionPersonaje.text = p.descripcion;

        if (barraNivel != null)
            barraNivel.value = p.nivelPersonaje;

        if (textoNivel != null)
            textoNivel.text = "Nivel " + p.nivelPersonaje;

        // Animación suave
        StartCoroutine(AnimarEntrada());
    }

    System.Collections.IEnumerator AnimarEntrada()
    {
        if (imagenPersonaje == null) yield break;

        RectTransform panel =
            imagenPersonaje.GetComponent<RectTransform>();
        Vector3 posOriginal = panel.localPosition;
        panel.localPosition = posOriginal + new Vector3(500, 0, 0);

        float tiempo = 0f;
        float duracion = 0.3f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            panel.localPosition = Vector3.Lerp(
                posOriginal + new Vector3(500, 0, 0),
                posOriginal,
                tiempo / duracion);
            yield return null;
        }

        panel.localPosition = posOriginal;
    }

    public void BotonSeleccionar()
    {
        if (GameData.Instance != null)
            GameData.Instance.personajeElegido =
                personajes[indiceActual].nombrePersonaje;

        Debug.Log("Personaje elegido: " +
            personajes[indiceActual].nombrePersonaje);

        SceneManager.LoadScene("Nivel1_Paramo");
    }
}