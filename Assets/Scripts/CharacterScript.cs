using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Internal;
using System.Collections;
using System.Collections.Generic;

public class CharacterScript : MonoBehaviour
{
 
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool balloon;
    public bool isGrounded;
    public bool onWall;
    public bool firePickedUp;
    public bool fireActive;
    public bool potionPickedUp;
    public bool potionActive;
    public bool shieldPickedUp;
    public bool shieldActive;
    public float jumpForce;
    public float speed;
    Rigidbody2D rb;
    public float distToGround;

    public int shieldDamageCounter;
    public int shieldExpirationCounter;
    public int fireCounter;

    private AudioSource audio;
    public AudioClip audio_jump;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float wallJumpForce;

    private float slideSpeed = 3f;
    private float climbSpeed = 5f;

    private float movementDirection;

    private bool wallGrab = false;

    public Vector2 wallJumpDirection;

    void Start ()
    {
        rb = GetComponent <Rigidbody2D> ();
        distToGround = rb.position.y;

        InvokeRepeating("UpdateShieldExpiration", 1f, 1f);
        wallJumpDirection.Normalize();
    }
 
    //bool IsPlayerGrounded()
    //{
    //    return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1));
    //}
 
    void Update () {

        if (Input.GetButtonDown ("Jump") && isGrounded && !balloon) {
            rb.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(audio_jump);
        }
        else if (Input.GetButtonDown("Jump") && onWall && !balloon)
        {
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        
        
        
        float y = rb.position.y;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        wallGrab = onWall && Input.GetKey(KeyCode.LeftShift);

        if (wallGrab && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
        }

        if (onWall && !isGrounded)
        {
            if(!wallGrab)
                WallSlide();
        }

        if (y < -3)
        {
            Destroy(gameObject);
        }

        if(Input.GetButtonDown("Potion") && potionPickedUp)
        {
            Debug.Log("Potion used!");
            HealthScript health = GetComponent<HealthScript>();
            if(health != null)
            {
                health.hp = health.maxHealth;
                health.healthBar.SetSize(1f);
            }
            
            potionPickedUp = false;
        }

        if(Input.GetButtonDown("Shield") && shieldPickedUp)
        {
            Debug.Log("Shield used!");
            shieldActive = true;
            shieldDamageCounter = 3;
            shieldExpirationCounter = 10;
            shieldPickedUp = false;
        }

        if(shieldActive && shieldExpirationCounter == 0)
        {
            Debug.Log("Shield expired!");
            shieldActive = false;
            shieldDamageCounter = 0;
        }
    }
 
    void FixedUpdate ()
    {
        //isGrounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
        if(Input.GetButtonDown("Left"))
        {
            //Debug.Log("Turning left!");
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetButtonDown("Right"))
        {
            //Debug.Log("Turning right!");
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (balloon && Input.GetButton("Vertical"))
        {
            movementDirection = Input.GetAxis("Horizontal");
            float movementDirectionY = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(movementDirection * speed, movementDirectionY * speed, 0f);
            rb.velocity = move;
        }
        else
        {
            movementDirection = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(movementDirection * speed, rb.velocity.y, 0f);
            rb.velocity = move;
        }

        

        
    }

    //make sure u replace "floor" with your gameobject name.on which player is standing
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        //Debug.Log(theCollision.gameObject.name);
        LayerMask ground = LayerMask.GetMask("Ground");
        if (theCollision.gameObject.layer == 8 && !balloon)
        {
            isGrounded = true;
        }

        if(theCollision.gameObject.layer == 9 && !balloon)
        {
            onWall = true;
        }

        if (theCollision.gameObject.layer == 11)
        {
            // fire item
            Debug.Log("Fire picked up!");
            firePickedUp = true;
            //fireActive = true;
            fireCounter = 5;
        }
        if (theCollision.gameObject.layer == 12)
        {
            // potion item
            Debug.Log("Potion picked up!");
            potionPickedUp = true;
            //potionActive = true;
        }
        if (theCollision.gameObject.layer == 13)
        {
            // shield item
            Debug.Log("Shield picked up!");
            shieldPickedUp = true;
            //shieldActive = true;
        }
    }

    void OnCollisionExit2D(Collision2D theCollision)
    {
        if(theCollision.gameObject.layer == 9 && !balloon)
        {
            onWall = false;
        }
    }

    void UpdateShieldExpiration()
    {
        if (shieldActive)
            shieldExpirationCounter--;
    }

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
    }
}