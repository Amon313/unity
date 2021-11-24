using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public float force = 1f;
    public TrailRenderer trail;
    //public SpriteRenderer renderer;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float size;
    [HideInInspector]
    public float color;
    [HideInInspector]
    public float pierce;
    [HideInInspector]
    public float impact;
    [HideInInspector]
    public LinkedList<int> alreadyCollided = new LinkedList<int>();
    protected int collisionID;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public string allegiance;


    /*private void OnTriggerEnter( other)
    {
        Debug.Log("An object entered.");
    }*/

    // Start is called before the first frame update
    void Start()
    {
        bulletStart();
    }


    public virtual void bulletStart()
    {
        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();

        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0, rb.transform.localScale.x);
        curve.AddKey(1.0f, 0f);
        trail.widthCurve = curve;


        //renderer = GetComponent<SpriteRenderer>();
        trail.startColor = GetComponent<SpriteRenderer>().color;
        //Debug.Log(GetComponent<SpriteRenderer>().color);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("An object entered.");
        /*if (collision.gameObject.tag == "Explosion")
        {
            return;
        }*/
        if (collision.gameObject.tag == "Wall")
        {
            contactWithWall(collision);
            return;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            if(collision.gameObject.GetComponent<Bullet>().allegiance == allegiance)
            {
                contactWithFriendlyBullet(collision);
                return;
            }

            contactWithBullet(collision);
            return;
            
        }

        if (alreadyCollided.Find(collision.GetInstanceID()) == null)
        {
            if (collision.gameObject.tag != allegiance)
            {
                contactWithEnemy(collision);
                return;
            }
        }
    }

    public virtual void contactWithWall(Collider2D collision)
    {
        delete();
    }

    public virtual void contactWithFriendlyBullet(Collider2D collision)
    {

    }

    public virtual void contactWithBullet(Collider2D collision)
    {
        if (pierce > collision.gameObject.GetComponent<Bullet>().pierce)
        {
            pierce -= collision.gameObject.GetComponent<Bullet>().pierce;
            //Debug.Log(pierce);

            //Debug.Log("end");
            //Destroy(collision.gameObject, collision.gameObject.GetComponent<Bullet>().trail.time);
            collision.gameObject.GetComponent<Bullet>().delete();
            return;
        }
        else
        {
            collision.gameObject.GetComponent<Bullet>().pierce -= pierce;
            delete();
            return;
        }
        /*pierce -= collision.gameObject.GetComponent<Bullet>().pierce;
        collision.gameObject.GetComponent<Bullet>().pierce -= pierce;



        if (pierce <= 0)
        {



            Destroy(gameObject, trail.time);
        }
        //transform.parent = targetHead.transform;*/
    }

    public virtual void contactWithEnemy(Collider2D collision)
    {
        //new Vector2((collision.transform.position - transform.position).normalized.x * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x), (collision.transform.position - transform.position).normalized.y * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x));
        //Debug.Log(pierce);
        //Debug.Log("why is this being called?");
        collisionID = collision.GetInstanceID();
        Debug.Log("id: " + collision.GetInstanceID());
        alreadyCollided.AddFirst(collisionID);

        
        collision.attachedRigidbody.AddForce((collision.transform.position - transform.position).normalized * impact);

        collision.gameObject.GetComponent<Character>().damage(damage);

        pierce--;
        if (pierce <= 0)
        {
            delete();
            return;
        }
    }


    public virtual void delete()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        Destroy(gameObject, trail.time);
    }

    
}

