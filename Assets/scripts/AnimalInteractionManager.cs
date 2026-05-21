using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class AnimalInteractionManager : MonoBehaviour
{
    public Camera camara;

    [Header("UI")]
    public TMP_Text textoNombreAnimal;
    public TMP_Text textoNombreCientifico;

    private mover_margay animalActual;

    public Image imagenHabitatUI;
    public TMP_Text textoHabitatUI;

    [Header("UI Amenazas")]
    public Image amenazaImagenUI1;
    public Image amenazaImagenUI2;
    public Image amenazaImagenUI3;

    public TMP_Text amenazaTextoUI1;
    public TMP_Text amenazaTextoUI2;
    public TMP_Text amenazaTextoUI3;

    public TMP_Text amenazaInfoAdicionalUI;

    [Header("UI Datos Interesantes")]
    public Image datosImagenUI;

    public TMP_Text datoInteresanteUI1;
    public TMP_Text datoInteresanteUI2;
    public TMP_Text datoInteresanteUI3;

    public GameObject panelInstruc;

    void Start()
    {
        if (animalActual != null)
        {
            animalActual.panelAnimal.SetActive(false);
            animalActual.panelHabitat.SetActive(false);
        }
    }

    void Update()
    {
        if (HayPanelInformativoAbierto())
            return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            RevisarToque(Mouse.current.position.ReadValue());

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            RevisarToque(Touchscreen.current.primaryTouch.position.ReadValue());
    }

    void RevisarToque(Vector2 posicionPantalla)
    {
        Ray ray = camara.ScreenPointToRay(posicionPantalla);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Animal"))
            {
                mover_margay nuevoAnimal = hit.collider.GetComponentInParent<mover_margay>();

                if (nuevoAnimal != null)
                {
                    if (animalActual != null && animalActual != nuevoAnimal)
                    {
                        animalActual.DetenerSonido();
                        animalActual.ContinuarAnimal();
                    }

                    animalActual = nuevoAnimal;

                    animalActual.SeguirCamara();

                    if (textoNombreAnimal != null)
                        textoNombreAnimal.text = animalActual.nombreAnimal;

                    if (textoNombreCientifico != null)
                        textoNombreCientifico.text = "Nombre científico: " + animalActual.nombreCientifico;

                    animalActual.ReproducirSonido();

                    if (animalActual.panelAnimal != null)
                    {
                        animalActual.panelAnimal.SetActive(true);
                        panelInstruc.SetActive(false);
                    }
                        
                }
            }
        }
    }
    public void CerrarPanel()
    {
        if (animalActual != null)
        {
            if (animalActual.panelAnimal != null)
                animalActual.panelAnimal.SetActive(false);

            animalActual.ContinuarAnimal();
        }
        panelInstruc.SetActive(true);
    }
    public void AbrirHabitat()
    {
        if (animalActual == null) return;

        animalActual.panelAnimal.SetActive(false);
        animalActual.panelHabitat.SetActive(true);

        imagenHabitatUI.sprite = animalActual.imagenHabitat;
        textoHabitatUI.text = animalActual.textoHabitat;
    }

    public void RegresarDeHabitat()
    {
        if (animalActual == null) return;

        animalActual.panelHabitat.SetActive(false);
        animalActual.panelAnimal.SetActive(true);
    }

    public void AbrirAmenazas()
    {
        if (animalActual == null) return;

        animalActual.panelAnimal.SetActive(false);
        animalActual.panelAmenazas.SetActive(true);

        amenazaImagenUI1.sprite = animalActual.amenazaImagen1;
        amenazaImagenUI2.sprite = animalActual.amenazaImagen2;
        amenazaImagenUI3.sprite = animalActual.amenazaImagen3;

        amenazaTextoUI1.text = animalActual.amenazaTexto1;
        amenazaTextoUI2.text = animalActual.amenazaTexto2;
        amenazaTextoUI3.text = animalActual.amenazaTexto3;

        amenazaInfoAdicionalUI.text = animalActual.amenazaInfoAdicional;
    }

    public void RegresarDeAmenazas()
    {
        if (animalActual == null) return;

        animalActual.panelAmenazas.SetActive(false);
        animalActual.panelAnimal.SetActive(true);
    }
    public void AbrirDatosInteresantes()
    {
        if (animalActual == null) return;

        animalActual.panelAnimal.SetActive(false);
        animalActual.panelDatosInteresantes.SetActive(true);

        datosImagenUI.sprite = animalActual.datosImagen;

        datoInteresanteUI1.text = animalActual.datoInteresante1;
        datoInteresanteUI2.text = animalActual.datoInteresante2;
        datoInteresanteUI3.text = animalActual.datoInteresante3;
    }

    public void RegresarDeDatosInteresantes()
    {
        if (animalActual == null) return;

        animalActual.panelDatosInteresantes.SetActive(false);
        animalActual.panelAnimal.SetActive(true);
    }

    bool HayPanelInformativoAbierto()
    {
        return animalActual != null &&
        (
            animalActual.panelHabitat.activeSelf ||
            animalActual.panelAmenazas.activeSelf ||
            animalActual.panelDatosInteresantes.activeSelf
        );
    }

}