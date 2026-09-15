using UnityEngine;

public class Bala : MonoBehaviour
{
    public int danio = 25;

    void OnTriggerEnter(Collider other)
    {
        // Daña al enemigo
        if (other.CompareTag("Enemigo"))
        {
            MineroEnemigo enemigo = other
                .GetComponent<MineroEnemigo>();
            if (enemigo != null)
                enemigo.RecibirDanio(danio);

            Destroy(gameObject);
        }

        // Se destruye al tocar el suelo
        if (other.CompareTag("Suelo"))
            Destroy(gameObject);
    }
}