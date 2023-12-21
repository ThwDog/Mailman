using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool mailHasSend = false;

    private GoalScore _GScore;
   

    private void Awake() 
    {
        _GScore = GameObject.Find("GameManger").GetComponent<GoalScore>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!mailHasSend && other.gameObject.TryGetComponent<ItemHolder>(out ItemHolder item))
        {
            if(item.item.itemName == "mail")
            {
                mailHasSend = true;
                _GScore.sendMail();
                Destroy(other.gameObject);
                gameObject.active = false;
            }
        }        
    }
}
