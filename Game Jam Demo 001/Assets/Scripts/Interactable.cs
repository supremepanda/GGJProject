using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public ItemType acceptedItemType;
    [SerializeField] public ItemVariation acceptedItem;
    public bool CheckItem(Item _item,bool acceptsVariations)
    {
        if (_item != null)
        {
            if (acceptedItemType == _item.itemType)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckItem(Item _item)
    {
        if (_item != null)
        {
            if (acceptedItemType == _item.itemType && acceptedItem == _item.itemVariation)
            {
                return true;
            }
        }
        return false;
    }
}
