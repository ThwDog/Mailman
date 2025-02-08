using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CamSetting : MonoBehaviour
{
    public static CamSetting _instance;
    public static CamSetting instance{get{return _instance;}}

    public enum Inputchoice{
        keyboard,controller,
    }
    public Inputchoice input;

    public Transform follow;
    public Transform lookAt;
    
    [Header("CamSetting")]
    [SerializeField]public float fov = 60f;
    [SerializeField][Range(0,3000)]public float x_Speed;
    [SerializeField][Range(0,3000)]public float y_Speed;

    public CinemachineVirtualCamera keyboardCam;
    public CinemachineVirtualCamera controllerCam;
    public CinemachineVirtualCamera Current
        {
            get { return input == Inputchoice.keyboard ? keyboardCam : controllerCam; }
        }

    public bool allowRuntimeCameraSettingsChanges;

    void Reset()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null && player.name == "Player")
        {
            follow = player.transform;

            lookAt = follow.Find("HeadTarget");
        }
    }

    private void Awake() {
        UpdateCameraSettings();
    }

    void Update()
    {
        if(allowRuntimeCameraSettingsChanges){
            UpdateCameraSettings();
        }
    }

    void UpdateCameraSettings()
    {
        camMoveUpdate(x_Speed,y_Speed);

        keyboardCam.Follow = follow;
        controllerCam.Follow = follow;
        keyboardCam.m_Lens.FieldOfView = fov;
        controllerCam.m_Lens.FieldOfView = fov;


        if(lookAt != null)
            keyboardCam.LookAt = lookAt;    
            controllerCam.LookAt = lookAt;    
    
        keyboardCam.Priority = input == Inputchoice.keyboard ? 1:0 ;
        controllerCam.Priority = input == Inputchoice.controller ? 1:0 ;
    }

    public void camMoveUpdate(float x_Speed,float y_Speed)
    {
        CinemachinePOV keycam = keyboardCam.GetCinemachineComponent<CinemachinePOV>();
        CinemachinePOV conCam = controllerCam.GetCinemachineComponent<CinemachinePOV>();

        keycam.m_HorizontalAxis.m_MaxSpeed = x_Speed;
        keycam.m_VerticalAxis.m_MaxSpeed = y_Speed;
        conCam.m_HorizontalAxis.m_MaxSpeed = x_Speed;
        conCam.m_VerticalAxis.m_MaxSpeed = y_Speed;
    }
}
