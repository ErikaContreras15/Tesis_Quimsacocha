using UnityEngine;

public class JugadorMovimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadCaminar = 5f;
    public float velocidadCorrer = 9f;
    public float fuerzaSalto = 12f;

    [Header("Verificacion suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    [Header("Disparo")]
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 15f;
    public float tiempoEntreDisparos = 0.3f;

    [Header("Estado")]
    public bool estaEnSuelo = false;
    public bool estaMirandoDerecha = true;
    public bool estaVivo = true;

    Rigidbody rb;
    float timerDisparo = 0f;
    float inputHorizontal;
    bool quiereSaltar;
    bool quiereCorrer;
    bool quiereDisparar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!estaVivo) return;

        // Captura inputs
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        quiereSaltar = Input.GetButtonDown("Jump");
        quiereCorrer = Input.GetKey(KeyCode.LeftShift);
        quiereDisparar = Input.GetKeyDown(KeyCode.Z) ||
                         Input.GetKeyDown(KeyCode.X);

        // Verifica si está en el suelo
        estaEnSuelo = Physics.CheckSphere(
            puntoSuelo.position, radioSuelo, capaSuelo);

        // Voltea el personaje
        if (inputHorizontal > 0 && !estaMirandoDerecha)
            Voltear();
        else if (inputHorizontal < 0 && estaMirandoDerecha)
            Voltear();

        // Disparo
        timerDisparo -= Time.deltaTime;
        if (quiereDisparar && timerDisparo <= 0)
        {
            Disparar();
            timerDisparo = tiempoEntreDisparos;
        }
    }

    void FixedUpdate()
    {
        if (!estaVivo) return;

        // Movimiento horizontal
        float velocidad = quiereCorrer ?
            velocidadCorrer : velocidadCaminar;
        rb.linearVelocity = new Vector3(
            inputHorizontal * velocidad,
            rb.linearVelocity.y,
            0);

        // Salto
        if (quiereSaltar && estaEnSuelo)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x, fuerzaSalto, 0);
        }
    }

    void Voltear()
    {
        estaMirandoDerecha = !estaMirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void Disparar()
    {
        if (prefabBala == null || puntoDisparo == null) return;

        GameObject bala = Instantiate(
            prefabBala,
            puntoDisparo.position,
            Quaternion.identity);

        Rigidbody rbBala = bala.GetComponent<Rigidbody>();
        if (rbBala != null)
        {
            float direccion = estaMirandoDerecha ? 1f : -1f;
            rbBala.linearVelocity = new Vector3(
                velocidadBala * direccion, 0, 0);
        }

        Destroy(bala, 3f);
    }

    public void Morir()
    {
        estaVivo = false;
        Debug.Log("El jugador murió");
        // Aquí va la animación de muerte
    }
}