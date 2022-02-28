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
