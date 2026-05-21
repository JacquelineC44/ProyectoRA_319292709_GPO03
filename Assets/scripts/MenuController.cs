using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelMenu;
    public GameObject panelNuevo;
    public GameObject panelSiguiente;

    public void Iniciar()
    {
        panelMenu.SetActive(false);
        panelNuevo.SetActive(true);
    }

    public void Continuar()
    {
        panelNuevo.SetActive(false);
        panelSiguiente.SetActive(true);
    }

    public void CerrarApp()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}