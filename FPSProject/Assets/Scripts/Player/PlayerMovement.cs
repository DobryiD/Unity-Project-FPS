using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed;
    public float sprintSpeed;
    public float JumpHeight;
    public AudioClip footstepSound;
    public float GroundDistance = 0.2f;
    public float footsFrequencyWalk = 1f;
    public float footsFrequencyRun = 0.2f;
    public LayerMask Ground;

    private float currentFootsFrequency;
    private Animator anim;
    private Vector3 inputDir;
    private Rigidbody rb;
    private bool isGrounded=false;
    private bool running;
    private float currentSpeed;
    private Transform _groundChecker;
    private PlayerHealth health;
    private AudioSource footSource;
    private float footSoundFrequency = 0f;
    public Vector3 InputDir
    {
        get
        {
            return inputDir;
        }

        set
        {
            inputDir = value;
        }
    }

    public bool Running
    {
        get
        {
            return running;
        }

        set
        {
            running = value;
        }
    }


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        _groundChecker = transform;
        health = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        footSource = GetComponent<AudioSource>();

    }

    
    // Update is called once per frame
    void Update () {

        if (health.GetCurrentHealth() <= 0) {
            return;
        }
        isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        
        if (isGrounded)
        {
            
            inputDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            running = Input.GetKey(KeyCode.LeftShift)&& Input.GetAxis("Vertical") != 0 && GetComponent<Aiming>().aimingShoot==false;
                     
            inputDir = transform.TransformDirection(inputDir);

            currentSpeed = (running ? sprintSpeed : walkSpeed) * inputDir.sqrMagnitude;

            if (inputDir != Vector3.zero)
            {
                footSoundFrequency += Time.deltaTime;
            }
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            footsSFX();
            HandAnimation();
            WalkAnimation();
        }
        

    }
    void footsSFX() {
        
        if (running)
        {
            currentFootsFrequency = footsFrequencyRun;
        }
        else
        {
            currentFootsFrequency = footsFrequencyWalk;
        }
        while (footSoundFrequency >= currentFootsFrequency)
        {
            footSoundFrequency = 0f;

            playFootstepSound();
        }
    }

    void playFootstepSound() {
        footSource.PlayOneShot(footstepSound);
    }
    void WalkAnimation() {
        float animationSpeed = (running ? 1f : 0.5f) * inputDir.magnitude;
        animationSpeed = Input.GetAxis("Vertical") < 0f ? animationSpeed * Input.GetAxis("Vertical") :
            Input.GetAxis("Vertical") != 0 ? animationSpeed : 0f;


        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", animationSpeed);
    }
    void HandAnimation()
    {
        if (GameObject.FindGameObjectWithTag("Pistol"))
        {
            anim.SetBool("PistolArmed", true);
            anim.SetBool("RifleArmed", false);
            anim.SetBool("GunUp", running ? false : true);
        }
        else if (GameObject.FindGameObjectWithTag("Rifle"))
        {
            anim.SetBool("RifleArmed", true);
            anim.SetBool("PistolArmed", false);
            anim.SetBool("GunUp", running ? false : true);
        }
        else
        {
            anim.SetBool("PistolArmed", false);
            anim.SetBool("RifleArmed", false);
        }
    }

    void FixedUpdate()
    {

        if (running)
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, sprintSpeed);
        else
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, walkSpeed);
        rb.MovePosition(rb.position + inputDir * currentSpeed  * Time.fixedDeltaTime);
   
    }
    void Jump()
    {
       rb.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2 * Physics.gravity.y), ForceMode.VelocityChange);
        
    }
}
