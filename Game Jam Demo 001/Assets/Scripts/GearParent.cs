using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearParent : MonoBehaviour
{
    [SerializeField]public ItemVariation[] combination = new ItemVariation[8];
    [SerializeField]public bool[] isTruela = new bool[8];
    [SerializeField]public GameObject door;
    [SerializeField]public GameObject energyLight;
    public bool IsTrue(int index,ItemVariation variation)
    {
        isTruela[index] = combination[index] == variation;
        Check();
        return isTruela[index];
    }
    public void TakeOut(int index)
    {
        isTruela[index] = false;
    }
    public void Check()
    {
        for (int i = 0; i < combination.Length; i++)
        {
            if (combination!=null)
            {
                if (!isTruela[i]) return;
            }
        }
        PuzzleDone();
    }
    public void PuzzleDone()
    {
        Debug.Log("PUZZLE DONE!!");
        BoxCollider[] colls = GetComponentsInChildren<BoxCollider>();
        foreach (var item in colls)
        {
            item.enabled = false;
        }
        if (energyLight!=null)
        {
            energyLight.GetComponent<SphereCollider>().enabled = true;
            energyLight.GetComponent<Rigidbody>().isKinematic = false; 
        }
        
        door.GetComponent<Animator>().SetBool("isOpened",true);
        door.GetComponent<AudioSource>().Play();
    }
}
