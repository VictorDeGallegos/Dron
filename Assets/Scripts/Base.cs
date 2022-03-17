using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
  void OnTriggerStay(Collider col)
  {
    Debug.Log("base" + col.gameObject.name);
    if (string.Equals(col.name, "Cuerpo"))
    {
      //   col.gameObject.transform.parent.GetComponent<Actuadores>().CargarBateria();
    }
  }

}
