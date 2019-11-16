using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rg;

    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0, 0, moveVertical);
        movement = transform.TransformDirection(movement);
        Vector3 torque = new Vector3(0, moveHorizontal, 0);
        rg.AddForce(movement * speed);
        rg.AddTorque(torque);
    }
}
