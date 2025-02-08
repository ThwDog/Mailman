using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{   
    public static PlayerMovement _instance;
    public static PlayerMovement instance{get{return _instance;}}

    private CharacterController player;

    public bool _lockedMove = false;
    [SerializeField]private bool _dead = false;
    private bool _onBike = false;
    public bool lockedMove
    {
        get
        {
            return _lockedMove;
        }
        set{ _lockedMove = value;}
    }

    public bool dead
    {
        get
        {
            return _dead;
        }
        set{ _dead = value;}
    }

    [Header("Movement")]
    private PlayerInput p_input;
    private bool isGrounded = true;
    private bool isRunning = false;
    private float verticalSpeed;
    private float _currentSpeed;
    [SerializeField]private float forceMagnitude;
    [SerializeField][Range(-100,100)]private float maxSpeed;
    [SerializeField][Range(-100,100)]private float minSpeed;
    [SerializeField][Range(0,100)]public float jumpHeight = 1.0f;
    [SerializeField]private float gravityValue = 20f;
    private float stickingGravityPro = 20f;



    [Header("Cam")]
    private Transform cameraTranform;
    [SerializeField]private CamSetting camSetting;

    [Header("bike")]
    private PlayerOnVechice onVechice;
    
    [Header("Scene")]
    private _SceneManager scene;

    [Header("Audio")]
    private AudioSource walkaudio;

    private float currentSpeed
    {
        get
        {
            return _currentSpeed = isRunning ? maxSpeed : minSpeed;
        }
    }
    
    private void Awake() 
    {
        scene = GameObject.Find("SceneManger").gameObject.GetComponent<_SceneManager>();
        Cursor.visible = false;
        cameraTranform = Camera.main.transform;
        walkaudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        player = GetComponent<CharacterController>();
        p_input = PlayerInput.instance;
        onVechice = GameObject.Find("Bicyle").gameObject.GetComponent<PlayerOnVechice>();

    }

    private void Update() 
    {
        lockMovement();
        playerDead();
    }

    void FixedUpdate()
    {
        move();
        checkGround();
        onBikcycle();
    }

    void move()
    {
        if(p_input.movement.x != 0 || p_input.movement.y != 0)
        {    
            walkaudio.enabled = true;
        }
        else
            walkaudio.enabled = false;
        Vector3 move = new Vector3(p_input.movement.x,0,p_input.movement.y);
        if(move.sqrMagnitude > 1.0f){
            move.Normalize();
        }
        //isRunning = p_input.run;
        
        //rotation
        move = cameraTranform.forward * move.z + cameraTranform.right * move.x;
        move.y = 0f; 
        //Fall
        move += verticalSpeed * Vector3.up * Time.deltaTime;
        //Move
        player.Move(move * currentSpeed);
        
    }


    void checkGround()
    {
        isGrounded = player.isGrounded;
        if (isGrounded)
        {
            verticalSpeed = -gravityValue * stickingGravityPro;
            if (p_input.jump && isGrounded)
            {
                verticalSpeed = jumpHeight;
            }

        }
        else
        {
           verticalSpeed -= gravityValue * Time.deltaTime;
        }
    }

    void lockMovement()
    {

        if(!lockedMove || !dead)
        {
            p_input.GainControl();
            //camSetting.camMoveUpdate();
        }
        else if(lockedMove)
        {
            p_input.ReleaseControl();
        } 
    }

    public void playerDead()
    {
        if(_dead)
        {
            p_input.ReleaseControl();
            p_input.ReleaseMouseControl();
            scene.restartScene();
        } 
    }

    //make player can push item
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.GetComponent<ItemHolder>() == true)
        {
            ItemHolder item = hit.gameObject.GetComponent<ItemHolder>();
            Rigidbody rb = hit.collider.attachedRigidbody;
            Vector3 forceDir = hit.gameObject.transform.position - transform.position;
            forceDir.y = 0;
            forceDir.Normalize();

            rb.AddForceAtPosition(forceDir * forceMagnitude * item._Weight,transform.position,ForceMode.Impulse);
        }   
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject != null && other.gameObject.TryGetComponent<ItemHolder>(out ItemHolder item))
        {
            if(p_input.interact)
                item.collect();
        }  

        if(other.gameObject != null && other.gameObject.TryGetComponent<PlayerOnVechice>(out PlayerOnVechice _onVechice ))
        {
            if(p_input.interact && _onBike == false)
            {
                lockedMove = true;
                _onBike = true;
            }
            else if(p_input.interact && _onBike == true)
            {
                lockedMove = false;
                _onBike = false;
            }
        }   
    }

    public void onBikcycle()
    {
        if(_onBike)
        {
            Physics.IgnoreCollision(player, onVechice.gameObject.GetComponent<Collider>());
            onVechice.hopONBike(gameObject.transform);

            
        }
        else if(!_onBike)
        {
            Physics.IgnoreCollision(player, onVechice.gameObject.GetComponent<Collider>(),false);
            onVechice.offBike();
            //onVechice = null;

        }
    }
}
