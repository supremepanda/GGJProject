using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergizingBeam : MonoBehaviour
{

    [SerializeField] Transform point;
  /*  public void CastBeam()
    {
        if (Physics.Raycast(point.pwosition, transform.forward, out RaycastHit hit))
        {
            NodeSlot nSlot = hit.transform.gameObject.GetComponent<NodeSlot>();
            if (nSlot)
            {
                nSlot.ChangeEnergizeState(true);
            }
        }
    }*/
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(point.position, point.position + transform.forward);
    }
}
