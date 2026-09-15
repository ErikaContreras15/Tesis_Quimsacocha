using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelManager : MonoBehaviour
{
    public static NivelManager Instance;

    [Header("Configuracion del Nivel")]
    public int nivelActual = 1;
    public int totalPreguntas = 5;
    public int respuestasCorrectas = 0;
    public int preguntaActual = 0;
    public float porcentajeMinimo = 0.6f;

    [Header("Estado")]
    public bool nivelTerminado = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResponderPregunta(bool esCorrecta)
    {
        if (nivelTerminado) return;

        preguntaActual++;

        if (esCorrecta)
        {
            respuestasCorrectas++;
            GameData.Instance.RespuestaCorrecta();
            Debug.Log("Respuesta correcta! Puntaje: "
                + GameData.Instance.puntajeTotal);
        }
        else
        {
            GameData.Instance.RespuestaIncorrecta();
            Debug.Log("Respuesta incorrecta. Vidas: "
                + GameData.Instance.vidas);
        }

        if (preguntaActual >= totalPreguntas)
        {
            VerificarFinDeNivel();
        }
    }

    void VerificarFinDeNivel()
    {
        nivelTerminado = true;
        float puntaje = (float)respuestasCorrectas / totalPreguntas;

        if (puntaje >= porcentajeMinimo)
        {
            Debug.Log("Nivel superado con " + (puntaje * 100) + "%");
            SubirDeNivel();
        }
        else
        {
            Debug.Log("Nivel fallido. Reiniciando...");
            ReiniciarNivel();
        }
    }

    void SubirDeNivel()
    {
        GameData.Instance.nivelActual++;

        // Desbloquea ropa del nivel
        DesbloquearRopa(nivelActual);

        // Marca nivel como completado
        if (nivelActual == 1)
            GameData.Instance.nivel1Completado = true;
        else if (nivelActual == 2)
            GameData.Instance.nivel2Completado = true;
        else if (nivelActual == 3)
            GameData.Instance.nivel3Completado = true;

        // Carga siguiente escena
        int siguienteNivel = nivelActual + 1;

        if (siguienteNivel <= 3)
        {
            SceneManager.LoadScene("Nivel" + siguienteNivel
                + "_" + ObtenerNombreNivel(siguienteNivel));
        }
        else
        {
            SceneManager.LoadScene("Victoria");
        }
    }

    void ReiniciarNivel()
    {
        if (GameData.Instance.vidas <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name);
        }
    }

    void DesbloquearRopa(int nivel)
    {
        string ropa = "";
        if (nivel == 1) ropa = "Poncho_Toquilla";
        else if (nivel == 2) ropa = "Banda_Chaleco";
        else if (nivel == 3) ropa = "Kushma_Condor";

        if (!GameData.Instance.ropaDesbloqueada.Contains(ropa))
        {
            GameData.Instance.ropaDesbloqueada.Add(ropa);
            Debug.Log("Ropa desbloqueada: " + ropa);
        }
    }

    string ObtenerNombreNivel(int nivel)
    {
        if (nivel == 1) return "Paramo";
        if (nivel == 2) return "Lagunas";
        if (nivel == 3) return "Maquina";
        return "";
    }

    // Para probar desde el Inspector
    [ContextMenu("Simular Respuesta Correcta")]
    public void SimularCorrecta()
    {
        ResponderPregunta(true);
    }

    [ContextMenu("Simular Respuesta Incorrecta")]
    public void SimularIncorrecta()
    {
        ResponderPregunta(false);
    }
}