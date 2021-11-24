using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{

    //public float speed; //assingned in unity 
    public float bulletForce; //assingned in unity 

    //[HideInInspector]
    //public Rigidbody2D rb;
    [HideInInspector]
    public Transform firePoint;
    //[HideInInspector]
    //public BoxCollider2D playerCollider;

    /*private enum ShootState
    {
        Ready,
        Shooting,
        Reloading
    }
    private ShootState shootState = ShootState.Ready;

    private int gun_id;
    private float bulletDamage;
    private float bulletPierce;
    private float reloadTime;
    private int roundsPerShot;
    private float accuracy; // the amount that the bullet spread is allowed to vary in degrees 


    private int launchForce;
    private int impactForce;
    private float bulletSize; //multiplier*/

    public GameObject gun;
    //public GameObject bulletPrefab;

    public Camera cam;
    [HideInInspector]
    //public Vector2 lookDir;

    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //Debug.Log("helbvgvhlo");
        begin();
        firePoint = rb.transform;
        /*gun = Gun(gun_id,
                bulletDamage,
                bulletPierce,
                reloadTime,
                roundsPerShot,
                accuracy,
                launchForce,
                impactForce,
                bulletSize);*/
        //playerCollider = GetComponent<BoxCollider2D>();
        //base.collider();
        gun.GetComponent<Gun>().initialize();
    }

    public Player()
    {
        size = 1;
        maxHealth = 750;
        speed = 4f;
    }

    /*public levelStart()
    {

    }*/

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     
        /*if (rb.velocity.magnitude > topSpeed)
        {
            //Debug.Log(rb.velocity.magnitude - topSpeed);
            //Debug.Log(2 ^ 2);
            Debug.Log(Mathf.Pow(1 - ((rb.velocity.magnitude - topSpeed)/ ((rb.velocity.magnitude + topSpeed))),2f));
            rb.velocity = rb.velocity * (1 - Mathf.Pow(((rb.velocity.magnitude - topSpeed) / ((rb.velocity.magnitude + topSpeed))), 3f));
        }*/

        rb.AddForce(new Vector3(movement.x, movement.y, 0).normalized * speed);
        Look();
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 pos = new Vector3(firePoint.position.x + boxCollider.size.x * lookDir.x * size / (1), firePoint.position.y + boxCollider.size.y * lookDir.y * size / (1), firePoint.position.z);
            if (gun.GetComponent<Gun>().Shoot(pos, firePoint, gameObject.tag, getColorPrimary()))
            {
                rb.AddForce(-transform.up * gun.GetComponent<Gun>().launchForce / rb.mass, ForceMode2D.Force);
            }

        }
        //gun.GetComponent<Gun>().Shoot(pos, firePoint.rotation, gameObject.tag, getColorPrimary());
    }

    void FixedUpdate()
    {
        //rb.AddForce(new Vector3(movement.x, movement.y, 0).normalized * speed * Time.fixedDeltaTime * 40);
        //rb.AddForce(new Vector3 (movement.x, movement.y, 0).normalized * speed);

        //Debug.Log(new Vector3 (movement.x, movement.y, 0) * speed);
        //Look();

    }

    /*public void shootBullet(){
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = player.transform.position;
    }*/

    public override void Look()
    {
        lookDir = (mousePos - rb.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        
        /*
        shootState = ShootState.Shooting;

        //creating bullet 
        GameObject bullet = Instantiate(bulletPrefab,
                                        new Vector3(firePoint.position.x + boxCollider.size.x * lookDir.x / 2 , firePoint.position.y + boxCollider.size.y * lookDir.y / 2 , firePoint.position.z),
                                        firePoint.rotation);


        bullet.GetComponent<Bullet>().allegiance = gameObject.tag;
        SpriteRenderer render = bullet.GetComponent<SpriteRenderer>();
        render.color = getColorPrimary();
        ParticleSystem ps = bullet.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        ma.startColor = getColorPrimary();


        Rigidbody2D rbullet = bullet.GetComponent<Rigidbody2D>();
        //bullet.GetComponent<Bullet>().pierce += 2;
        bullet.transform.Rotate(new Vector3(
                    Random.Range(-1f, 1f) * accuracy,
                    Random.Range(-1f, 1f) * accuracy,
                    0
                ));
        rbullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Force);        //bulletspeed 
        rb.AddForce(-firePoint.up * bulletForce / rb.mass, ForceMode2D.Force);  //recoil 
        //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Force);
        //rb.AddForce(firePoint.up, ForceMode2D.Impulse);
        //rb.AddForce(firePoint.normalized * bulletForce);

        //rb.AddForce(firePoint.up * 0, ForceMode2D.Impulse);
        */
    }
}