using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSlot : Interactable
{
    [SerializeField]GameObject rightNode, leftNode, upNode, downNode;
    public Item item=null;
    public bool energized = false;
    public bool SetNode(Item _item)
    {
        if (CheckItem(_item, true))
        {
            
                item = _item;
                Instantiate(item.itemObject);
                return true;
            
        }
        return false;
    }
   

}
