using UnityEngine;

public class ColeccionableUI : MonoBehaviour
{
    public GameObject panelDato;

    public void OcultarDespues(float segundos)
    {
        StartCoroutine(Ocultar(segundos));
    }

    System.Collections.IEnumerator Ocultar(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        if (panelDato != null)
            panelDato.SetActive(false);
    }
}