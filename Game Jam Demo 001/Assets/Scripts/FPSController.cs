using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] [Range(0.1f, 1)] float movementSpeed;
    [SerializeField] [Range(0.1f, 15)] float lookSensitivity = 3;
    [SerializeField] [Range(0.1f, 15)] float jumpVelocity = 3;
    InventoryManager invManager;

    State playerState = State.Controllable;
    void Start()
    {
        invManager = GetComponent<InventoryManager>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        if (playerState != State.Controllable) return;
        Move();
        Rotate();
        CheckInput();

    }
    void Interact(GameObject hit)
    {
        Debug.Log("Interacting with " + hit.transform.name);
        if (hit.transform.gameObject.GetComponent<ActivateObject>())
        {
            if (hit.transform.gameObject.GetComponent<ActivateObject>().Activate(invManager.selectedItem))
            {
                invManager.RemoveItem();
            }
            else
            {
                Debug.Log("Wrong Item");
            }
        }
        if (hit.transform.gameObject.GetComponent<NodeSlot>())
        {
            if (hit.transform.gameObject.GetComponent<NodeSlot>().SetNode(invManager.selectedItem))
            {
                invManager.RemoveItem();
            }
            else
            {
                Debug.Log("Wrong Item");
            }
        }
        else
        {
            invManager.AddItem(hit.transform.gameObject.GetComponent<ItemHolder>().myItem);
            hit.transform.gameObject.SetActive(false);
        }
    }
    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.tag == "Interactable")
                {
                    Interact(hit.transform.gameObject);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            invManager.SelectItem(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            invManager.SelectItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            invManager.SelectItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            invManager.SelectItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            invManager.SelectItem(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            invManager.SelectItem(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            invManager.SelectItem(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            invManager.SelectItem(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            invManager.SelectItem(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            invManager.SelectItem(8);
        }
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
public enum ItemType { MachinePart, Cable }
public enum State { Controllable, Animation };
public enum ItemVariation { A, B, C, D, F, G, H };

