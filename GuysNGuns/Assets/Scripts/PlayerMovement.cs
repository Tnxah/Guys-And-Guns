using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
   public string name;

    protected Joystick MoveJoystick;
    protected Joystick AimJoystick;

    public float moveSpeed = 5f;

    public GameObject aim;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    Rigidbody2D rb;
    Camera cam;
    Animator animator;

    float move;

    Vector2 aimPos;

    Vector2 movement;
    Vector2 mousePos;

    private void Awake()
    {
        name = Random.Range(0, 10000).ToString();
        animator = transform.GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("KURWAAAA");
        MoveJoystick = GameObject.Find("Move Joystick").GetComponent<Joystick>();
        AimJoystick = GameObject.Find("Aim Joystick").GetComponent<Joystick>();
        //healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);

        aim = GameObject.FindGameObjectWithTag("Aim");
        
        rb = transform.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + MoveJoystick.Horizontal;
        movement.y = Input.GetAxisRaw("Vertical") + MoveJoystick.Vertical;

        aimPos.x = transform.position.x + AimJoystick.Horizontal;
       // Debug.Log("Horiz " + AimJoystick.Horizontal);
        aimPos.y = transform.position.y + AimJoystick.Vertical;
       // Debug.Log("Vertic " + AimJoystick.Vertical);

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);
    }

    void FixedUpdate()
    {
        move = Mathf.Abs(movement.x * moveSpeed) + Mathf.Abs(movement.y * moveSpeed);
        animator.SetFloat("Speed", move);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        

        Vector2 lookDir = aimPos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullets owner name: " + collision.gameObject.GetComponent<Bullet>().owner + " My name: " + name);
        
        if (collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<Bullet>().owner != name)
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().Damage);
        }
    }
}
