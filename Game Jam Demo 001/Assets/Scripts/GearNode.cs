using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearNode : Interactable
{
    public Item item = null;
    [SerializeField] public GearParent parent;
    [SerializeField] public int index;
    public GameObject AddedItem;
    public virtual bool SetNode(Item _item)
    {
        if (_item != null)
        {
            if (CheckItem(_item, true)&&item==null)
            {
                item = _item;
                AddedItem = Instantiate(item.itemObject, transform);
                parent.IsTrue(index, _item.itemVariation);
                return true;
            }
            return false;
        }
        else
        {
            FPSController.instance.invManager.AddItem(item);
            parent.TakeOut(index);
            item = null;
            Destroy(AddedItem);
            return false;
        }

    }
}
