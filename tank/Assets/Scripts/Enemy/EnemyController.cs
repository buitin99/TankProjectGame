using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour,IFigure
{
    public GameObject barrel;
    public GameObject projectile;
    //public GameObject explosion;
    private RaycastHit vision;
    [Range(0.1f, 100.0f)]
    [SerializeField]
    private float speedMove = 5f;
    private int health;
    private int visionRange;
    public static event Action onDeath;
    // Start is called before the first frame update
    void Start()
    {
        //The execution of a coroutine can be paused at any point using the yield statement.
        StartCoroutine(OnRandomMove());
        StartCoroutine(OnRandomShoot());
        health = 100;
        visionRange = 30;
    }

    // Update is called once per frame
  

    public void ShootBullet()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
    }
    //Called when this collider/rigidbody has begun touching Bullet Player
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            if (health > 0)
            {
                TakeDamage(100);
                if (health == 0)
                {
                    Death();
                }
            }
        }
    }

    //Called when enemy dead
    public void Death()
    {   
        if (onDeath != null)
        {
            //Check onDeath is listening
            onDeath?.Invoke();
        }    
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //Called when the enemy hit player bullets
    public void TakeDamage(int damge)
    {
            health -= damge;
    }
    void Update()
    {
        //Debug.DrawRay(barrel.transform.position, transform.TransformDirection(Vector3.forward) * 30, Color.red);
    }


    //Check enemy when collider with Obstacle 
    private bool CheckObstacle()
    {
        //Debug.Log(Physics.Raycast(barrel.transform.position, transform.TransformDirection(Vector3.forward), out vision));
        //Physics.Raycast is ray intersects
        //Physics.Raycast return true if the ray intersects with collider  
        //barrel.transform.position is The starting point of the ray in world coordinates.
        //transform.TransformDirection(Vector3.forward) is The direction of the ray z(0,0,1). 
        //out vision is The max distance the ray should check for collisions.
        if (Physics.Raycast(barrel.transform.position, transform.TransformDirection(Vector3.forward), out vision))
        {
            var hitPoint = vision.point;
            //Returns the distance between two point
            var distance = Vector3.Distance(hitPoint, barrel.transform.position);
            return ((vision.collider.CompareTag("wall") || vision.collider.CompareTag("player1")) && distance < visionRange);
        }
        else
        {
            return false;
        }
    }

    //Enemy Move Random
    private IEnumerator OnRandomMove()
    {
        
        while (true)
        {
            // Check if there's obstacle in vision range, then random rotation another direction, then check is there obstacle again
            while (CheckObstacle())
            {
                //The yield return statement is special;
                //it is what actually tells Unity to pause the script and continue on the next frame
                yield return new WaitForSeconds(1);
                //When enemy touch obstacle then barrel enemy will rotate
                //transform.rotation is Quaternion
                //Quaternion that stores the rotation of the Transform in world space. (x,y,z,w)
                // Euler returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.
                gameObject.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

            }

            // If there's no obstacle in vision range, then move on 0.5s forward
            yield return new WaitForSeconds(1);
            var moveTimer = .5f;
            while (moveTimer > 0)
            {
                gameObject.transform.Translate(speedMove * Time.deltaTime * (Vector3.forward));
                moveTimer -= Time.deltaTime;
                yield return null;
            }

        }
    }

    //Enemy Shoot Random
    private IEnumerator OnRandomShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            //fire point is barrel enemy tank
            Transform firePoint = barrel.transform;
            //Debug.DrawRay(firePoint.position, barrel.transform.right * 100, Color.red);
            //Rb Control of an object's position through physics simulation.
            //projectile is An existing object that you want to make a copy of
            //firePoint.position is Position for the new object.
            //Quaternion.identity is Orientation of the new object.
            //Quaternion.identity is This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            //Force is applied continuously along the direction of the force vector y(0,1,0)
            //ForceMode.Impulse is Add an instant force impulse to the rigidbody, using its mass.
            rb.AddForce((barrel.transform.up) * 100f, ForceMode.Impulse);
            rb.AddTorque((barrel.transform.up) * 1f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
