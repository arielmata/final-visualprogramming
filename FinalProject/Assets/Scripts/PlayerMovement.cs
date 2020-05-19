using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public AudioClip jumpClip;
    public AudioClip grenadeClip;
    public AudioClip gameOverClip;
    public AudioClip weaponShootClip;

    public GameObject bullet;
    public float runSpeed = 25f;
    public bool hasGrenade = false;

    public float dyingTimeMax = 0.11f;

    float horizontalMove = 0f; // Default horizontal speed is zero
    bool jumpFlag = false;
    bool jump = false;

    public bool finishedGame = false;
    public int health = 0;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false; // jumpFlag is triggered, so now we turn it off
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Pressed space/jump button.");
            if (animator.GetBool("IsJumping") == false)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position); // Play jumpClip
                jump = true;
                animator.SetBool("IsJumping", true); 
            }
        }

        Shoot();

        if (health >= 3)
        {
            // if player is shot 3 times
            animator.SetBool("IsDead", true); // play dying animation
            Destroy(this.gameObject, dyingTimeMax);

        }
    }

    void Shoot()
    {
        // Need to check when weapon is shot
        if (Input.GetMouseButtonDown(0))
        {
            // If primary object is pressed, create object at current position 
            // and create instance of bullet
            Debug.Log("Pressed primary button.");
            if (!controller.m_FacingRight)
            {
                GameObject.Instantiate(bullet, transform.position, transform.rotation);
            }
            else
            {
                bullet.GetComponent<BulletController>().speed *= -1;
                GameObject.Instantiate(bullet, transform.position, transform.rotation);
                bullet.GetComponent<BulletController>().speed *= -1;
            }
            
            AudioSource.PlayClipAtPoint(weaponShootClip, transform.position); // Play weaponShootClip

        }
    }

    void Grenade()
    {
        if (hasGrenade)
        {
            //if player has sticky grenade
            if (Input.GetMouseButtonDown(1))
            {
                // if player presses secondary button
                // launch grenade certain distance
                // if grenade makes contact with police or wall, then it breaks or kills it - should be in grenade controller
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            health += 3; // player instantly dies if comes into contact with police
        } else if (collision.gameObject.layer == 12)
        {
            health++; // if shot by enemy bullet, then player loses one heart
            animator.SetBool("IsHurting", true);
        } else if (collision.gameObject.layer == 13)
        {
            finishedGame = true;
        }
    }
}
