using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class SlotCanvas : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;
    [SerializeField]
    private TextMeshProUGUI m_Label;
    [SerializeField]
    private GameObject m_stackObj;
    [SerializeField]
    private TextMeshProUGUI m_Stacklabel;
    [SerializeField]
    public TextMeshProUGUI m_SlotNum;
    [SerializeField]
    private Image m_slotNumBG;

    [Header("color")]
    [SerializeField]
    private Color selectColor;
    [SerializeField]
    private Color defaultColor;


    public void set(InventoryItem item)
    {
        m_icon.sprite = item.data.icon;
        m_Label.text = item.data.name;
        if(item.stackSize < 1)
        {
            m_stackObj.SetActive(false);
            return;
        }
        m_Stacklabel.text = item.stackSize.ToString();
        int slotnum = InventorySystem.instance.inventory.IndexOf(item);
        slotnum++;
        m_SlotNum.text = slotnum.ToString();
    }

    public void selectNUMBG(int numKey)
    {
        int slotNum = Convert.ToInt32(m_SlotNum);
        if(numKey == slotNum)
            m_slotNumBG.color = selectColor;
        else 
            m_slotNumBG.color = defaultColor;
    }
 
}
