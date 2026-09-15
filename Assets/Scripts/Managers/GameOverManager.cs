using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start() { }

    public void BotonReintentar()
    {
        Debug.Log("Reintentar presionado");

        if (GameData.Instance != null)
        {
            GameData.Instance.vidas = 3;
            GameData.Instance.puntajeTotal = 0;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BotonMenu()
    {
        Debug.Log("Menu presionado");

        if (GameData.Instance != null)
        {
            GameData.Instance.vidas = 3;
            GameData.Instance.nivelActual = 1;
            GameData.Instance.puntajeTotal = 0;
        }

        SceneManager.LoadScene(0);
    }

}