using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldado : MonoBehaviour {

    public GameObject cronometroGO;
    public Cronometro cronometroScript;
    public GameObject motorCarreteraGO;
    public MotorCarretera motorCarreteraScript;

    private void Start()
    {
        cronometroGO = GameObject.FindObjectOfType<Cronometro>().gameObject;
        cronometroScript = cronometroGO .GetComponent<Cronometro>();
        motorCarreteraGO = GameObject.Find("MotorCarretera");
        motorCarreteraScript = motorCarreteraGO.GetComponent<MotorCarretera>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Tank>() != null)
        {
            cronometroScript.score = cronometroScript.score + 3;
            motorCarreteraScript.velocidad = motorCarreteraScript.velocidad + 1;
            Destroy(this.gameObject);
            
        }
    }
}
