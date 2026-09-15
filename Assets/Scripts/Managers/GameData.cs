using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [Header("Jugador")]
    public string nombreJugador = "";
    public string personajeElegido = "";
    public int nivelActual = 1;
    public int vidas = 3;
    public int puntajeTotal = 0;

    [Header("Progreso")]
    public bool nivel1Completado = false;
    public bool nivel2Completado = false;
    public bool nivel3Completado = false;

    [Header("Vestuario")]
    public List<string> ropaDesbloqueada = new List<string>();

    [Header("Ecosistema Quimsacocha")]
    public float calidadAgua = 100f;
    public float coberturaParamo = 100f;
    public float nivelArsenico = 0f;
    public float caudalRio = 51f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RespuestaCorrecta()
    {
        calidadAgua = Mathf.Clamp(calidadAgua + 5f, 0f, 100f);
        coberturaParamo = Mathf.Clamp(coberturaParamo + 3f, 0f, 100f);
        nivelArsenico = Mathf.Clamp(nivelArsenico - 2f, 0f, 100f);
        puntajeTotal += 100;
    }

    public void RespuestaIncorrecta()
    {
        calidadAgua = Mathf.Clamp(calidadAgua - 8f, 0f, 100f);
        coberturaParamo = Mathf.Clamp(coberturaParamo - 5f, 0f, 100f);
        nivelArsenico = Mathf.Clamp(nivelArsenico + 4f, 0f, 100f);
        vidas--;
    }
    public void CargarEscena(int indice)
    {
        StartCoroutine(CargarEscenaCoroutine(indice));
    }

    System.Collections.IEnumerator CargarEscenaCoroutine(int indice)
    {
        Debug.Log("GameData — cargando escena: " + indice);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("GameData — ejecutando LoadScene: " + indice);
        SceneManager.LoadScene(indice);
    }


}