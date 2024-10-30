using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearEnemigo : MonoBehaviour
{
    public GameObject prefabEnemy;
    public float Zposition;
    public float xPosition;
    private void Start()
    {
        Createenemy();
    }
    void Createenemy()
    {
        Vector3 positioncreate = new Vector3(xPosition, 0, Zposition);
        GameObject moneda = Instantiate(prefabEnemy, positioncreate, transform.rotation);
        Invoke("Createenemy", 3.0f);
    }
}
