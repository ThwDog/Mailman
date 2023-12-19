using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Unity.VisualScripting;
using System;
using TMPro;

public class SendPlayerData : MonoBehaviour
{
    public static SendPlayerData _instance;
    public static SendPlayerData instance{get{return _instance;}}
    string getApiUrl = "http://127.0.0.1:8000/get_player_data/";
    string updateApiUrl = "http://127.0.0.1:8000/update_player_data/";
    public TextMeshProUGUI scoreDisplay; 

    void Start()
    {
        //StartCoroutine(SendDataRoutine());
    }

    // Routine to continuously send data
    /*public IEnumerator SendDataRoutine(string name,string time,string date,int Score)
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Adjust the time as needed
            Send(name,time, date,Score);
            Debug.Log("send");
        }
    }*/

    public void Send(int id,string name,string time,string date,int Score)
    {
        PlayerData data = new PlayerData
        {
            PlayerID = id, // Set your player ID here
            Name = name,
            TimeCount = time,
            RealDate = date,
            Score = Score,
        };

        StartCoroutine(Post(updateApiUrl, JsonUtility.ToJson(data)));
    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }

     public IEnumerator GetHighScores()
    {
        string url = getApiUrl;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                scoreDisplay.text = "Failed to load high scores.";
            }
            else
            {
                List<PlayerData> data = JsonUtility.FromJson<Wrapper>("{\"items\":" + request.downloadHandler.text + "}").items;
                DisplayScores(data);
            }
        }
    }

    private void DisplayScores(List<PlayerData> data)
    {
        scoreDisplay.text = "";
        foreach (PlayerData _data in data)
        {
            scoreDisplay.text += $"{_data.PlayerID} - {_data.Name} - {_data.TimeCount} - {_data.RealDate} - {_data.Score}\n";
        }
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<PlayerData> items;
    }
}



