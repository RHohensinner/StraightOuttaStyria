using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Internal;
using System.Collections;
using System.Collections.Generic;

public class MoveScript : MonoBehaviour
{
 
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGrounded;
    public float jumpForce;
    public float speed;
    Rigidbody2D rb;
    public float distToGround;

    void Start ()
    {
        rb = GetComponent <Rigidbody2D> ();
        distToGround = rb.position.y;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
 
    void FixedUpdate ()
    {
        //if(isPlayerGrounded())
        //{
        //    isGrounded = true;
        //}

        isGrounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
        float x = Input.GetAxis ("Horizontal");
        Vector3 move = new Vector3 (x * speed, rb.velocity.y, 0f);
        rb.velocity = move;
    }

}