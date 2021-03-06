using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class ComportamientoAutomatico2 : MonoBehaviour
{
  private Team Team => _team;
  [SerializeField] private Team _team;
  private Sensores sensor;
  private Actuadores actuador;
  private int count;
  public TextMeshProUGUI countText;
  public GameObject ganadorTextObject;
  public GameObject baseCarga;
  public Transform baseCargaT;
  public int grados = 0;
  public bool rotando = false, dir = false;
  public Vector3 posBase = Vector3.zero; //(0,0,0) 

  private GameObject _target;
  private enum Estado { AvanzarAlFrente, RotarRandom, Attack }; //Generar pequeña estructura de datos con un estado inicial llamado AvanzarAlFrentee
  private Estado estadoActual; //El estado actual indicara la accion que se realiza "AvanzarAlFrente o RotarRandom"

  public GameObject Bala;
  public float tiempoTranscurrido = 0;
  public float tiempodeDisparo;

  public Material rojo;
  public Material azul;
  void Start()
  {
    sensor = GetComponent<Sensores>();
    actuador = GetComponent<Actuadores>();
    posBase = baseCargaT.position;
    posBase = new Vector3(posBase.x, transform.position.y, posBase.z);
    count = 0;
    SetCountText();
    ganadorTextObject.SetActive(false);
  }

  void Update()
  {
    tiempoTranscurrido += Time.deltaTime;
  }
  void FixedUpdate()
  {
    if (sensor.Bateria() <= 0)
    {
      return;
    }

    actuador.Flotar();
    if (sensor.Bateria() < 60)
    {
      //Origen destino velocidad
      actuador.Detener();
      transform.position = Vector3.MoveTowards(sensor.Ubicacion(), posBase, Time.deltaTime);

    }
    else

      //Punto para determinar la accion a realizar
      switch (estadoActual)
      {
        //CASO 1 AVANZAR AL FRENTE
        //Moverse en dirección al frente mientras no se tenga una pared cerca 
        //Si hay una pared u objeto enfrente se detiene y gira la derecha y 
        //se cambia el estado a RotarRandom

        case (Estado.AvanzarAlFrente):
          if (rotando)
          {
            rotar();
          }
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
            count = count + 1;
            SetCountText();
            Debug.Log("Punto obtenido");
          }
          break;

        //CASO 2 ROTAR DE MANERA CONTINUAA
        //Girar levemente y cambiar al estado AvanzarAlFrente si no hay pared 
        //u objeto al frente al frente al frente al frente al frente
        //Se trata de un movimiento continuo que gira y avanza al mismo tiempo 
        case (Estado.RotarRandom):
          actuador.Detener();
          if (!sensor.FrenteAPared())
            estadoActual = Estado.AvanzarAlFrente;
          break;
      }
  }

  void SetCountText()
  {
    countText.text = "Puntos: " + count.ToString();
    if (count >= 3)
    {
      ganadorTextObject.SetActive(true);
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
  internal void Disparar(Vector3 direccion)
  {
    // Debug.Log("Disparar");
    if (tiempoTranscurrido > tiempodeDisparo)
    {
      tiempoTranscurrido = 0;
      GameObject balaTemporal = Instantiate(Bala, transform.position, Quaternion.identity);
      balaTemporal.GetComponent<Bala>().direccion = direccion; // Accediendo a la variable bala
      if (transform.CompareTag("DronRojo"))
      {
        // Debug.Log("DispararR");
        balaTemporal.GetComponent<Bala>().material = rojo; // accediendo a color de bala

      }
      else
      {
        // Debug.Log("DispararA");
        balaTemporal.GetComponent<Bala>().material = azul;// accediendo a color de bala

      }
    }
  }
  bool randomDir()
  {
    return (Random.value > 0.5f);
  }
}
public enum Team
{
  Red,
  Blue
}