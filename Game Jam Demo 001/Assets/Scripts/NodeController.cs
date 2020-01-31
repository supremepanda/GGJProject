using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    NodeSlot[] nodes;
    private void Start()
    {
        nodes=GetComponentsInChildren<NodeSlot>();
    }
    public void Energize()
    {

    }
}
