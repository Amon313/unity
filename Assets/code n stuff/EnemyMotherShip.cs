using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotherShip : Enemy
{
    /* // Start is called before the first frame update
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }*/
    public CircleCollider2D circleCollider;
    private float initialMass;

    public GameObject SpawnExplosion;
    public List<GameObject> Enemies;

    public EnemyMotherShip()
    {
        size *= 80;
        maxHealth *= 50;
        speed *= 10f;
        force *= 4f;
    }

    void Start()
    {
        beginEnemy();
        //gradientBegin();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        rb.mass *= 12;

        initialMass = rb.mass;
    }

    public override void damage(float dam)
    {   
        Debug.Log("size: " + size);
        Debug.Log("mass: " + rb.mass);
        Debug.Log("Enemies.Count: " + Enemies.Count);
        for (int i = 0; i < (int) dam; i++)
        {
            int enemyChoosen = Random.Range(0, Enemies.Count);
            Vector2 randDir = Random.insideUnitCircle;
            GameObject baby = (GameObject)Instantiate(Enemies[enemyChoosen], new Vector3(rb.transform.position.x + circleCollider.radius * randDir.x * size, rb.transform.position.y + circleCollider.radius * randDir.y * size, rb.transform.position.z), Quaternion.identity);
            baby.GetComponent<Rigidbody2D>().AddForce(randDir * 700);
            
            ParticleSystem PartSystem = SpawnExplosion.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = PartSystem.main;
            GameObject e = Instantiate(SpawnExplosion, new Vector3(rb.transform.position.x + circleCollider.radius * randDir.x * size / (1), rb.transform.position.y + circleCollider.radius * randDir.y * size / (1), rb.transform.position.z), transform.rotation);
        }
        health -= dam;
        render.color = gradient.Evaluate(1 - health / maxHealth);
        if (health > 0)
        {
            rb.transform.localScale = new Vector3(size - size * (0.9f * (maxHealth - health) / (maxHealth)), size - size * (0.9f * (maxHealth - health) / (maxHealth)), 1f);
            rb.mass = initialMass - initialMass * (0.9f * (maxHealth - health) / (maxHealth));
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

        
        deathExplosion();

        //Enemy.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 375);
        Destroy(gameObject);
        //return;
    }


    public override Color getColorPrimary()
    {
        return new Vector4(0.6705883f, 1f, 0.63f, 1); ;
    }

    public override Color getColorSecondary()
    {
        return new Vector4(0.75f, 0.75f, 0.75f, 1); ;
    }
}
