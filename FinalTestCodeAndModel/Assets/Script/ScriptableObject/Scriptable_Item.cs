using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType{
    Throwable,Unthrowable,Packed
}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class Scriptable_Item : ScriptableObject {
    
    public static Scriptable_Item _instance;
    public static Scriptable_Item instance{get{return _instance;}}

    public Sprite icon;
    public string itemName;
    public string itemDesciption;

    //if have model delete this line
    //public Mesh model;

    public GameObject Prefap;
    public GameObject model;
    public itemType type;
    //public Vector3 size;
    public float radiusSize;
    [Range(0,10)]public float _item_Weight;
    
    [Header("Packed only")]
    public Item item;

    public virtual Item packitem{get{return item;}}

}


