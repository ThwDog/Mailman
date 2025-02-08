using System;
using System.Transactions;

[Serializable]
public class InventoryItem 
{
    public Scriptable_Item data;
    public int stackSize;

    public InventoryItem(Scriptable_Item source,int amount)
    {
        data = source;
        //doesnt work must add item before play game 
        //int amount = data.type == itemType.Packed ? data.packitem.amountItemInPack : 1;
        addToStack(amount);
    }

    public void addToStack(int amount)
    {
        stackSize += amount;
    }
    
    public void removeFromStack(int amount)
    {
        stackSize -= amount;
    }
}
