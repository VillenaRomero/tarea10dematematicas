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

    private Vector2 direction;
    private float smoothChangeSpeed = 5f;
    private float verticalDirection = 0f;
    private float horizontalDirection = 0f;


    private void Start() {
        pointsList = new GameObject[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            pointsList[i] = Instantiate(point, launchPoint.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 launchPosition = transform.position;

        direction = mousePosition - launchPosition;
        direction = direction.normalized;

        if (Input.GetKey(KeyCode.W))
        {
            verticalDirection = Mathf.Lerp(verticalDirection, 1f, smoothChangeSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalDirection = Mathf.Lerp(verticalDirection, -1f, smoothChangeSpeed * Time.deltaTime);
        }
        else
        {
            verticalDirection = Mathf.Lerp(verticalDirection, 0f, smoothChangeSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalDirection = Mathf.Lerp(horizontalDirection, 1f, smoothChangeSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalDirection = Mathf.Lerp(horizontalDirection, -1f, smoothChangeSpeed * Time.deltaTime);
        }
        else
        {
            horizontalDirection = Mathf.Lerp(horizontalDirection, 0f, smoothChangeSpeed * Time.deltaTime);
        }

        direction = new Vector2(direction.x + horizontalDirection, direction.y + verticalDirection);
        direction = direction.normalized;

        transform.right = direction;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        for (int i = 0; i < pointsCount; i++)
        {
            pointsList[i].transform.position = CurrentPosition(i * spaceBetween);
        }
    }

    private void Shoot(){
        GameObject proyectile = Instantiate(proyectilePrefab, launchPoint.position, Quaternion.identity);
        proyectile.GetComponent<Rigidbody2D>().velocity = transform.right * launchModifier;
    }

    private Vector2 CurrentPosition(float t){
        return (Vector2)launchPoint.position + (direction.normalized * launchModifier * t) + (Vector2)(0.5f * Physics.gravity * (t * t));
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "enemigo")
        {
            Destroy(this.gameObject);
        }
    }
}
