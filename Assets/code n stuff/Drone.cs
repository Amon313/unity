using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Bullet
{
    public GameObject target;
    public float speed = 2f;
    public float angularSpeed = 0.75f;
    public float distance = 25;
    public float sightDistance = 250;
    public GameObject DeathExplosion;
    private GameObject[] gos;
    private Vector3 lookDir;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        bulletStart();
    }





    // The target marker.
    //public Transform target;

    // Angular speed in radians per sec.



    // Update is called once per frame
    void Update()
    {
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        target = FindClosestTarget(gos);
        //Debug.Log("target: " + target);
        if(target == null)
        {
            target = GameObject.Find("Player");
        }
        Move();
        Look();
        
    }
        

    public override void bulletStart()
    {
        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();

        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0, rb.transform.localScale.x);
        curve.AddKey(1.0f, 0f);
        trail.widthCurve = curve;


        trail.startColor = GetComponent<SpriteRenderer>().color;
        //Debug.Log(GetComponent<SpriteRenderer>().color);
    }

    /*public override void Look()
    {
        lookDir = (new Vector2(target.transform.position.x, target.transform.position.y) - rb.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public virtual void Move(Transform target)
    {
        // add force to my rigid body to make me move
        rb.AddForce((target.transform.position - transform.position).normalized * speed);
        //Debug.Log((target.transform.position - transform.position).normalized * speed);
    }*/

    public virtual void Move()
    {
        // add force to my rigid body to make me move
        //rb.AddForce(transform.rotation * speed);
        //Vector2 v2 = new Vector2(transform.rotation.x, transform.rotation.y);
        /*Debug.Log("transform.rotation: " + transform.rotation);
        Debug.Log("v2: " + v2);*/
        rb.AddForce(transform.up * speed);

    }

    public void Look()
    {
        //Vector3 dir = target.transform.position - transform.position;
        //lookDir = (new Vector2(target.transform.position.x, target.transform.position.y) - rb.position).normalized;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        /*Debug.Log("angle: " + angle + " rotation: " + rb.rotation);
        if(rb.rotation > -0)
        {
            if (rb.rotation > angle + angularSpeed || rb.rotation - 180 < angle + angularSpeed)
            {
                rb.rotation -= angularSpeed;
            }
        }
        else
        {
            if (rb.rotation > angle + angularSpeed || rb.rotation + 180 < angle + angularSpeed)
            {
                rb.rotation -= angularSpeed;
            }
        }
        if(rb.rotation < -270) { rb.rotation += 360;}
        if (rb.rotation > 90) { rb.rotation -= 360;}
        */
        //Debug.Log("angle: " + (angle + 90) + " rb.rotation: " + (rb.rotation + 90));

        float dot = Mathf.Cos((angle+90) * Mathf.Deg2Rad) * -Mathf.Sin((rb.rotation+90) * Mathf.Deg2Rad) + Mathf.Sin((angle+90) * Mathf.Deg2Rad) * Mathf.Cos((rb.rotation+90)*Mathf.Deg2Rad);
        if (dot > 0)
        {
            rb.rotation += angularSpeed;
            return;
        }
        rb.rotation -= angularSpeed;



        /*else if (rb.rotation < angle + angularSpeed || rb.rotation - 180 > angle + angularSpeed)
        {
            rb.rotation += angularSpeed;
        }
        else
        {
            rb.rotation = angle;
        }*/
            //rb.rotation = angle;
    }

    /*public void Look(GameObject target)
    {
        // Determine which direction to rotate towards
        Vector2 targetDirection = target.transform.position - transform.position;
        Debug.Log("targetDirection:" + targetDirection);

        // The step size is equal to speed times frame time.
        float singleStep = angularSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector2 newDirection = Vector2.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f).normalized;

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }*/

    private GameObject FindClosestTarget(GameObject[] gos)
    {
        GameObject closest = null;
        float tempDistance = sightDistance;
        Vector3 position = transform.position;
        Vector3 diff;
        foreach (GameObject go in gos)
        {
            diff = go.transform.position - position;
            //float curDistance = diff.sqrMagnitude;
            if (diff.sqrMagnitude < tempDistance)
            {
                closest = go;
                tempDistance = diff.sqrMagnitude;
            }
        }
        return closest;
    }

    public override void delete()
    {

        rb.velocity = new Vector2(0.0f, 0.0f);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;
        gos = sort(gos);

        while (pierce > 0 && i < gos.Length)
        {
            GameObject target = gos[i];
            i++;
            if (target == null)
            {
                break;
            }
            pierce--;

            target.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * impact);

            target.GetComponent<Character>().damage(damage);
        }

        ParticleSystem PartSystem = DeathExplosion.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule ma = PartSystem.main;
        ma.startColor = GetComponent<SpriteRenderer>().color;
        ma.startSpeed = new ParticleSystem.MinMaxCurve(0.3f * distance, 0.5f * distance);

        GameObject e = Instantiate(DeathExplosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);

        Destroy(gameObject);
    }

    private GameObject[] sort(GameObject[] gos)
    {
        int flag, i, j, k;
        k = gos.Length;
        float x;
        GameObject val;
        for (i = 0; i < k; i++)
        {
            val = gos[i];
            x = (val.transform.position - transform.position).sqrMagnitude;
            Debug.Log("x: " + x);
            if (x <= distance)
            {

                flag = 0;
                for (j = i - 1; j >= 0 && flag != 1;)
                {
                    if (x < (gos[j].transform.position - transform.position).sqrMagnitude)
                    {
                        gos[j + 1] = gos[j];
                        j--;
                        gos[j + 1] = val;
                    }
                    else flag = 1;
                }
            }
            else
            {
                gos[i] = gos[k - 1];
                gos[k - 1] = null;
                i--;
                k--;
            }
        }
        return gos;
    }

    public override void contactWithEnemy(Collider2D collision)
    {
        if (!dead)
        {
            dead = true;
            delete();
        }
        else
        {
            Debug.Log("no touching");
        }
    }
}
