using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject losePanel;


    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float speed;
    private float input;

    Rigidbody2D rb;
    Animator anim;
    AudioSource source;

    public int health;

    public float startDashTime;
    private float dashTime;
    public float extraSpeed;
    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(input != 0)
        {
            anim.SetBool("isRunning", true);
        } 
        else {
            anim.SetBool("isRunning", false);
        }

        if(input < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(input > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isDashing == false)
        {
            speed += extraSpeed;
            isDashing = true;
            dashTime = startDashTime;
        }

        if(dashTime <=0 && isDashing ==true)
        {
            isDashing = false;
            speed -= extraSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Storing player's input
        input = Input.GetAxisRaw("Horizontal");
        
        // Moving player
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    public void TakeDamage(int damageAmount)
    {
        source.Play();
        health -= damageAmount;
        UpdateHealthUI(health);
        if(health <= 0)
        {
            losePanel.SetActive(true);
            Destroy(gameObject);
        }
    }

    void UpdateHealthUI(int currentHealth){
        for(int i = 0; i < hearts.Length; i++){
            if(i < currentHealth){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public void Heal(int healAmount){
        if(health + healAmount > 5){
            health = 5;
        } else {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }
}

