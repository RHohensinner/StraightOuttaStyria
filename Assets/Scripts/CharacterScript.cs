using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Internal;
using System.Collections;
using System.Collections.Generic;

public class CharacterScript : MonoBehaviour
{
 
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGrounded;
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

    void Start ()
    {
        rb = GetComponent <Rigidbody2D> ();
        distToGround = rb.position.y;

        InvokeRepeating("UpdateShieldExpiration", 1f, 1f);
    }
 
    //bool IsPlayerGrounded()
    //{
    //    return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1));
    //}
 
    void Update () {
        if (Input.GetButtonDown ("Jump") && isGrounded) {
            rb.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        float y = rb.position.y;

        if(y < -3)
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

        if(Input.GetButtonDown("Right"))
        {
            //Debug.Log("Turning right!");
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        float x = Input.GetAxis ("Horizontal");
        Vector3 move = new Vector3 (x * speed, rb.velocity.y, 0f);
        rb.velocity = move;
    }

    //make sure u replace "floor" with your gameobject name.on which player is standing
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        //Debug.Log(theCollision.gameObject.name);
        LayerMask ground = LayerMask.GetMask("Ground");
        if (theCollision.gameObject.layer == 8)
        {
            isGrounded = true;
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

    void UpdateShieldExpiration()
    {
        if (shieldActive)
            shieldExpirationCounter--;
    }
}