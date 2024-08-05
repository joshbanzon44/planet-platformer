using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float jumpPower = 1f;


    //References to player components
    Rigidbody2D rb;
    Animator animator;


    //Private function variables
    private float lateralMovement = 0;
    private bool isJumping = false;
    private bool canJump = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get components and set equal to variables
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();



    }

    // Update is called once per frame
    //Get input every frame but apply movement at fixedUpdate
    void Update()
    {
        //Movement input
        lateralMovement = Input.GetAxis("Horizontal");
        isJumping = Input.GetAxis("Jump") > 0 ? true : false;

        //If player falls off map
        if (transform.position.y < -5f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        //Update location based on input
        rb.velocity = new Vector2(lateralMovement * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);

        //Jumping mechanic
        //Only jump if feet on ground and after delay
        if (isJumping && canJump)
        {
            //Check if our feet on ground
            canJump = false;
            Invoke("allowJump", 0.5f);
            Vector3 feetPosition = transform.GetChild(0).position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(feetPosition, 0.02f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject == gameObject)
                    continue; //Move to next loop iteration
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpPower);
                break;  //Stop loop after movement
            }
        }

        //Flip the sprite based on direction of movement
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }


        //Set animator bool if moving to change to walking animation
        if (rb.velocity.magnitude > 0.0001f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //Set animator bool to change jumping animation.
        if (rb.velocity.y > 0.001f)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
        if (rb.velocity.y < -0.001f)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                Debug.Log("Finish object collision");
                string name = SceneManager.GetActiveScene().name;
                Debug.Log(name.Substring(name.IndexOf("L") + 1));
                Debug.Log(name.Substring(0, name.IndexOf("L")));

                int num = 0;
                if (name.Substring(name.IndexOf("L" + 1)) == "3")
                    num = 111;
                else if (name.Substring(name.IndexOf("L" + 1)) == "2")
                    num = 110;
                else
                    num = 100;

                PlayerPrefs.SetInt(name.Substring(0, name.IndexOf("L")), num);
                Invoke("toLevelSelect", 1);
                break;

            default:
                break;
        }
        PlayerPrefs.Save();
    }

    private void toLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    //Use of function to delay jumps
    private void allowJump()
    {
        canJump = true;
    }




}
