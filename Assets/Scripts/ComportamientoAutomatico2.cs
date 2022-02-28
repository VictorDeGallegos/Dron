using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAutomatico2 : MonoBehaviour
{

  private Sensores sensor;
  private Actuadores actuador;

  private enum Estado { AvanzarAlFrente, RotarDerecha }; //Generar pequeña estructura de datos con un estado inicial llamado AvanzarAlFrente
  private Estado estadoActual; //El estado actual indicara la accion que se realiza "AvanzarAlFrente o RotarDerecha"

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

    //Punto para determinar la accion a realizar
    switch (estadoActual)
    {
      //CASO 1 AVANZAR AL FRENTE
      //Moverse en dirección al frente mientras no se tenga una pared cerca 
      //Si hay una pared u objeto enfrente se detiene y gira la derecha y se cambia el estado a RotarDerecha
      case (Estado.AvanzarAlFrente):
        if (sensor.FrenteAPared())
        {
          estadoActual = Estado.RotarDerecha;
        }
        else
        {
          actuador.Adelante();
        }
        break;

      //CASO 2 ROTAR DE MANERA CONTINUA
      //Girar levemente y cambiar al estado AvanzarAlFrente si no hay pared u objeto al frente al frente al frente al frente al frente
      //Se trata de un movimiento continuo que gira y avanza al mismo tiempo 
      case (Estado.RotarDerecha):
        actuador.Detener();
        actuador.GirarDerecha();
        if (!sensor.FrenteAPared())
          estadoActual = Estado.AvanzarAlFrente;
        break;
    }
  }
}
