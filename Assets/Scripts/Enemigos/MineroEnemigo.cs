using UnityEngine;

public class MineroEnemigo : MonoBehaviour
{
    [Header("Configuracion")]
    public float velocidad = 2f;
    public float distanciaDeteccion = 5f;
    public float distanciaAtaque = 1.2f;
    public int danioAtaque = 20;
    public int vidaMaxima = 100;
    public int vidaActual = 100;

    [Header("Patrulla")]
    public float limiteIzquierdo = -2f;
    public float limiteDerecho = 2f;

    [Header("Ataque")]
    public float tiempoEntreAtaques = 1.5f;
    float timerAtaque = 0f;

    Transform jugador;
    bool moviendoDerecha = true;
    float posicionInicial;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicionInicial = transform.position.x;
        limiteIzquierdo = posicionInicial - 3f;
        limiteDerecho = posicionInicial + 3f;

        GameObject obj = GameObject.FindGameObjectWithTag("Jugador");
        if (obj != null)
            jugador = obj.transform;
        else
            Debug.LogError("No se encontró el Jugador — verifica el Tag");
    }

    void Update()
    {
        if (jugador == null) return;

        timerAtaque -= Time.deltaTime;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaDeteccion)
            PerseguirJugador();
        else
            Patrullar();

        if (distancia <= distanciaAtaque && timerAtaque <= 0f)
            Atacar();
    }

    void Patrullar()
    {
        if (moviendoDerecha)
        {
            transform.position += Vector3.right * velocidad * Time.deltaTime;
            if (transform.position.x >= limiteDerecho)
                moviendoDerecha = false;
        }
        else
        {
            transform.position += Vector3.left * velocidad * Time.deltaTime;
            if (transform.position.x <= limiteIzquierdo)
                moviendoDerecha = true;
        }
    }

    void PerseguirJugador()
    {
        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0;
        direccion.z = 0;
        direccion.Normalize();
        transform.position += direccion * velocidad * 1.5f * Time.deltaTime;
    }

    void Atacar()
    {
        timerAtaque = tiempoEntreAtaques; // reinicia el cooldown

        JugadorVida vida = jugador.GetComponent<JugadorVida>();
        if (vida != null)
            vida.RecibirDanio(danioAtaque);

        Debug.Log("Minero atacó — Daño: " + danioAtaque);
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Minero recibió daño. Vida: " + vidaActual);

        if (vidaActual <= 0)
            Morir();
    }

    void Morir()
    {
        Debug.Log("¡Minero derrotado!");
        Destroy(gameObject, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }
}