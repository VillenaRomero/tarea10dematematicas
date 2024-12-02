using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float launchModifier;
    [SerializeField] private Transform launchPoint;

    [SerializeField] private GameObject point;
    private GameObject[] pointsList;
    [SerializeField] private int pointsCount;
    [SerializeField] private float spaceBetween;

    private Vector3 direction;

    private void Start()
    {
        pointsList = new GameObject[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            pointsList[i] = Instantiate(point, launchPoint.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, launchPoint.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 mousePosition = ray.GetPoint(distance);
            Vector3 launchPosition = transform.position;

            // Calcular la dirección hacia el mouse
            direction = (mousePosition - launchPosition).normalized;

            // Hacer que el objeto apunte hacia la dirección calculada
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);

            // Actualizar la posición de los puntos de parábola
            for (int i = 0; i < pointsCount; i++)
            {
                pointsList[i].transform.position = CurrentPosition(i * spaceBetween);
            }

            // Disparar si se presiona el botón del mouse
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        // Instanciar el proyectil
        GameObject proyectile = Instantiate(proyectilePrefab, launchPoint.position, Quaternion.identity);

        // Obtener la dirección actual del arco
        Vector3 shootDirection = transform.forward; // Usar la dirección del frente del arco

        // Establecer la velocidad del proyectil
        proyectile.GetComponent<Rigidbody>().velocity = shootDirection * launchModifier;

        // Opcional: Si deseas que el proyectil tenga una trayectoria parabólica
        // Puedes agregar una fuerza hacia arriba para simular la gravedad
        proyectile.GetComponent<Rigidbody>().AddForce(Vector3.up * launchModifier, ForceMode.Impulse);
    }

    private Vector3 CurrentPosition(float t)
    {
        // Usar la dirección actual del arco para calcular la trayectoria
        Vector3 shootDirection = transform.forward; // Dirección hacia donde apunta el arco
        return launchPoint.position + (shootDirection * launchModifier * t) + (0.5f * Physics.gravity * (t * t));
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "enemigo")
        {
            Destroy(this.gameObject);
        }
    }
}