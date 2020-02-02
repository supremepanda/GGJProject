using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPSController : MonoBehaviour
{
    static public FPSController instance;
    Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] [Range(0.1f, 100)] float movementSpeed;
    [SerializeField] [Range(0.1f, 15)] float lookSensitivity = 3;
    [SerializeField] [Range(0.1f, 15)] float jumpVelocity = 3;
    public InventoryManager invManager;
    [SerializeField] public Item sphereItem;
    public float sphereGlow = 0;
    AudioSource auSource;
    [SerializeField] AudioClip walkSound;
    State playerState = State.Controllable;
    [SerializeField] GameObject lightObject;
    bool isRunning = false;
    public bool lightTaken = false;
    bool lightEquipped = false;
    bool gameFinished=false;
    public bool BatteryTaken = false;
    int killedPM = 0;
    public int endingIndex = 1;
    public Text txt;
    string aHint;
    public int gearTryAmount=0;
    public bool paused=false;
    void Start()
    {
        aHint = "I couldn't figure it out. Maybe I should listen to mathematician Fibonacci and rest my head a bit";
        txt.text = "";
        StartCoroutine(PlaySound());
        if (instance == null)
        {
            instance = this;
        }
        invManager = GetComponent<InventoryManager>();
        auSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (paused) return;
        if (playerState != State.Controllable) return;
        Move();
        Rotate();
        CheckInput();

    }
    void InteractLeft(GameObject hit)
    {
        if (hit.transform.gameObject.GetComponent<NodeSlot>())
        {
            hit.transform.gameObject.GetComponent<NodeSlot>().RotateNode();
        }
    }
    void Interact(GameObject hit)
    {
        Debug.Log("Interacting with " + hit.transform.name);
        if (hit.transform.gameObject.GetComponent<ActivateObject>())
        {
            Debug.Log("hit Activate Object");
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
        if (hit.transform.gameObject.GetComponent<GearNode>())
        {
            if (hit.transform.gameObject.GetComponent<GearNode>().SetNode(invManager.selectedItem))
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
            Destroy(hit.transform.gameObject);
        }
    }
    Material lastMat;
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.tag == "Interactable")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Interact(hit.transform.gameObject);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    InteractLeft(hit.transform.gameObject);
                }
                Renderer rend = hit.transform.gameObject.GetComponent<Renderer>();
                if (rend)
                {
                    Material newMat = rend.materials[rend.materials.Length - 1];
                    if (newMat != null)
                    {
                        if (lastMat != newMat && lastMat != null)
                        {
                            lastMat.SetFloat("_Outline", 0);
                        }
                        lastMat = newMat;
                        lastMat.SetFloat("_Outline", 30);
                    }
                }

            }
            else if (hit.transform.tag == "PM")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Hit PM");
                    hit.transform.GetComponent<PacController>().Kill(transform.position);
                    killedPM ++;

                }
            }
            else
            {
                if (lastMat != null)
                {
                    lastMat.SetFloat("_Outline", 0);
                    lastMat = null;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (lightTaken)
            {
                lightEquipped = !lightEquipped;
                lightObject.SetActive(lightEquipped);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (invManager.IsItemEquipped(sphereItem))
            {
                Collider[] colls = Physics.OverlapSphere(invManager.hand.position, 5);
                for (int i = 0; i < colls.Length; i++)
                {
                    Transform coll = colls[i].transform;
                    if (coll.tag == "Energy")
                    {
                        if (Vector3.Distance(coll.position, transform.position) > 1f)
                        {
                            coll.transform.position += (transform.position - coll.transform.position).normalized * 0.1f;
                        }
                        else
                        {
                            sphereGlow += 0.2f;
                            invManager.GetComponentInChildren<Light>().intensity = sphereGlow;
                            Debug.Log("Collected");
                            Destroy(coll.gameObject);
                        }
                    }
                }
            }
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
    IEnumerator Talk(string talkingline)
    {
        txt.gameObject.SetActive(true);
        txt.text = "";
        foreach (var item in talkingline)
        {
            txt.text += item;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2f);
        txt.gameObject.SetActive(false);
    }
    void Move()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;
        Vector3 velocity = (movHorizontal + movVertical).normalized * movementSpeed * Time.deltaTime;
        if (isRunning)
        {
            rb.MovePosition(rb.position + velocity*1.5f);
        }
        else
        {
            rb.MovePosition(rb.position + velocity);
        }
        
        
    }

    IEnumerator PlaySound()
    {
        while (true)
        {
            yield return new WaitForSeconds(isRunning ? 0.2f : 0.5f);
            if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
            {
                auSource.PlayOneShot(walkSound);
            }
        }
    }
    IEnumerator txtcrt;
    float timer=0;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name=="Fibonacci")
        {
            timer += Time.deltaTime;
            if (timer>15)
            {
                txtcrt = Talk(aHint);
                StartCoroutine(txtcrt);
                Destroy(other.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="TalkLine")
        {
            if (txtcrt!=null)
            {
                StopCoroutine(txtcrt);
                txtcrt = null;
            }
            txtcrt = Talk(other.gameObject.GetComponent<TextHold>().line);
            StartCoroutine(txtcrt);
            Destroy(other.gameObject);
        }
        if (other.transform.name == "FinishLevel")
        {
           
            if (gameFinished)
            {
                Debug.LogWarning("FinishLevel");

                 if (killedPM>3)
                {
                    //MonsterEnding
                    endingIndex = 2;
                    SceneManager.LoadScene("FinishingScene");
                }
                else if(!BatteryTaken)
                {
                    //GoodEnding
                    endingIndex = 0;
                    SceneManager.LoadScene("FinishingScene");
                }
                
                else
                {
                    //BadEnding
                    endingIndex = 1;
                    SceneManager.LoadScene("FinishingScene");
                }
            }
        }
        else if (other.transform.name == "FinishLevelTrigger")
        {
            Debug.Log("FinishLevelTrigger");   
            gameFinished = true;
            Destroy(other.gameObject);
        }
    }
}
public enum ItemType { MachinePart, Cable, GearPart, EnergyBall, RealEnergyBall }
public enum State { Controllable, Animation };
public enum ItemVariation { A, B, C, D, F, G, H, J, K, L };

