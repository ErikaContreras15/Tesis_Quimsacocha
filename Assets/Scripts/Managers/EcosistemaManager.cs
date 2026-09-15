using UnityEngine;

public class EcosistemaManager : MonoBehaviour
{
    public static EcosistemaManager Instance;

    [Header("Variables del Ecosistema - Datos ETAPA EP 2024")]
    [Range(0, 100)] public float calidadAgua = 100f;
    [Range(0, 100)] public float coberturaParamo = 100f;
    [Range(0, 100)] public float nivelArsenico = 0f;
    [Range(0, 51)] public float caudalRio = 51f;
    [Range(0, 100)] public float poblacionTrucha = 100f;
    [Range(0, 100)] public float biodiversidad = 100f;

    [Header("Estado del Ecosistema")]
    public string estadoActual = "Saludable";
    public bool ecosistemaEnPeligro = false;

    [Header("Objetos del juego que reaccionan")]
    public GameObject aguaLimpia;
    public GameObject aguaContaminada;
    public GameObject vegetacionSana;
    public GameObject vegetacionMuerta;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        ActualizarEstado();
        SincronizarConGameData();
    }

    public void MejorarEcosistema(float cantidad)
    {
        calidadAgua = Mathf.Clamp(calidadAgua + cantidad, 0f, 100f);
        coberturaParamo = Mathf.Clamp(coberturaParamo + (cantidad * 0.5f), 0f, 100f);
        nivelArsenico = Mathf.Clamp(nivelArsenico - (cantidad * 0.3f), 0f, 100f);
        poblacionTrucha = Mathf.Clamp(poblacionTrucha + (cantidad * 0.4f), 0f, 100f);
        biodiversidad = Mathf.Clamp(biodiversidad + (cantidad * 0.3f), 0f, 100f);

        Debug.Log("Ecosistema mejorado. Calidad agua: " + calidadAgua);
    }

    public void DeteriorarEcosistema(float cantidad)
    {
        calidadAgua = Mathf.Clamp(calidadAgua - cantidad, 0f, 100f);
        coberturaParamo = Mathf.Clamp(coberturaParamo - (cantidad * 0.5f), 0f, 100f);
        nivelArsenico = Mathf.Clamp(nivelArsenico + (cantidad * 0.4f), 0f, 100f);
        poblacionTrucha = Mathf.Clamp(poblacionTrucha - (cantidad * 0.6f), 0f, 100f);
        biodiversidad = Mathf.Clamp(biodiversidad - (cantidad * 0.4f), 0f, 100f);

        Debug.Log("Ecosistema deteriorado. Arsénico: " + nivelArsenico);
    }

    void ActualizarEstado()
    {
        if (calidadAgua >= 80f && nivelArsenico <= 10f)
        {
            estadoActual = "Saludable";
            ecosistemaEnPeligro = false;
        }
        else if (calidadAgua >= 50f && nivelArsenico <= 40f)
        {
            estadoActual = "En riesgo";
            ecosistemaEnPeligro = false;
        }
        else
        {
            estadoActual = "En peligro critico";
            ecosistemaEnPeligro = true;
        }

        // Cambia objetos visuales según estado
        if (aguaLimpia != null)
            aguaLimpia.SetActive(calidadAgua >= 50f);
        if (aguaContaminada != null)
            aguaContaminada.SetActive(calidadAgua < 50f);
        if (vegetacionSana != null)
            vegetacionSana.SetActive(coberturaParamo >= 50f);
        if (vegetacionMuerta != null)
            vegetacionMuerta.SetActive(coberturaParamo < 50f);
    }

    void SincronizarConGameData()
    {
        if (GameData.Instance != null)
        {
            GameData.Instance.calidadAgua = calidadAgua;
            GameData.Instance.coberturaParamo = coberturaParamo;
            GameData.Instance.nivelArsenico = nivelArsenico;
            GameData.Instance.caudalRio = caudalRio;
        }
    }

    [ContextMenu("Simular Mineria Activa")]
    public void SimularMineria()
    {
        DeteriorarEcosistema(20f);
    }

    [ContextMenu("Simular Proteccion Exitosa")]
    public void SimularProteccion()
    {
        MejorarEcosistema(15f);
    }
}