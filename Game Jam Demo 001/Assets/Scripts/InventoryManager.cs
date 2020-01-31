using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    Item[] inventorySlots;
    public Item selectedItem;
    int selectedSlot;
    [SerializeField] GameObject uiParent;
    Image[] uiImages;
    [SerializeField] Color highlightColor,normalColor;
    private void Start()
    {
        uiImages = new Image[uiParent.transform.childCount];
        for (int i = 0; i < uiImages.Length; i++)
        {
            uiImages[i] = uiParent.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
        inventorySlots = new Item[10];
    }
    public void AddItem(Item _item)
    {
        inventorySlots[LookForFreeSlot()] = _item;
        UpdateInventoryUI();
    }
    public void RemoveItem()
    {
        inventorySlots[selectedSlot]=null;
        HighlightSlot(-1);
        UpdateInventoryUI();
    }
    public int LookForFreeSlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public void SelectItem(int _index)
    {
        selectedSlot = _index;
        selectedItem = inventorySlots[_index];
        HighlightSlot(_index);
    }
    Image lastImage;
    void HighlightSlot(int _index)
    {
        if (lastImage!=null)
        {
            lastImage.color =normalColor;
        }
        if (_index>=0)
        {
            lastImage = uiImages[_index].transform.GetChild(0).GetComponent<Image>();
            lastImage.color = highlightColor;
        }
        else
        {
            lastImage = null;

        }
    }
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i]!=null)
            {
                uiImages[i].sprite = inventorySlots[i].itemImage;
                uiImages[i].enabled = true;
            }
            else
            {
                uiImages[i].enabled = false;
            }

        }
    }

}
