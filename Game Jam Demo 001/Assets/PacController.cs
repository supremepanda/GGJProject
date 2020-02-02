using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PacController : MonoBehaviour
{
    public GameObject target;
    public Transform trg;
    [SerializeField] float force;
    [SerializeField] GameObject DieFx;
    bool dead = false;
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GetComponent<NavMeshAgent>().SetDestination(trg.position);
            GetComponent<Animator>().SetBool("running", true);
        }
        else
        {
            
        }
    }
    public void Kill(Vector3 position)
    {
        if (dead) return;
        dead = true;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Animator>().enabled = false;
        Vector3 offset = transform.position - position;
        if (offset.y < 0)
        {
            offset = new Vector3(offset.x, -offset.y, offset.z).normalized * force;

        }
        GetComponent<Rigidbody>().AddForce(offset, ForceMode.Impulse);
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        Destroy(Instantiate(DieFx,transform.position,Quaternion.identity),5);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
