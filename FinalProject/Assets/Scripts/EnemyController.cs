using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bullet;
    public Transform player;
    public PlayerMovement p1;

    public Animator animator;

    public AudioClip deathClip;
    public AudioClip shootClip;
    
    public float distanceMin = 10f;
    private float distanceFromPlayer;

    private bool isFacingRight = true;
    public bool isDead = false;

    private float timer = 0f;
    public float timerAmount = 4f;
    public float dyingTime = 1f;

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = transform.position.x - player.position.x;

        if (distanceFromPlayer > 0 && isFacingRight)
        {
            Flip();
        }
        else if (distanceFromPlayer < 0 && !isFacingRight)
        {
            Flip();
        }

        timer += Time.deltaTime;
        if (Mathf.Abs(distanceFromPlayer) < distanceMin && Mathf.Abs(transform.position.y - player.position.y) < distanceMin && !isDead)
        {
            if (timer > timerAmount)
            {
                Shoot();
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        if (isFacingRight)
        {
            GameObject.Instantiate(bullet, transform.position, transform.rotation);
        }
        else
        {
            bullet.GetComponent<EnemyBulletController>().speed *= -1;
            GameObject.Instantiate(bullet, transform.position, transform.rotation);
            bullet.GetComponent<EnemyBulletController>().speed *= -1;
        }
        AudioSource.PlayClipAtPoint(shootClip, transform.position);
    }

    void Flip()
    {
            // Switch the way the player is labelled as facing.
            isFacingRight = !isFacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            isDead = true;
            animator.SetBool("IsDead", true);
            AudioSource.PlayClipAtPoint(deathClip, transform.position);
            Destroy(this.gameObject, dyingTime);
        } else if (collision.gameObject.layer == 9 && !isDead)
        {
            p1.health += 3;
        }
    }
}
