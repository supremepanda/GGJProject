using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
    }
}
