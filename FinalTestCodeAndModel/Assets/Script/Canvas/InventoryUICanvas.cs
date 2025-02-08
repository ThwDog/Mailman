using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUICanvas : MonoBehaviour
{
    public static InventoryUICanvas _instance;
    public static InventoryUICanvas instance{get{return _instance;}}

    [SerializeField]
    private GameObject SlotPrefap;

    private void Start() 
    {

    }

    public void updateInventory()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        drawInventory();
    }

    public void drawInventory()
    {
        foreach(InventoryItem item in InventorySystem.instance.inventory)
        {
            addInventorySlot(item);
        }
    }

    public void addInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(SlotPrefap);
        obj.transform.SetParent(transform,false);
        
        SlotCanvas slot = obj.GetComponent<SlotCanvas>();
        slot.set(item);
    }

}
