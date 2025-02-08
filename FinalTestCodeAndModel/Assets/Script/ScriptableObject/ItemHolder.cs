using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit3D;
using Unity.VisualScripting;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Scriptable_Item item;
    public bool canupdate;
    public InventorySystem inventory;
    [SerializeField]private DialogueCanvasController dialogue;

    public float _Weight
    {
        get
        {
            return item._item_Weight;
        }
    }


    private void Awake() 
    {
        updateItem();
    }

    

    private void Start() {
        inventory = GameObject.Find("InventoryManager").gameObject.GetComponent<InventorySystem>();
        dialogue = GameObject.Find("DialogueCanvas").gameObject.GetComponent<DialogueCanvasController>();
        colliderSetting();
    }

    private void FixedUpdate() 
    {
        if(canupdate)
            updateItem();       
    }

    void updateItem()
    {   
        //if have model delete this line
        //this
        //MeshFilter meshObj = GetComponent<MeshFilter>();
        //if(item.model != null)
            //meshObj.mesh = item.model;
        //to this

        //meshObj.gameObject.transform.localScale = item.size;
        if(gameObject.GetComponent<Rigidbody>() == false)
            gameObject.AddComponent<Rigidbody>();
    }

    void colliderSetting()
    {
        if(gameObject.GetComponent<Collider>() == false)
            gameObject.AddComponent<MeshCollider>();
        
        gameObject.AddComponent<SphereCollider>();
        
        if(gameObject.GetComponent<MeshCollider>())
        {
            MeshCollider collider = GetComponent<MeshCollider>();
            collider.convex = true;
        }

        SphereCollider sphere = GetComponent<SphereCollider>();


        sphere.isTrigger = true;
        sphere.center = new Vector3(0,0,0);
        sphere.radius = item.radiusSize;
        
    }

    public void collect()
    {

        int amount = item.type == itemType.Packed ? item.packitem.amountItemInPack : 1;

        Scriptable_Item _Item  = item.type == itemType.Packed ? item.packitem.itemPack : item;
        inventory.add(_Item,amount);
        //inventory.add(_Item);

        dialogue.DeactivateCanvasWithDelay(0.1f);
        Destroy(gameObject);
        
    }
}
