using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartnFinishPoint : MonoBehaviour
{
    public enum point
    {
        start,finish
    }

    public point _point;
    private ScoreSystem time;
    private GoalScore _Gscore;
    private bool begin = false;
    [HideInInspector]public bool hasSendTime = false;
    //[HideInInspector]public string _name;
    [HideInInspector]public string timeCount;

    private void Awake() 
    {
        time = GameObject.Find("GameManger").GetComponent<ScoreSystem>();  
        _Gscore = time.gameObject.GetComponent<GoalScore>();  
    }

    private void Update() 
    {
        if(begin)
            time.currentTime += Time.deltaTime;
            DisplayTime(time.currentTime);
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeCount = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<PlayerMovement>() && !_Gscore.allMailSend)
        {
            _point = point.start;
            begin = true;
        }
        else if(other.GetComponent<PlayerMovement>() && _Gscore.allMailSend)
        {
            _point = point.finish;
            if(!hasSendTime && _point == point.finish)
            {
                hasSendTime = true;
                _Gscore.activeScore();
            }
            begin = false;
            
        }
    }


}
