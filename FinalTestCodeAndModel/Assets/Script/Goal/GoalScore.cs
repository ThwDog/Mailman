using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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
    [SerializeField]private TMP_InputField nameInputField;
    [SerializeField]private TMP_InputField idInputField;
    [SerializeField]private TMP_Text con;
    [SerializeField]private int score;

    [Header("Canvas")]
    [SerializeField]private TMP_Text displayTime;
    [SerializeField]private TMP_Text displayCurrnentmailSend;

    private ScoreSystem time;
    private StartnFinishPoint startnFinish;
    private SendPlayerData sendData;    


    private void Awake() 
    {
        sendScoreCanvas.SetActive(false);     
        time = GameObject.Find("GameManger").GetComponent<ScoreSystem>();  
        startnFinish = GameObject.FindGameObjectWithTag("StartandFinish").GetComponent<StartnFinishPoint>();
        sendData = GetComponent<SendPlayerData>();
    }

    public void sendMail()
    {
        currentMailSend++;
    }

    private void Update() 
    {
        allMailSend = currentMailSend >= requirementMail? true : false;   
        timetoscore();
        displayTime.text = startnFinish.timeCount;
        displayCurrnentmailSend.text = $"mail send \t {currentMailSend}/{requirementMail}";
    }

    public void send()
    {
        //add when send go to show sql database 
        int id = Convert.ToInt32(idInputField.text);
        string _time = startnFinish.timeCount;
        string name = nameInputField.text;
        string date_time = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        
        sendData.Send(id,name,_time,date_time,score);
        time.add(name,_time);
    }

    void timetoscore()
    {
        int _time = (int)time.currentTime;
        score = 10000;
        score -= _time * 2;
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
