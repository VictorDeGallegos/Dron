using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
  internal Vector3 direccion;
  public float velocidad;

  internal Material material;

  public Material rojo;
  public Material azul;

  void Start()
  {
    GetComponent<MeshRenderer>().material = material;
  }
  // Update is called once per frame
  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, direccion, velocidad * Time.deltaTime);
  }
  void OnTriggerEnter(Collider col)
  {
    if (col.transform.parent.CompareTag("DronAzul") && GetComponent<MeshRenderer>().sharedMaterial == rojo)
    {
      Debug.Log("Choque con BA" + col.transform.parent.name);
      Debug.Log("DESTRUIR" + col.transform.parent.name);
      Destroy(col.transform.parent.gameObject);
      Destroy(gameObject);
    }

    if (col.transform.parent.CompareTag("DronRojo") && GetComponent<MeshRenderer>().sharedMaterial == azul)
    {
      Debug.Log("Choque con BR" + col.transform.parent.name);
      Destroy(col.transform.parent.gameObject);
      Destroy(gameObject);
    }
  }
}
