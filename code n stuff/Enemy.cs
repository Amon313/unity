using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //public float speed = 2f;    // Follow speed

    //[HideInInspector]
    public GameObject target;   // the target i want to get closer to 
    protected GameObject objSpawn;
    protected int SpawnerID;
 
    


    /*
        //[HideInInspector]
        public Rigidbody2D rb;
        //[HideInInspector]
        public new BoxCollider2D collider;
        [HideInInspector]
        public Vector2 lookDir;

        //private float abilityTimer = 10f;
        //private float counter = 0f;
    */

    /*void Start()
    {
        begin();
    }*/



    void Start()
    {
        //Debug.Log("character created");
        beginEnemy();
    }

    private void Update()
    {
        Move(target.transform); // do a follow
        Look();
        
    }

    public override void Look()
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
    }

    public override void die()
    {
        if (objSpawn)
        {
            objSpawn.BroadcastMessage("killEnemy", SpawnerID);

        }
        deathExplosion();
        Destroy(gameObject);
    }


    public override Color getColorPrimary()
    {
        return Color.red;
    }

    public override Color getColorSecondary()
    {
        return Color.red;
    }


    public void beginEnemy()
    {
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");
        begin();
        target = GameObject.Find("Player");
    }


    // this gets called in the beginning when it is created by the spawner script
    void setName(int sName)
    {
        SpawnerID = sName;
    }

}

