using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorCarretera : MonoBehaviour {
    public float velocidad;
    public bool inicio;
    public bool final;
    public bool pausa;

    public GameObject ContenedorCalleGo;
    public GameObject[] ContenedorCalleArray;

    int contadorCalle = 0;
    int numeroSelectorCalle;

    public GameObject calleAnterior;
    public GameObject calleNuevo;

    public Vector3 medidaLimitePantalla;
    public bool salidaPantalla;
    public GameObject mCamGo;
    public Camera mCamComp;

    public float calleSize;


    public GameObject tankGO;
    public GameObject bgFinalGO;



    // Use this for initialization
    void Start () {
        InicioJuego();
	}
    void InicioJuego()
    {
        ContenedorCalleGo = GameObject.Find("ContenedorCalles");
        mCamGo  = GameObject.Find("Main Camera");
        mCamComp=mCamGo.GetComponent< Camera >();
        bgFinalGO = GameObject.Find("Panel");
        bgFinalGO.SetActive(false);

        tankGO = GameObject.FindObjectOfType<Tank>().gameObject;

        VelocidadMotor();
        BuscarCalle();


    }
    void VelocidadMotor()
    {
        velocidad = 8;
    }
    void BuscarCalle()
    {
        ContenedorCalleArray = GameObject.FindGameObjectsWithTag("calle");
        for (int i = 0; i < ContenedorCalleArray.Length; i++)
        {
            ContenedorCalleArray[i].gameObject.transform.parent = ContenedorCalleGo.transform;
            ContenedorCalleArray[i].gameObject.SetActive(false);
            ContenedorCalleArray[i].gameObject.name = "CalleOFF_" + i;
        }
        CrearCalle();
    }
    void CrearCalle()
    {
        contadorCalle++;
        numeroSelectorCalle = Random.Range(0, ContenedorCalleArray.Length);
        GameObject Calle = Instantiate(ContenedorCalleArray[numeroSelectorCalle]);
        Calle.SetActive(true);
        Calle.name = "Calle" + contadorCalle;
        Calle.transform.parent = gameObject.transform;
        PosicionarCalle();
    }
    void PosicionarCalle()
    {
        calleAnterior = GameObject.Find("Calle" + (contadorCalle - 1));
        calleNuevo = GameObject.Find("Calle" + contadorCalle);
        MedirCalle();
        calleNuevo.transform.position = new Vector3(calleAnterior.transform.position.x, calleAnterior.transform.position.y + calleSize-2F, 0);
        salidaPantalla = false;
    }

    void MedirCalle()
    {
        for (int i = 0; i < calleAnterior.transform.childCount; i++)
        {
            if (calleAnterior.transform.GetChild(i).gameObject.GetComponent<Pieza>() != null) { }
            float piezaSize = calleAnterior.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            calleSize = calleSize + piezaSize;
        }
    }

    void MedirPantalla()
    {
        medidaLimitePantalla = new Vector3(0, mCamComp.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.5f, 0);
    }
    void DestruyoCalles()
    {
        Destroy(calleAnterior);
        calleSize = 0;
        calleAnterior = null;
        CrearCalle();
    }
    // Update is called once per frame


         public void JuegoEstados()
    {
        bgFinalGO.SetActive(true);
    }
    void Update () {
       if (inicio == true && final == false)
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
            if(calleAnterior.transform.position.y + calleSize <medidaLimitePantalla.y && salidaPantalla==false)
            {
                salidaPantalla = true;
                DestruyoCalles();
                
              
            }
        }

    }
}
