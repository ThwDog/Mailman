using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/PackItem")]
public class Scriptable_Pack : Scriptable_Item
{
    public override Item packitem{get{return item;}}

}
