using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyAbility
{
    //public float reloadTimer = 10f;
    //public float counter = 0f;
    [HideInInspector]
    public Transform firePoint;
    public GameObject bulletPrefab;

    protected float bulletForce; //assingned in unity 

    //[HideInInspector]
    //public Vector2 lookDir;

    void Start()
    {
        beginEnemy();
        //gradientBegin();
        firePoint = rb.transform; 


    }

    public EnemyShooter()
    {
        bulletForce = 750;
        speed = 0.5f;
        abilityTimer = 12;
    }

    /*public override void Move(Transform target)
    {
        counter += Time.fixedDeltaTime;
        if (counter >= reloadTimer)
        {
            //lookDir = (new Vector2(target.transform.position.x, target.transform.position.y) - rb.position).normalized;
            Shoot();
            counter = 0;
        }
        else
        {
            rb.AddForce((target.transform.position - transform.position).normalized * speed);
        }
    }*/


    public override void ability(Transform target)
    {
        
         //creating bullet 
        GameObject bullet = Instantiate(bulletPrefab,
                                        new Vector3(firePoint.position.x + boxCollider.size.x * lookDir.x  , firePoint.position.y + boxCollider.size.y * lookDir.y  , firePoint.position.z),
                                        firePoint.rotation);
        bullet.GetComponent<Bullet>().allegiance = gameObject.tag;
        SpriteRenderer render = bullet.GetComponent<SpriteRenderer>();
        render.color = getColorPrimary();
        ParticleSystem ps = bullet.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        ma.startColor = getColorPrimary();
        Rigidbody2D rbullet = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().pierce = 1;
        rbullet.transform.localScale = new Vector3(size/2, size/2, 1f);

        rbullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Force);        //bulletspeed 
        rb.AddForce(-firePoint.up * bulletForce * 4 / rb.mass, ForceMode2D.Force);  //recoil 
        //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Force);
        //rb.AddForce(firePoint.up, ForceMode2D.Impulse);
        //rb.AddForce(firePoint.normalized * bulletForce);

        //rb.AddForce(firePoint.up * 0, ForceMode2D.Impulse);

    }

    public override Color getColorPrimary()
    {
        return Color.blue;
    }

    public override Color getColorSecondary()
    {
        return Color.green;
    }
}