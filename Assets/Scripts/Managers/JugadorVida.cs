using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorVida : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 100;
    public int vidaActual = 100;
    public int vidas = 3;

    [Header("Invencibilidad")]
    public float tiempoInvencible = 1.5f;
    bool esInvencible = false;
    bool estaMuriendo = false;

    [Header("Proteccion inicio")]
    public float tiempoProteccion = 2f;
    bool protegido = true;

    [Header("Game Over")]
    public GameObject canvasGameOver; // ← arrastra CanvasGameOver aquí

    void Start()
    {
        estaMuriendo = false;
        esInvencible = false;
        protegido = true;
        vidaActual = vidaMaxima;

        if (GameData.Instance != null)
        {
            if (GameData.Instance.vidas <= 0)
                GameData.Instance.vidas = 3;
            vidas = GameData.Instance.vidas;
        }
        else
        {
            vidas = 3;
        }

        Debug.Log("Jugador iniciado — Vidas: " + vidas + " Vida: " + vidaActual);
        StartCoroutine(QuitarProteccion());
    }

    System.Collections.IEnumerator QuitarProteccion()
    {
        yield return new WaitForSeconds(tiempoProteccion);
        protegido = false;
        Debug.Log("Proteccion quitada");
    }

    public void RecibirDanio(int cantidad)
    {
        if (protegido || esInvencible || estaMuriendo) return;

        vidaActual -= cantidad;
        vidaActual = Mathf.Max(vidaActual, 0);
        Debug.Log("Vida: " + vidaActual);

        if (vidaActual <= 0)
            Morir();
        else
            StartCoroutine(PeriodoInvencible());
    }

    System.Collections.IEnumerator PeriodoInvencible()
    {
        esInvencible = true;
        yield return new WaitForSeconds(tiempoInvencible);
        esInvencible = false;
    }

    public void Morir()
    {
        if (estaMuriendo || protegido) return;
        estaMuriendo = true;

        vidas--;
        vidas = Mathf.Max(vidas, 0);

        if (GameData.Instance != null)
            GameData.Instance.vidas = vidas;

        Debug.Log("Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            Debug.Log("GAME OVER");
            StartCoroutine(MostrarGameOver());
        }
        else
        {
            Debug.Log("Reiniciando nivel...");
            StartCoroutine(ReiniciarNivel());
        }
    }

    System.Collections.IEnumerator MostrarGameOver()
    {
        yield return new WaitForSeconds(1f);

        if (canvasGameOver != null)
            canvasGameOver.SetActive(true);
        else
            Debug.LogError("Asigna CanvasGameOver en el Inspector del Jugador");
    }

    System.Collections.IEnumerator ReiniciarNivel()
    {
        yield return new WaitForSeconds(1f);
        estaMuriendo = false;
        vidaActual = vidaMaxima;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}