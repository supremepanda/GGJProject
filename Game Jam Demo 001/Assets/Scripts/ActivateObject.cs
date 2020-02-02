using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject :Interactable
{
    [SerializeField] GameObject item;
    
    public bool Activate(Item _item)
    {
        Debug.Log("Checking" + _item)
   ;        if (CheckItem(_item))
        {
            item.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            FinishGame();
            return true;
        }
        return false;
    }
    public void FinishGame()
    {
        Debug.Log("Game Finished");
    }
   
}
