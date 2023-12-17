using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem _instance;
    public static InventorySystem instance{get{return _instance;}}

    private Dictionary<Scriptable_Item,InventoryItem> itemDic;

    
    public List<InventoryItem> inventory;
    [SerializeField]
    private InventoryUICanvas inventoryUI;

    private void Awake() 
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != null)
        {
            Destroy(gameObject);
        }

        inventory = new List<InventoryItem>();
        itemDic = new Dictionary<Scriptable_Item,InventoryItem>();
        inventoryUI = GameObject.Find("InventoryBar").GetComponent<InventoryUICanvas>();
    }

    public InventoryItem get(Scriptable_Item refer)
    {
        if(itemDic.TryGetValue(refer,out InventoryItem value)) 
        {
            return value;
        }
        return null;
    }

    public void add(Scriptable_Item refer,int amount)
    {
        if(itemDic.TryGetValue(refer,out InventoryItem value))
        {
            //amount = value.data.type == itemType.Packed ? value.data.packitem.amountItemInPack : 1;
            value.addToStack(amount);
        }
        else
        {
            InventoryItem newitem = new InventoryItem(refer,amount);
            inventory.Add(newitem);
            itemDic.Add(refer,newitem);
        }
        inventoryUI.updateInventory();
    }


    public void remove(Scriptable_Item refer,int amount)
    {
        if(itemDic.TryGetValue(refer, out InventoryItem value))
        {
            value.removeFromStack(amount);
            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDic.Remove(refer);
            }
        }
        inventoryUI.updateInventory();
    }
}
