using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float speed = 3f;    // speed
    protected float force = 750;
    protected float contactDamage = 10;
    protected float maxHealth = 10;
    protected float health;
    protected float size = 0.25f;


    protected Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;
    protected SpriteRenderer render;

    //[HideInInspector]
    //public GameObject target;   // the target i want to get closer to 

    //[HideInInspector]
    public Rigidbody2D rb;

    //[HideInInspector]
    public BoxCollider2D boxCollider;
    [HideInInspector]
    public Vector2 lookDir;

    public GameObject DeathExplosion;
    //public float topSpeed;

    private ContactPoint2D[] colliders = new ContactPoint2D[1];

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("character created");
        begin();
    }

    public void begin()
    {
        health = maxHealth;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 0.18f;
        rb.mass = 5;
        rb.transform.localScale = new Vector3(size, size, 1f);
        //DeathExplosion = GameObject.Find("DeathExplosion");
        //Debug.Log(DeathExplosion);
        gradientBegin();

    }

    



    // Update is called once per frame
    void Update()
    {
        Look();

    }

    public void gradientBegin()
    {
        gradient = new Gradient();

        render = GetComponent<SpriteRenderer>();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = getColorPrimary();
        colorKey[0].time = 0.0f;
        colorKey[1].color = getColorSecondary();
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        /*
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;
*/
        gradient.SetKeys(colorKey, alphaKey);
        render.color = getColorPrimary();

    }

    public virtual Color getColorPrimary()
    {
        return Color.white;
    }

    public virtual Color getColorSecondary()
    {
        return Color.red;
    }

    public virtual void Look()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            return;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            return;
        }

        if (collision.gameObject.tag == gameObject.tag)
        {
            return;
        }
       


        //Debug.Log("hello does this ever happen");
        //Debug.Log(collision.gameObject.tag);

        Vector2 pos = rb.position;
        Vector2 heading;

        heading = (Vector2)collision.transform.position - pos;
        collision.rigidbody.AddForce(heading.normalized * force / (collision.rigidbody.mass), ForceMode2D.Force);


        collision.gameObject.GetComponent<Character>().damage(contactDamage);

        /* foreach (var character in foundCharacters)
         {


             if (heading != Vector2.zero)
             {

                 if (heading.sqrMagnitude > 1)
                 {
                     character.rb.AddForce(heading.normalized * force * 2 / (heading.sqrMagnitude * character.rb.mass), ForceMode2D.Force);
                     //Debug.Log(heading.sqrMagnitude);
                 }
                 else
                 {
                 }
             }
         }*/
        /*if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("then how bout this");
            
               // rb.SetRotation(360 - rb.rotation);
                 
        
            Debug.Log("xasd");
            //Collider2D[] colliders;
            int x = collision.GetContacts(colliders);
            Debug.Log("x");
            Vector2 inNormal = colliders[0].normal;
            //rb.velocity = 

            //Debug.Log(rb.velocity.Reflect(new Vector2(0.0f, 0.0f), new Vector2(0.0f, 0.0f)));

            //Debug.Log(inNormal);
            //Debug.Log(rb.velocity.Reflect(rb.velocity, inNormal));

            //Debug.Log(rb.velocity.magnitude - topSpeed);
            //rb.velocity = Vector2.Reflect(rb.velocity, inNormal);

            *//*
                    Vector2D inDirection = GetComponent<RigidBody2D>().velocity;
                    Vector2D inNormal = collision.contacts[0].normal;
                    Vector2D newVelocity = Vector2D.Reflect(inDirection, inNormal);
            *//*
            //collision.attachedRigidbody.AddForce((collision.transform.position - transform.position).normalized * force);
            //collisionID = collision.GetComponent<Rigidbody2D>().gameObject.GetInstanceID();

            Debug.Log("collision");


        }
        return;*/
    }

    public virtual void damage(float dam)
    {
        health -= dam;
        /*Debug.Log(dam);
        Debug.Log(health);*/

        if (health <= 0)
        {
            die();
        }

    }

    public virtual void die()
    {

        /* GameObject bullet = Instantiate(bulletPrefab,
                                         new Vector3(firePoint.position.x + boxCollider.size.x * lookDir.x / 2, firePoint.position.y + boxCollider.size.y * lookDir.y / 2, firePoint.position.z),
                                         firePoint.rotation);*/
        deathExplosion();

        //e.GetComponent<ParticleSystem>().StartColor = getColorPrimary();
        //bullet.GetComponent<Bullet>().pierce += 2;
        //Destroy(gameObject, 10f);
        Destroy(gameObject);

        //GameObject e = Instantiate(Enemy) as GameObject;
        //e.transform.position = transform.position;
        return;
    }

    public virtual void deathExplosion()
    {
        /*GameObject NewParticles = Instantiate(Particles, OffScreen, Quaternion.identity) as GameObject;
        Debug.Log("Newpart at " + OffScreen);*/
        Character[] foundCharacters = FindObjectsOfType<Character>();
        //var foundTextMeshObjects = FindObjectsOfType(typeof(TextMesh));
        //Debug.Log(foundCharacters + " : " + foundCharacters.Length);
        Vector2 pos = rb.position;
        Vector2 heading;

        // loops throgh all the characters and checks that its not self then moves that character away from self 

        foreach (var character in foundCharacters)
        {
            heading = (Vector2)character.transform.position - pos;
            if (heading != Vector2.zero)
            {
                if (heading.sqrMagnitude > 1)
                {
                    character.rb.AddForce(heading.normalized * force * 2 / (heading.sqrMagnitude * character.rb.mass), ForceMode2D.Force);
                    //Debug.Log(heading.sqrMagnitude);
                }
                else
                {
                    character.rb.AddForce(heading.normalized * force * 2 / (character.rb.mass), ForceMode2D.Force);
                }
            }
        }

        //Debug.Log(foundTextMeshObjects + " : " + foundTextMeshObjects.Length);
        //GameObject NewParticles = Instantiate (Particles, BalloonObj.transform.position, BalloonObj.transform.rotation) as GameObject;
        ParticleSystem PartSystem = DeathExplosion.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule ma = PartSystem.main;
        ma.startColor = deathExplosionParticleColor();

        GameObject e = Instantiate(DeathExplosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);

    }

    public virtual Color deathExplosionParticleColor()
    {
        return getColorPrimary();
    }
}