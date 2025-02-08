using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class ThrowItemSystem : MonoBehaviour
{
    [Header("ThrowItem")]
    private float item_Weight;
    public float throw_power; //Throw power
    public float throw_Up_power; //Throw up power
    public float coolDown;
    private PlayerInput p_input;
    protected bool readyToThrow;

    [Header("Cam")]
    [SerializeField]private Transform cameraTranform;

    public Transform throw_Point;
    public Transform handPoint;
    
    [Header("Obj")]
    //private GameObject throw_Obj;
    [SerializeField]private int currentObjNum;
    [SerializeField]private Scriptable_Item _Item;
    [SerializeField]private InventorySystem inventory;

    private SlotCanvas slot;
    public float _Throw_p
    {
        get
        {
            return throw_power;
        }
        set
        {
            throw_power = value;
        }
    }

    public float _Weight
    {
        get
        {
            return _Item._item_Weight;
        }
    }

    private void Awake() 
    {
        cameraTranform = Camera.main.transform;
    }
    private void Start() 
    {
        readyToThrow = true;
        p_input = PlayerInput.instance;
        _Item = Scriptable_Item.instance;    
        inventory = GameObject.Find("InventoryManager").gameObject.GetComponent<InventorySystem>();
        
    }

    private void FixedUpdate() 
    {
        p_Throw();
    }
    
    private void Update() 
    {
        insertobject();
        if(_Item != null)
            spawnItemOnhand(_Item.model);

    }

    private void insertobject()
    {
        var keyCode = Input.inputString;
        int num;
        int.TryParse(keyCode,out num);
        num -= 1;
        //Select obj in list
        switch(keyCode)
        {
            case "1":
                set(num);
                break;
            case "2":
                set(num);
                break;
            case "3":
                set(num);
                break;
            case "4":
                set(num);
                break;
            case "5":
                set(num);
                break;
            case "6":
                set(num);
                break;
            case "7":
                set(num);
                break;
            case "8":
                set(num);
                break;
            case "9":
                set(num);
                break;
            default:
                /*_Item = null;
                currentObjNum = 0;*/
                break;
        }
    }    

    private void set(int num)
    {
        _Item = inventory.inventory[num].data;
        currentObjNum = inventory.inventory[num].stackSize;
        //setBG(num);
    }

    /*private void setBG(int num)
    {   
        if(slot == null)
        {
            slot = GameObject.FindGameObjectWithTag("ItemSlot").GetComponent<SlotCanvas>();
            int slotNum = Convert.ToInt32(slot.m_SlotNum);
            if(slotNum == num)
            {
                slot.selectNUMBG(num);
            }
            else
                slot = null;
        }
        
                
    }*/

    private void throw_Set(GameObject obj)
    {

        Vector3 throw_pos = new Vector3(throw_Point.position.x,throw_Point.position.y,throw_Point.position.z);
        GameObject throwObj = Instantiate(obj,throw_pos,cameraTranform.rotation);

        if(!throwObj.GetComponent<Rigidbody>())
            throwObj.AddComponent<Rigidbody>();
    
        Rigidbody throwObj_Rb = throwObj.GetComponent<Rigidbody>();

        Vector3 force = cameraTranform.forward * throw_power + transform.up * throw_Up_power;

        throwObj_Rb.AddForce(force,ForceMode.Impulse);

    }

    IEnumerator _throw(GameObject obj)
    {
        readyToThrow = false;
        
        throw_Set(obj);
        currentObjNum--;
        yield return new WaitForSeconds(coolDown);

    }

    private void p_Throw()
    {
        if(p_input.focus)
        {
            if(p_input.fire && readyToThrow && currentObjNum > 0)
            {
                StartCoroutine(_throw(_Item.Prefap));
                inventory.remove(_Item,1);

                readyToThrow = true;
            }
        }
    }

    private void spawnItemOnhand(GameObject obj)
    {
        if(_Item == null)
            return;
        MeshFilter objFilter = handPoint.GetComponent<MeshFilter>();
        MeshRenderer objRen = handPoint.GetComponent<MeshRenderer>();
        if(currentObjNum == 0)
        {
            objFilter.mesh = null;
            objRen.material = null;
        }
        else
        {
            MeshFilter model = (MeshFilter)obj.GetComponent("MeshFilter");
            MeshRenderer renderer = (MeshRenderer)obj.GetComponent("MeshRenderer");
            objFilter.mesh = model.sharedMesh;
            objRen.material = renderer.sharedMaterial;
        }
    }
}
