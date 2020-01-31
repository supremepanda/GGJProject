using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject :Interactable
{
    [SerializeField] GameObject item;
    
    public bool Activate(Item _item)
    {
        if (CheckItem(_item))
        {
            item.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            return true;
        }
        return false;
    }
   
}
