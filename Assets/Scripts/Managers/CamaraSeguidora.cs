using UnityEngine;

public class CamaraSeguidora : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform jugador;
    public Vector3 offset = new Vector3(0, 2, -10);

    [Header("Suavidad")]
    public float velocidadSuavidad = 5f;

    [Header("Limites del nivel")]
    public float limiteIzquierdo = -10f;
    public float limiteDerecho = 50f;
    public float limiteAbajo = -5f;
    public float limiteArriba = 10f;

    void LateUpdate()
    {
        if (jugador == null) return;

        // Posicion objetivo
        Vector3 posObjetivo = jugador.position + offset;

        // Aplica limites
        posObjetivo.x = Mathf.Clamp(
            posObjetivo.x, limiteIzquierdo, limiteDerecho);
        posObjetivo.y = Mathf.Clamp(
            posObjetivo.y, limiteAbajo, limiteArriba);
        posObjetivo.z = offset.z;

        // Movimiento suave
        transform.position = Vector3.Lerp(
            transform.position,
            posObjetivo,
            velocidadSuavidad * Time.deltaTime);
    }
}