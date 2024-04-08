using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health;
    public float speed = 5.0f;
    public GameObject DownHealth;
    public int damage;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    private PlayerMovement player;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        normalSpeed = speed;

    }

    private void Update()
    {
        if(stopTime <= 0)
        {
            speed = normalSpeed;
        } else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }


        if (health <= 0)
        {
            Instantiate(DownHealth, transform.position, Quaternion.identity);   
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        stopTime = startTimeBtwAttack;
        Instantiate(DownHealth, player.transform.position, Quaternion.identity);
        health -= damage;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if (timeBtwAttack <= 0){
                anim.SetTrigger("attakEnemy");
            }
            else {
                anim.SetTrigger("stopAttack");
                timeBtwAttack -= Time.deltaTime;
            }
        }               
    }

    public void OnEnemyAttak()
    {
        Instantiate(DownHealth, player.transform.position, Quaternion.identity);
        player.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }
}
