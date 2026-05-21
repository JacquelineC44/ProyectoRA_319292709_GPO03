using UnityEngine;

public class mover_margay : MonoBehaviour
{
    public Transform[] puntos;
    public float velocidad = 0.5f;
    public float velocidadGiro = 5f;

    [Header("Seguir cámara")]
    public Camera camara;
    public float distanciaFrenteCamara = 2f;
    public float distanciaParaSentarse = 0.25f;

    [Header("Datos del animal")]
    public string nombreAnimal;
    public string nombreCientifico;
    public GameObject panelAnimal;
    public GameObject panelHabitat;

    private int puntoActual = 0;
    private bool siguiendoCamara = false;

    public Animator anim;

    [Header("Sonido")]
    public AudioClip sonidoAnimal;
    public AudioSource audioSource;

    [Header("Datos de Hábitat")]
    public Sprite imagenHabitat;

    [TextArea(4, 10)]
    public string textoHabitat;

    [Header("Panel Amenazas")]
    public GameObject panelAmenazas;

    public Sprite amenazaImagen1;
    public Sprite amenazaImagen2;
    public Sprite amenazaImagen3;

    public string amenazaTexto1;
    public string amenazaTexto2;
    public string amenazaTexto3;

    [TextArea(4, 10)]
    public string amenazaInfoAdicional;

    [Header("Panel Datos Interesantes")]
    public GameObject panelDatosInteresantes;

    public Sprite datosImagen;

    public string datoInteresante1;
    public string datoInteresante2;
    public string datoInteresante3;



    //public void ReproducirSonido()
    //{
    //    if (audioSource != null && sonidoAnimal != null)
    //    {
    //        audioSource.PlayOneShot(sonidoAnimal);
    //    }
    //}
    public void ReproducirSonido()
    {
        if (audioSource != null && sonidoAnimal != null)
        {
            audioSource.clip = sonidoAnimal;
            audioSource.Play();
        }
    }
    public void DetenerSonido()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (siguiendoCamara)
        {
            MoverHaciaCamara();
            return;
        }

        Patrullar();
    }

    void Patrullar()
    {
        if (puntos == null || puntos.Length == 0) return;

        Transform destino = puntos[puntoActual];

        Vector3 direccion = destino.position - transform.position;
        direccion.y = 0;

        if (direccion.magnitude > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino.position,
                velocidad * Time.deltaTime
            );

            GirarHacia(direccion);

            if (anim != null)
                anim.SetBool("Sentado", false);
        }
        else
        {
            puntoActual++;

            if (puntoActual >= puntos.Length)
                puntoActual = 0;
        }
    }

    void MoverHaciaCamara()
    {
        Vector3 destino = camara.transform.position + camara.transform.forward * distanciaFrenteCamara;
        destino.y = transform.position.y;

        Vector3 direccion = destino - transform.position;
        direccion.y = 0;

        float distancia = direccion.magnitude;

        if (distancia > distanciaParaSentarse)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino,
                velocidad * Time.deltaTime
            );

            GirarHacia(direccion);

            if (anim != null)
                anim.SetBool("Sentado", false);
        }
        else
        {
            MirarCamara();

            if (anim != null)
                anim.SetBool("Sentado", true);
        }
    }

    public void SeguirCamara()
    {
        siguiendoCamara = true;

        if (anim != null)
            anim.SetBool("Sentado", false);
    }


    public void ContinuarAnimal()
    {
        siguiendoCamara = false;

        DetenerSonido();

        if (anim != null)
            anim.SetBool("Sentado", false);
    }
    void GirarHacia(Vector3 direccion)
    {
        if (direccion == Vector3.zero) return;

        Quaternion rotacion = Quaternion.LookRotation(direccion);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rotacion,
            velocidadGiro * Time.deltaTime
        );
    }

    void MirarCamara()
    {
        Vector3 direccion = camara.transform.position - transform.position;
        direccion.y = 0;

        GirarHacia(direccion);
    }
}