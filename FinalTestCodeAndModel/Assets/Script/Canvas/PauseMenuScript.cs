using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [Header("Menu")]
    public GameObject PauseMenu;
    public GameObject inventoryMenu;

    [Header("Slider")]
    [SerializeField] private Slider fov;
    [SerializeField] private Slider xCamSpeed;
    [SerializeField] private Slider yCamSpeed;
    [SerializeField] private TextMeshProUGUI fov_text;
    [SerializeField] private TextMeshProUGUI xCamSpeed_text;
    [SerializeField] private TextMeshProUGUI yCamSpeed_text;

    private StartnFinishPoint startnFinish;


    private CamSetting cam;
    private _SceneManager scene;

    private void Awake() 
    {
        scene = _SceneManager.instance;
        //PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<GameObject>();
        PauseMenu.SetActive(false);
        cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<CamSetting>();
        startnFinish = GameObject.FindGameObjectWithTag("StartandFinish").GetComponent<StartnFinishPoint>();
    }
    
    private void Start() 
    {
        sliderDeffaut();
    }

    private void Update() 
    {
        sliderValue();
        
        if(Input.GetKey(KeyCode.Escape) && !startnFinish.hasSendTime)
        {
            PauseGame();     
        }
    }

    private void sliderDeffaut()
    {
        fov.value = 50;
        xCamSpeed.value = 150;
        yCamSpeed.value = 150; 
    }

    private void sliderValue()
    {
        cam.fov = (int)fov.value;
        cam.x_Speed = (int)xCamSpeed.value;
        cam.y_Speed = (int)yCamSpeed.value;

        fov_text.text = fov.value.ToString();
        xCamSpeed_text.text = xCamSpeed.value.ToString();
        yCamSpeed_text.text = yCamSpeed.value.ToString();
    }

    public void restart()
    {
        scene.restartScene();
        Time.timeScale = 1;
    }

    public void exit()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        inventoryMenu.SetActive(false);
        Cursor.visible = true;
    }

    
    public void resumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        inventoryMenu.SetActive(true);
        Cursor.visible = false;
    }
}
