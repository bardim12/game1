using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health;
    public float numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float heal;


    public Animator animator;
    public Rigidbody2D rb;




    public bool Turn = true;

    Vector2 movement;
    float Move = 0f;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y= Input.GetAxisRaw("Vertical");

        Move = (Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical")) * moveSpeed; 
        animator.SetFloat("Speed", Mathf.Abs(Move));
    }

    private void FixedUpdate()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        health += Time.deltaTime * heal;

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite= fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;

            }

            if(i < numOfHearts) 
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if ( health < 1)
            {
                //animator.SetTrigger("EndGame");

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //добавить конец игры и рестарт

            }
        }


        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (Turn == false && movement.x > 0)
        {
            Flip();
           // Gn.offset = -180;
        }
        else if (Turn == true && movement.x < 0)
        {
            Flip();
           // Gn.offset = 0;
        }

    }

    void Flip()
    {
        Turn = !Turn;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
