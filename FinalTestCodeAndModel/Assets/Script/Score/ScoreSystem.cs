using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem _instance;
    public static ScoreSystem instance{get{return _instance;}}
    public List<Score> _ScoreList;
    public float currentTime; 

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
        
        _ScoreList = new List<Score>();
    }

    public void add(string name,string time)
    {
        Score newPlayer = new Score(name,time); 
        _ScoreList.Add(newPlayer);  
        /*foreach(Score n in _ScoreList)
        {
             
            if(name == n._name)
                n.addTime(time);
            else
            {
                Score newPlayer = new Score(name,time); 
                _ScoreList.Add(newPlayer);           
            }
        }*/
    }
}
