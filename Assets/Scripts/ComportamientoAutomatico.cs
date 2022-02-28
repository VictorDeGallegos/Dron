using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAutomatico : MonoBehaviour
{

  private Sensores sensor;
  private Actuadores actuador;

  void Start()
  {
    sensor = GetComponent<Sensores>();
    actuador = GetComponent<Actuadores>();
  }

  void FixedUpdate()
  {
    if (sensor.Bateria() <= 0)
    {
      return;
    }

    actuador.Flotar();

    //Comportmaiento automatico dado por el ayudante
    //Problema Principal con esta forma es generar un codigo spaguetti y tener cosas revueltas
    if (sensor.FrenteAPared())
    {
      actuador.Detener();
      actuador.GirarDerecha();
    }
    else
    {
      actuador.Adelante();
    }
  }
}
