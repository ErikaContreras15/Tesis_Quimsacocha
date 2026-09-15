using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPrincipalManager : MonoBehaviour
{
    [Header("Campos del formulario")]
    public TMP_InputField inputNombre;
    public TMP_InputField inputCorreo;
    public TMP_InputField inputContrasena;
    public TMP_InputField inputEdad;

    [Header("Paneles")]
    public GameObject panelRegistro;

    [Header("Mensajes")]
    public TMP_Text textoMensaje;

    void Start()
    {
        panelRegistro.SetActive(true);
    }

    public void BotonRegistrarse()
    {
        string nombre = inputNombre.text.Trim();
        string correo = inputCorreo.text.Trim();
        string contrasena = inputContrasena.text.Trim();
        string edadTexto = inputEdad.text.Trim();

        // Validaciones
        if (string.IsNullOrEmpty(nombre))
        {
            MostrarMensaje("Por favor ingresa tu nombre.");
            return;
        }
        if (string.IsNullOrEmpty(correo) || !correo.Contains("@"))
        {
            MostrarMensaje("Por favor ingresa un correo válido.");
            return;
        }
        if (string.IsNullOrEmpty(contrasena) || contrasena.Length < 6)
        {
            MostrarMensaje("La contraseña debe tener al menos 6 caracteres.");
            return;
        }
        if (string.IsNullOrEmpty(edadTexto) || !int.TryParse(edadTexto, out int edad))
        {
            MostrarMensaje("Por favor ingresa una edad válida.");
            return;
        }

        // Guarda datos en GameData
        GameData.Instance.nombreJugador = nombre;

        // Llama al backend para registrar
        StartCoroutine(RegistrarJugador(nombre, correo, contrasena, edad));
    }

    public void BotonIniciarSesion()
    {
        string correo = inputCorreo.text.Trim();
        string contrasena = inputContrasena.text.Trim();

        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
        {
            MostrarMensaje("Ingresa tu correo y contraseña.");
            return;
        }

        StartCoroutine(IniciarSesion(correo, contrasena));
    }

    System.Collections.IEnumerator RegistrarJugador(
        string nombre, string correo, string contrasena, int edad)
    {
        MostrarMensaje("Registrando...");

        string url = "https://tu-backend.com/api/auth/registro";
        string jsonBody = JsonUtility.ToJson(new RegistroData
        {
            nombre = nombre,
            correo = correo,
            contrasena = contrasena,
            edad = edad
        });

        using (UnityEngine.Networking.UnityWebRequest request =
            UnityEngine.Networking.UnityWebRequest.PostWwwForm(url, jsonBody))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result ==
                UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                MostrarMensaje("Registro exitoso. Cargando juego...");
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("SeleccionPersonaje");
            }
            else
            {
                MostrarMensaje("Error al registrar. Intenta de nuevo.");
            }
        }
    }

    System.Collections.IEnumerator IniciarSesion(string correo, string contrasena)
    {
        MostrarMensaje("Iniciando sesión...");

        string url = "https://tu-backend.com/api/auth/login";
        string jsonBody = JsonUtility.ToJson(new LoginData
        {
            correo = correo,
            contrasena = contrasena
        });

        using (UnityEngine.Networking.UnityWebRequest request =
            UnityEngine.Networking.UnityWebRequest.PostWwwForm(url, jsonBody))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result ==
                UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                MostrarMensaje("Sesión iniciada. Cargando juego...");
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("SeleccionPersonaje");
            }
            else
            {
                MostrarMensaje("Correo o contraseña incorrectos.");
            }
        }
    }

    public void BotonSalir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

    void MostrarMensaje(string mensaje)
    {
        if (textoMensaje != null)
            textoMensaje.text = mensaje;
        Debug.Log(mensaje);
    }
}

[System.Serializable]
public class RegistroData
{
    public string nombre;
    public string correo;
    public string contrasena;
    public int edad;
}

[System.Serializable]
public class LoginData
{
    public string correo;
    public string contrasena;
}

