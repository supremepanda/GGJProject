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
    [SerializeField] public Transform hand;
    [SerializeField]AudioClip takeSound;
    [SerializeField]AudioSource auSource;

    private void Start()
    {
        uiImages = new Image[uiParent.transform.childCount];
        for (int i = 0; i < uiImages.Length; i++)
        {
            uiImages[i] = uiParent.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
        inventorySlots = new Item[10];
    }
    public bool IsItemEquipped(Item _item)
    {
        return selectedItem == _item;
    }
    public void AddItem(Item _item)
    {
        if (_item.itemType!=ItemType.EnergyBall)
        {
            inventorySlots[LookForFreeSlot()] = _item;
            auSource.PlayOneShot(takeSound);
            UpdateInventoryUI();
        }
        if (_item == FPSController.instance.sphereItem)
        {
            FPSController.instance.BatteryTaken = true;
        }
        else
        {
            FPSController.instance.lightTaken=true;
        }
    }
    public void RemoveItem()
    {
        inventorySlots[selectedSlot]=null;
        selectedItem = null;
        HighlightSlot(-1);
        UpdateInventoryUI();
        if (holdingObject != null)
        {
            Destroy(holdingObject);
        }
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
    GameObject holdingObject;
    public void SelectItem(int _index)
    {
        selectedSlot = _index;
        selectedItem = inventorySlots[_index];
        HighlightSlot(_index);
        if (holdingObject!=null)
        {
            Destroy(holdingObject);
        }
        if (selectedItem != null)
        {
            holdingObject = Instantiate(selectedItem.itemObject, hand); 
        }
        
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
