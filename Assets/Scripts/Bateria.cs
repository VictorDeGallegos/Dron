using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Componente auxiliar que modela el comportamiento de una bateria interna
// Dicha batería se descarga constantemente a menos que se utilize un método para recargar
public class Bateria : MonoBehaviour
{
  public float bateria; // Esta cifra es equivalente a los segundos activos de la batería
  public float capacidadMaximaBateria; // Indica la capacidad máxima de la batería
  public float velocidadDeCarga; // Escalar para multiplicar la velocidad de carga de la batería
  private bool cargando = false;

  void Update()
  {
    if (bateria > 0 && !cargando) // esto desgasta la bateria y revisa si cargando es falso o cargando == false
      bateria -= Time.deltaTime; // esto tambien 
    if (bateria < 0)
    {
      bateria = 0; //Aqui si se evita que sea negativa
    }
  }

  // ========================================
  // Métodos públicos que podrán ser utilizados por otros componentes (scripts):
  public void Cargar()
  {
    cargando = true;
    Debug.Log("Soy " + transform.parent.name);
    Debug.Log("Bateria = " + bateria);
    Debug.Log("CMB = " + capacidadMaximaBateria);
    if (bateria < capacidadMaximaBateria)
    {
      Debug.Log("Cargandoo");
      bateria += Time.deltaTime * velocidadDeCarga;
    }
  }

  public float NivelDeBateria()
  {
    return bateria;
  }
}
