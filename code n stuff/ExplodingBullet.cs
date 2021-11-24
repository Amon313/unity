using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : Bullet
{
    public float distance;
    public GameObject DeathExplosion;
    public float mass;
    private bool dead = false;

    void Start()
    {
        bulletStart();
        rb.mass = mass;
        //LR = GetComponent<LineRenderer>();
    }

    public override void contactWithBullet(Collider2D collision)
    {
        delete();
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

        Destroy(gameObject, trail.time);
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
}