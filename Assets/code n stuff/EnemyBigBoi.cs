using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigBoi : Enemy
{
    /* // Start is called before the first frame update
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }*/
    private float initialMass;

    public GameObject enemy;

    public EnemyBigBoi()
    {
        maxHealth *= 5;
        force *= 4.5f;
    }

    void Start()
    {
        beginEnemy();
        //gradientBegin();
        initialMass = rb.mass;
    }

    public override void damage(float dam)
    {
        health -= dam;
        render.color = gradient.Evaluate(1 - health/maxHealth);
        if (health > 0)
        {
            rb.transform.localScale = new Vector3(size + size * (4 * (maxHealth - health) / (maxHealth)), size + size * (4 * (maxHealth - health) / (maxHealth)), 1f);
            rb.mass = initialMass + initialMass * (4 * (maxHealth - health) / (maxHealth));
        }
        /*Debug.Log(dam);
        Debug.Log(health);
        Debug.Log("HELATH:" + health);
        Debug.Log("maxHELATH:" + maxHealth);*/
        if (health <= 0)
        {
            die();
        }

    }

    

    public override void die()
    {
        objSpawn.BroadcastMessage("killEnemy", SpawnerID);

        for (int i = 0; i < 15; i++)
        {
            GameObject baby = (GameObject)Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
            baby.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 375);
        }
        deathExplosion();

        //Enemy.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 375);
        Destroy(gameObject);
        //return;
    }


    public override Color getColorPrimary()
    {
        return Color.magenta;
    }

    /*public override Color getColorSecondary()
    {
        return new Vector4(1f, 0.5f, 0);
    }*/
}
