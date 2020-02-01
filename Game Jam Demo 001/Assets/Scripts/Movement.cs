using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] [Range(0.1f, 1)] float movementSpeed;
    [SerializeField] [Range(0.1f, 15)] float lookSensitivity = 3;
    [SerializeField] [Range(0.1f, 15)] float jumpVelocity = 3;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        Rotate();
        Move();
    }
     void Rotate()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 camRotation = new Vector3(xRot, 0, 0) * lookSensitivity;
        Vector3 rotation = new Vector3(0, yRot, 0) * lookSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        cam.transform.Rotate(-camRotation);
    }
    void Move()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;
        Vector3 velocity = (movHorizontal + movVertical).normalized * movementSpeed;
        rb.MovePosition(rb.position + velocity);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 1, 0) * jumpVelocity;
        }
    }
}
