using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje",
    menuName = "Guardianes/Personaje")]
public class PersonajeData : ScriptableObject
{
    [Header("Informacion")]
    public string nombrePersonaje;
    public string subtitulo;
    [TextArea(3, 5)]
    public string descripcion;
    public Sprite imagenPersonaje;
    public int nivelPersonaje = 1;

    [Header("Elemento")]
    public Color colorElemento = Color.white;
    public string elemento;
}
