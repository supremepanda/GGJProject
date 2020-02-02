using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSlot : Interactable
{
    [SerializeField] bool[] acceptAndEnergize = new bool[4];
    public Item item = null;
    public bool energized = false;
    public bool energizedThisUpdate = false;
    [SerializeField] bool fixedNode;
    [SerializeField] bool FinalNode;
    [SerializeField] GameObject door;
    [SerializeField]AudioClip clickSound, rotateSound;
    [SerializeField]AudioSource auSource;

    GameObject AddedItem;
    private void Start()
    {
    }
    public bool SetNode(Item _item)
    {
        if (FinalNode) return false;
        if (_item != null)
        {
            if (CheckItem(_item, true)&&item==null)
            {

                item = _item;
                AddedItem=Instantiate(item.itemObject, transform);
                auSource.PlayOneShot(clickSound);
                SetEnergizingSides();
                //  nodesBeam = GetComponentInChildren<EnergizingBeam>();
                return true;

            }
            return false;
        }
        else
        {
            FPSController.instance.invManager.AddItem(item);
            item = null;
            SetEnergizingSides();
            Destroy(AddedItem);
            return false;
        }

    }

    public void RotateNode()
    {
        if (FinalNode) return;
        RotateEnergizingSides();
    }
    public void RotateEnergizingSides()
    {
        if (FinalNode) return;
        //rotateindex++;
        bool[] halo = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            halo[i] = acceptAndEnergize[i];
        }
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("<color=blue>Accept[" + i + "](" + acceptAndEnergize[i] + ")=</color><color=red>halo[" + (i + 3) % 4 + "](" + halo[(i + 3) % 4] + ")</color>");
            acceptAndEnergize[i] = halo[(i + 3) % 4];
        }
        transform.GetChild(1).transform.Rotate(Vector3.up * 90);
        auSource.PlayOneShot(rotateSound);
    }
    public void SetEnergizingSides()
    {
        if (FinalNode) return;
        for (int i = 0; i < 4; i++)
        {
            acceptAndEnergize[i] = false;
        }
        if (item != null)
        {
            if (item.itemVariation == ItemVariation.A)
            {
                acceptAndEnergize[0] = true;
                acceptAndEnergize[2] = true;
            }
            else if (item.itemVariation == ItemVariation.B)
            {
                acceptAndEnergize[3] = true;
                acceptAndEnergize[2] = true;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    acceptAndEnergize[i] = false;
                }
            }
        }
    }
    public void ChangeEnergizeState(bool state, int side)
    {
        if (FinalNode) PuzzleDone();
        
            if (acceptAndEnergize[(side + 2) % 4])
            {
                energized = state;
              //  setColor(Color.blue);
                TestSides();
            }
        
       


    }
    void PuzzleDone()
    {
        Debug.Log("PUZZLE DONE!!");
        BoxCollider[] colls =transform.parent.GetComponentsInChildren<BoxCollider>();
        foreach (var item in colls)
        {
            item.enabled = false;
        }
        door.GetComponent<Animator>().SetBool("isOpened", true);
        door.GetComponent<AudioSource>().enabled=true;

    }
    private void Update()
    {
        if (FinalNode) return;
        energizedThisUpdate = false;
        if (!fixedNode)
        {

            energized = false;

        }
    }
    public void LateUpdate()
    {
        if (FinalNode) return;
        if (energized)
        {
            TestSides();
            //setColor(Color.blue);
        }
        else
        {
            //setColor(Color.white);

        }
    }
   /* public void setColor(Color halob)
    {
        if (FinalNode) return;
        Renderer[] halorr = GetComponentsInChildren<Renderer>();
        foreach (var item in halorr)
        {
            item.material.color = halob;
        }      

    }*/
    public void TestSides()
    {
        if (FinalNode) return;
        if (!energizedThisUpdate)
        {
            energizedThisUpdate = true;
            for (int i = 0; i < 4; i++)
            {
                if (acceptAndEnergize[i])
                {
                    if (Physics.Raycast(transform.position, Quaternion.AngleAxis((i * 90), transform.up) * transform.forward, out RaycastHit hit))
                    {
                        NodeSlot nSlot = hit.transform.gameObject.GetComponent<NodeSlot>();
                        if (nSlot)
                        {
                           
                            nSlot.ChangeEnergizeState(true, i);
                        }
                    }
                }
            }
        }
    }

}
