using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    private PlayerMovement player;
    [Header("Enemy")]
    private Rigidbody rb;
    [SerializeField]private float speed;
    private float currentSpeed;
    public bool startChasing = false;
    public bool startLookAt = true;
    
    private _SceneManager scene;
    [SerializeField]private Animator anim;
    private AudioSource _audio;

    public float _currentSpeed
    {
        get
        {
            return currentSpeed = Vector3.Distance(transform.position,player.transform.position) < 5F ? speed / 1.5f : speed;
        }
    }

    /*public bool _startChasing
    {
        get
        {
            return startChasing = Vector3.Distance(transform.position,player.transform.position) <= 2f ? false : true;
        }
    }*/

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        scene = GameObject.Find("SceneManger").GetComponent<_SceneManager>();
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate() 
    {
        chase();
    }

    private void chase()
    {
        if(startLookAt)
        {
            Vector3 playerpos = new Vector3(player.transform.position.x,gameObject.transform.position.y,player.transform.position.z);
            transform.LookAt(playerpos);
            if(startChasing)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerpos, _currentSpeed *Time.deltaTime);
                anim.SetBool("Chasing",true);
                _audio.enabled = true;
            }
        }
        else if(!startChasing && !startLookAt)  
        {    
            anim.SetBool("Chasing",false);
            _audio.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(startChasing && other.gameObject.GetComponent<PlayerMovement>())
        {
            scene.restartScene();
        }
    }

    public void setchasing()
    {
        startChasing = true;
        startLookAt = true;
    }

    public void stopchasing()
    {
        startChasing = false;
        startLookAt = false;
    }
}
