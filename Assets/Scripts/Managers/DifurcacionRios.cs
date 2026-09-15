using UnityEngine;

public class DifurcacionRios : MonoBehaviour
{
    public enum TipoRio
    {
        Tarqui,
        Contaminado,
        Precipicio
    }

    [Header("Tipo de zona")]
    public TipoRio tipoRio;

    [Header("Efectos")]
    public int danio = 30;

    private bool puedeActivar = false; // ← NUEVO

    void Start()
    {
        Invoke(nameof(ActivarTrigger), 1f); // ← espera 1 segundo al cargar
    }

    void ActivarTrigger()
    {
        puedeActivar = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!puedeActivar) return; // ← bloquea si aún no está listo
        if (!other.CompareTag("Jugador")) return;

        switch (tipoRio)
        {
            case TipoRio.Tarqui:
                RioTarqui(other);
                break;
            case TipoRio.Contaminado:
                RioContaminado(other);
                break;
            case TipoRio.Precipicio:
                Precipicio(other);
                break;
        }
    }

    void RioTarqui(Collider jugador)
    {
        Debug.Log("¡Elegiste el Río Tarqui! El agua está limpia.");
        if (GameData.Instance != null)
            GameData.Instance.RespuestaCorrecta();
    }

    void RioContaminado(Collider jugador)
    {
        Debug.Log("¡El agua está contaminada! Pierdes vida.");
        JugadorVida vida = jugador.GetComponent<JugadorVida>();
        if (vida != null)
            vida.RecibirDanio(danio);
        if (GameData.Instance != null)
            GameData.Instance.RespuestaIncorrecta();
    }

    void Precipicio(Collider jugador)
    {
        Debug.Log("¡Caíste al precipicio!");
        JugadorVida vida = jugador.GetComponent<JugadorVida>();
        if (vida != null)
            vida.Morir();
    }
}