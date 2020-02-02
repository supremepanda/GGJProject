using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : GearNode
{
    public GameObject energyLight;
    public Color mycolor;
    public AudioSource ausource;
    public AudioClip clickSound;
    private void Start()
    {
        energyLight.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", Color.black);
        energyLight.GetComponent<Renderer>().sharedMaterial.color = Color.black;
    }
    public override bool SetNode(Item _item)
    {
        if (_item != null)
        {
            if (CheckItem(_item, true) && item == null)
            {
                item = _item;
                AddedItem = Instantiate(item.itemObject, transform);
                ausource.PlayOneShot(clickSound);
                Debug.Log("Rotating" + CheckRotation() + "Degrees");
                AddedItem.transform.Rotate(Vector3.forward * CheckRotation());
               if( parent.IsTrue(index, _item.itemVariation))
                {
                    energyLight.GetComponent<Light>().intensity += 0.5f;
                    Material shMat = energyLight.GetComponent<Renderer>().sharedMaterial;

                    Color color = (mycolor+ shMat.color) * Mathf.LinearToGammaSpace(1);
                    shMat.color += mycolor;
                    shMat.SetColor("_EmissionColor",color);
                    Debug.Log(shMat.color.r);
                    
                }
                return true;
            }
            return false;
        }
        else
        {

            FPSController.instance.invManager.AddItem(item);
            parent.TakeOut(index);
            energyLight.GetComponent<Light>().intensity -= 0.5f;
            Material shMat = energyLight.GetComponent<Renderer>().sharedMaterial;

            
                Color color = (mycolor - shMat.color) * Mathf.LinearToGammaSpace(1);
            Debug.Log(shMat.color.r+"   "+shMat.color.g+"   "+shMat.color.b);
            if (shMat.color.r>= mycolor.r && shMat.color.g >= mycolor.g && shMat.color.b >= mycolor.b)
            {
                shMat.color -= mycolor;
                shMat.SetColor("_EmissionColor", color); 
            }
            Debug.Log(shMat.color.r);
            item = null;
            Destroy(AddedItem);
            return false;
        }

    }
    float CheckRotation()
    {
        switch (index)
        {
            case 0: return -180;
            case 1: return 0;
            case 2: return -60;
            case 3: return -300;
            case 4: return -120;
            case 5: return -240;
            default:return 0;
        }
    }
}
