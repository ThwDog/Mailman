using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoalScore : MonoBehaviour
{
    public static GoalScore _instance;
    public static GoalScore instance{get{return _instance;}}

    [Header("Mail send setting")]
    [Range(0,100)]public int requirementMail;
    public int currentMailSend;
    public bool allMailSend = false;
    [Header("SendScore")]
    [SerializeField]private GameObject sendScoreCanvas;
    [SerializeField]private TMP_InputField inputField;
    [SerializeField]private Button sendButton;
    [SerializeField]private TMP_Text con;
    private ScoreSystem time;
    private StartnFinishPoint startnFinish;
    

    private void Awake() 
    {
        sendScoreCanvas.SetActive(false);     
        time = GameObject.Find("GameManger").GetComponent<ScoreSystem>();  
        startnFinish = GameObject.FindGameObjectWithTag("StartandFinish").GetComponent<StartnFinishPoint>();
    }

    public void sendMail()
    {
        currentMailSend++;
    }

    private void Update() 
    {
        allMailSend = currentMailSend == requirementMail? true : false;   
    }

    public void send()
    {
        //add when send go to show sql database 
        string _time = startnFinish.timeCount;
        string name = inputField.text;
        time.add(name,_time);
    }

    public void activeScore()
    {
        string _time = startnFinish.timeCount;
        Time.timeScale = 0;
        Cursor.visible = true;
        sendScoreCanvas.SetActive(true);    
        
        con.text = "congratulation you did time\t"+_time;
    }
}
