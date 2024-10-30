using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile1 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRB;

    private void Start() {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float angle = Mathf.Atan2(myRB.velocity.y, myRB.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
