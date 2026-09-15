using UnityEngine;

public class ZonaBifurcacion : MonoBehaviour
{
    public GameObject panelBifurcacion;
    bool yaActivo = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Jugador")) return;
        if (yaActivo) return;

        yaActivo = true;
        if (panelBifurcacion != null)
            panelBifurcacion.SetActive(true);

        // Se oculta después de 3 segundos
        StartCoroutine(OcultarPanel());
    }

    System.Collections.IEnumerator OcultarPanel()
    {
        yield return new WaitForSeconds(3f);
        if (panelBifurcacion != null)
            panelBifurcacion.SetActive(false);
    }
}