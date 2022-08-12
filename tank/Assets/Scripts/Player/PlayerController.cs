using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class PlayerController : MonoBehaviour, IFigure
{
    //Initialization health player
    public int maxHealth = 100;
    //Initialization current health player
    public int currentHealth;

    public HealthPlayerController healthPlayerController;
    //Initialization speed tank
    [Range(0, 100)]
    public float speed;

    //speed rotate tank
    [Range(0, 100)]
    public float rotateSpeed;

    //create object pooling
    public BulletObjectPooling bulletPool;

    //position, rotation and scale barel of tank
    public Transform barrel;

    //timer fire
    private float fireTimer;

    //interval
    private float fireInterval = 0.1f;

    public AudioManager audioManager;

    AudioSource m_shootingSound;

    public static int totalScore;

    public Text currentScore; 

    //private static PlayerController _instance;
    //public static PlayerController Instance => _instance;

    public static bool isLose = false;

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        fireTimer = Mathf.Infinity;
        currentHealth = maxHealth;
        //  healthPlayerController.SetMaxHealth(maxHealth);
        m_shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ShootBullet();
    }

    //get a bullet
    private GameObject GetABullet()
    {
        GameObject bullet = bulletPool.GetBullet();
        return bullet;
    }


    //Called when this collider/rigidbody has begun touching Bullet Enemy
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            TakeDamage(100);
        }
    }

    //Called when player dead
    public void Death()
    {
        gameObject.SetActive(false);
        isLose = true;
    }
    //Called when the player hit enemy bullets
    public void TakeDamage(int damge)
    {
        currentHealth -= damge;
        //send current health player to display play screen
        healthPlayerController.SetHealth(currentHealth);
        if(currentHealth == 0)
        {
            Death();
        }    
    }

    //Call when player Shoot bullets
    public void ShootBullet()
    {
        //Get event when click button Space from keyboard
        if (Input.GetKey(KeyCode.Space) && fireTimer > fireInterval)
        {
            fireTimer = 0f;
            //
            GameObject newBullet = GetABullet();

            if (newBullet != null)
            {
                m_shootingSound.Play();
                newBullet.SetActive(true);
                //Shoot bullets in the direction of the barrel x(1,0,0)
                newBullet.transform.forward = barrel.transform.right;
                //Move the bullet to the tip of the gun or it will look strange if we rotate while firing
                newBullet.transform.position = barrel.transform.position;
            }
            else
            {
                Debug.Log("Couldn't find a new bullet");
            }
        }
        //Update the time since we last fired a bullet
        fireTimer += Time.deltaTime;
    }
    //Move Tank in the direction
    public void Move()
    {
        //Get event and rotate in the y-axis (0,1,0)
        var Horizontal = Input.GetAxis("Horizontal") * Vector3.up * Time.deltaTime * speed;
        //Get event and move forward the z-axis (0,0,1)
        var Vertical = Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * speed;
        //Move forward
        transform.Translate(Vertical);
        //Move Rotate
        transform.Rotate(Horizontal);
    }
    //Add score for player 
    public void UpdateScore()
    {
        totalScore += 1;
        currentScore.text = "Score: " + totalScore;
    }

    public void OnEnable()
    {
        EnemyController.onDeath += UpdateScore;
    }

    public void OnDisable()
    {
        EnemyController.onDeath -= UpdateScore;
    }
}
