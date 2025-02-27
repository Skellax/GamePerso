using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
     [SerializeField] private float jump;
    private float speed = 5f;

    private float timeForRun = 0.5f;
    [SerializeField] private bool isGrounded = true;

    [SerializeField] private bool isJumped = false;

    private float timeJumped = 1f;

    public LayerMask GroundLayer;
    public BoxCollider2D GroundCollider;


    private float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeForRun = 0.5f;
    }

    public Rigidbody2D GetRB()
    {
        return rb;
    }

    // Update is called once per frame
    void Update()
    {     
        //Function for jump.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.velocity = Vector2.up * jump;
            isJumped = true;
            timeJumped = 0.5f; 
        }
        if (Input.GetButton("Jump") && isJumped) {
            if (timeJumped  > 0) {
                    rb.velocity = Vector2.up * jump;
                    timeJumped -= Time.deltaTime;
            }
            else {
                    isJumped = false;
            }
        }
            if (Input.GetButtonUp("Jump")) {
                    Debug.Log("isJumped = false");
                    isJumped = false;
                }    
    }
        
    

    private void FixedUpdate(){
        
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * horizontalInput, rb.velocity.y);
        HandleRun();
    }

    //function for increase the speed when the character move
    private void HandleRun()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            if(timeForRun > 0)
            {
                speed += 0.5f;
                timeForRun -= Time.deltaTime;

            }
        }
        else if (horizontalInput == 0)
        {
            timeForRun = 0.5f;
            speed = 5f;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(GroundLayer == (1 << other.gameObject.layer))
        {
            isGrounded =  true;
        }

    }
}
