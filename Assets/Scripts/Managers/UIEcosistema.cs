using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEcosistema : MonoBehaviour
{
    [Header("Agua")]
    public Slider barraAgua;
    public TextMeshProUGUI textoAgua;

    [Header("Paramo")]
    public Slider barraParamo;
    public TextMeshProUGUI textoParamo;

    [Header("Arsenico")]
    public Slider barraArsenico;
    public TextMeshProUGUI textoArsenico;

    void Update()
    {
        if (GameData.Instance == null)
        {
            Debug.LogError("GameData no encontrado");
            return;
        }

        Debug.Log("Agua: " + GameData.Instance.calidadAgua +
                  " Paramo: " + GameData.Instance.coberturaParamo +
                  " Arsenico: " + GameData.Instance.nivelArsenico);

        if (barraAgua != null)
            barraAgua.value = GameData.Instance.calidadAgua;
        if (textoAgua != null)
            textoAgua.text = "Agua: " +
                Mathf.RoundToInt(GameData.Instance.calidadAgua) + "%";

        if (barraParamo != null)
            barraParamo.value = GameData.Instance.coberturaParamo;
        if (textoParamo != null)
            textoParamo.text = "Paramo: " +
                Mathf.RoundToInt(GameData.Instance.coberturaParamo) + "%";

        if (barraArsenico != null)
            barraArsenico.value = GameData.Instance.nivelArsenico;
        if (textoArsenico != null)
            textoArsenico.text = "Arsenico: " +
                Mathf.RoundToInt(GameData.Instance.nivelArsenico) + "%";
    }
}