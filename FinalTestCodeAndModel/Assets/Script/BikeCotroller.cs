using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeCotroller : MonoBehaviour
{
    public static BikeCotroller _instance;
    public static BikeCotroller instance{get{return _instance;}}

    Rigidbody rb;
    [Header("Controller")]
    public float currentSpeed;
    public bool isSpeedUp;
    [SerializeField]protected bool breakPress;
    [SerializeField]private float breakForce;
    private float currentBreak;

    public float _currentSpeed
    {
        get
        {
            if (isSpeedUp)
                return currentSpeed * 2f;
            return currentSpeed;
        }
        set
        {
            currentSpeed = value;
        }
    }

    [Header("Key Controller")]
    public bool playerOnBike;
    protected Vector2 movement;
    
    [SerializeField]protected KeyCode speedKey; 
    [SerializeField]protected KeyCode breakKey; 

    public bool _playerOnBike
    {
        get
        {
            return playerOnBike;
        }
        set
        {
            playerOnBike = value;
            if(!playerOnBike)
                movement = Vector2.zero;
        }
    }
    
    public bool _speedUp
    {
        get
        {
            return isSpeedUp;
        }
        set
        {
            isSpeedUp = value;
        }
    }

    [Header("Wheel")]
    [SerializeField][Range(0,100)]private float maxSteerAngle;
    [SerializeField]private WheelCollider frontWheel;
    [SerializeField]private WheelCollider BackWheel;
    [SerializeField]private Transform t_frontWheel;
    [SerializeField]private Transform t_BackWheel;
    [SerializeField]private Transform r_Handle;
    [SerializeField]private Transform r_FrontSh;
    [SerializeField]private Transform gear,lF,rF;

    [Header("Cam")]
    private Transform cameraTranform;

    private float currentSteerAngle;

    void Awake() 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cameraTranform = Camera.main.transform;
    }

    void Start()
    {
        
    }

    private void Update() 
    { 
        _speedUp = Input.GetKey(speedKey);
        breakPress = Input.GetKey(breakKey);

        if(playerOnBike)
            movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        currentBreak = breakPress || !playerOnBike ? breakForce : 0f;

        if (movement.sqrMagnitude > 1f)
                movement.Normalize();

        
        biking();
    }

    void biking()
    {
        currentSteerAngle = maxSteerAngle * movement.x;


        frontWheel.motorTorque = movement.y * _currentSpeed;
        BackWheel.motorTorque = movement.y * _currentSpeed;
        
        frontWheel.steerAngle = currentSteerAngle; 

        Vector3 z_Rotation = new Vector3(0,0,currentSteerAngle);

        wheelUpdate(frontWheel,t_frontWheel);
        wheelUpdate(BackWheel,t_BackWheel);
        wheelUpdate(BackWheel,gear);

        //Rotation other Obj
        r_FrontSh.localEulerAngles = z_Rotation;
        r_Handle.localEulerAngles = z_Rotation; 

        ApplyBreaking();
    }
    
    private void ApplyBreaking()
    {
        frontWheel.brakeTorque = currentBreak;
        BackWheel.brakeTorque = currentBreak;
        
    }

    //Copy rote from wheel to target obj
    private void wheelUpdate(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        //wheelTransform.position = pos;
    }


}
