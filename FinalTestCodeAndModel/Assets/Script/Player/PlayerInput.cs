using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    protected static PlayerInput p_instance; 
    public static PlayerInput instance { get { return p_instance; } }
    protected InputManager p_Input;
    protected Vector2 p_movement;
    protected bool p_jump;
    protected bool p_Fire;
    protected bool p_Focus;
    protected bool p_Run;
    protected bool p_Interact;
    //[HideInInspector]
    public bool inputBlock; 
    //[HideInInspector]
    public bool mouseInputBlock; 

    public Vector2 movement{
        get
        {
            if(inputBlock)
                return Vector2.zero;
            return p_movement;
        }
    }

    

    public bool jump
    {
        get{return p_jump && !inputBlock;}
    }
    public bool fire
    {
        get{return p_Fire && !mouseInputBlock;}
    }
    public bool focus
    {
        get{return p_Focus && !mouseInputBlock;}
    }
    public bool run
    {
        get{return p_Run && !inputBlock;}
    }

    public bool interact
    {
        get{return p_Interact;}
    }

    void Awake()
    {
        if(p_instance != null && p_instance != this){
            Destroy(this.gameObject);
        }
        else{ p_instance = this;}
        p_Input = new InputManager();        
    }

    void OnEnable()
    {
        p_Input.Enable();
    }

    void OnDisable()
    {
        p_Input.Disable();    
    }

    private void Update() 
    {
        p_jump = Input.GetButton("Jump");
        p_movement = p_Input.Player.Walk.ReadValue<Vector2>();
        p_Fire = Input.GetButtonUp("Fire1");
        p_Focus = Input.GetButton("Fire2");
        p_Run = Input.GetKey(KeyCode.LeftShift);    
        p_Interact = Input.GetKeyDown(KeyCode.E);
    }

    private void FixedUpdate() 
    {
    }
    

    public bool HaveControl()
    {
        return !inputBlock;
    }

    public void ReleaseControl()
    {
        inputBlock = true;
    }

    public void GainControl()
    {
        inputBlock = false;
    }

    public bool HaveMouseControl()
    {
        return !mouseInputBlock;
    }

    public void ReleaseMouseControl()
    {
        mouseInputBlock = true;
    }

    public void GainMouseControl()
    {
        mouseInputBlock = false;
    }
}
