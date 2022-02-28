using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{

  public bool rot;

  //Hace rota el GameObject al que esta asociado en
  //un rango 60 grados por frame.

  public Sensores sensor;
  void Update()
  {
    if (sensor.Bateria() > 0)
    {
      this.transform.Rotate(new Vector3(0.0f, 60.0f, 0.0f));
    }
  }

}
