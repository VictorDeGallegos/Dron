using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Componente auxiliar para genera rayos que detecten colisiones de manera lineal
// En el script actual dibuja y comprueba colisiones con un rayo al frente del objeto
// y a un costado, sin embargo, es posible definir más rayos de la misma manera.
public class Rayo : MonoBehaviour
{

  public float longitudDeRayo;
  public float longitudDeDisparo;
  private bool frenteAPared;

  void Update()
  {
    // Se muestra el rayo únicamente en la pantalla de Escena (Scene)
    Debug.DrawLine(transform.position, transform.position + (transform.forward * longitudDeRayo), Color.blue);
    Debug.DrawLine(transform.position, transform.position + (transform.forward * longitudDeDisparo), Color.red);
  }

  void FixedUpdate()
  {
    // Similar a los métodos OnTrigger y OnCollision, se detectan colisiones con el rayo:
    frenteAPared = false;
    RaycastHit raycastHit;

    if (Physics.Raycast(transform.position, transform.forward, out raycastHit, longitudDeRayo))
    {
      if (raycastHit.collider.gameObject.CompareTag("Pared"))
      {
        frenteAPared = true;
      }
    }
    if (Physics.Raycast(transform.position, transform.forward, out raycastHit, longitudDeDisparo))
    {
      // Debug.Log("Choque con" + raycastHit.collider.gameObject.transform.parent.name);
      if (raycastHit.collider.gameObject.transform.parent.gameObject.CompareTag("DronAzul") && gameObject.transform.parent.CompareTag("DronRojo"))
      {
        // Debug.Log("Disparando");
        // Accediendo al padre para utilizar en cualquier script
        Vector3 direccion = raycastHit.collider.gameObject.transform.parent.position;
        transform.parent.GetComponent<ComportamientoAutomatico2>().Disparar(direccion);
      }
    }
  }

  // Ejemplo de métodos públicos que podrán usar otros componentes (scripts):
  public bool FrenteAPared()
  {
    return frenteAPared;
  }
}
