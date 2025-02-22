using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jump;
    [SerializeField] private float speed;
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
    }

    public Rigidbody2D GetRB()
    {
        return rb;
    }

    // Update is called once per frame
    void Update()
    {     
        //Function gerant le saut du personnage.
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
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(GroundLayer == (1 << other.gameObject.layer))
        {
            isGrounded =  true;
        }

    }
}
