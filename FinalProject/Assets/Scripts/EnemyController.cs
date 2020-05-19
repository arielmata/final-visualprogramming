using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bullet;
    public Transform player;
    public Animator animator;
    
    public float distanceMin = 10f;
    private float distanceFromPlayer;

    private bool isFacingRight = true;

    private float timerBullet;
    private float maxTimerBullet;

    public float dyingTime = 1f;
    public float timerMin = 2f;
    public float timerMax = 10f;
    public bool canFireBullets = false;

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

        // Next, we want to see y distance min.
        if (Mathf.Abs(distanceFromPlayer) < distanceMin || Mathf.Abs(transform.position.y - player.position.y) < distanceMin)
        {
            canFireBullets = true;
            Shoot();
        }
        else
        {
            canFireBullets = false;
        }
    }

    void Shoot()
    {
        while (canFireBullets)
        {
            timerBullet = 0f;
            maxTimerBullet = Random.Range(timerMin, timerMax);

            if (timerBullet >= maxTimerBullet)
            {
                if (!isFacingRight)
                {
                    GameObject.Instantiate(bullet, transform.position, transform.rotation);
                }
                else
                {
                    bullet.GetComponent<EnemyBulletController>().speed *= -1;
                    GameObject.Instantiate(bullet, transform.position, transform.rotation);
                    bullet.GetComponent<EnemyBulletController>().speed *= -1;
                }
                timerBullet = 0;
                maxTimerBullet = Random.Range(timerMin, timerMax);
            }
            timerBullet += 0.1f;
        }
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
            animator.SetBool("IsDying", true);
            Destroy(this.gameObject, dyingTime);
        }
    }
}
