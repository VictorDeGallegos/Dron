using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAutomatico : MonoBehaviour
{

  private Sensores sensor;
  private Actuadores actuador;

  public GameObject baseCarga;
  public Transform baseCargaT;
  public int grados = 0;
  public bool rotando = false, dir = false;
  public Vector3 posBase = Vector3.zero; //(0,0,0)



  void Start()
  {
    sensor = GetComponent<Sensores>();
    actuador = GetComponent<Actuadores>();
    // posBase = tranform.transform.position;
    // posBase = GameObject.Find("Base").transform.position;
    // posBase = baseCarga.transform.position;
    posBase = baseCargaT.position;
    posBase = new Vector3(posBase.x, transform.position.y, posBase.z);

  }

  void FixedUpdate()
  {
    if (sensor.Bateria() <= 0)
    {
      return;
    }

    actuador.Flotar();
    if (sensor.Bateria() < 20)
    {
      //Origen destino velocidad
      actuador.Detener();
      transform.position = Vector3.MoveTowards(sensor.Ubicacion(), posBase, Time.deltaTime);
    }
    else
    {
      if (rotando)
      {
        rotar();
      }
      //Comportmaiento automatico dado por el ayudante
      //Problema Principal con esta forma es generar un codigo spaguetti y tener cosas revueltas
      else if (sensor.FrenteAPared())
      {
        actuador.Detener();
        dir = randomDir();
        rotando = true;
      }
      else
      {
        actuador.Adelante();
      }

      if (sensor.TocandoBasura())
      {
        actuador.Limpiar(sensor.GetBasura());
        Debug.Log("Limpie basura");
      }
    }
  }
  void rotar()
  {
    grados++;
    if (grados == 90)
    {
      rotando = false;
      grados = 0;
    }
    else
    {
      if (dir)
      {
        actuador.GirarDerecha();
      }
      else
      {
        actuador.GirarIzquierda();
      }
    }
  }
  bool randomDir()
  {
    return (Random.value > 0.5f);
  }
}
