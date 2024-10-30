using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMIGO2d : MonoBehaviour
{
    private Rigidbody rigibody;
    public float speedX;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        rigibody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rigibody.velocity = new Vector3(-speedX,0, 0);
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "bala")
        {
            Destroy(this.gameObject);
        }
    }
}
