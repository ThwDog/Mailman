using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Item 
{
    [Header("PackSetting")]
    public int amountItemInPack;
    public Scriptable_Item itemPack;

}