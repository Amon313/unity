using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour

{
    public int gun_id;
    public float bulletDamage;
    public float bulletPierce;
    public float reloadTime;
    public int roundsPerShot;
    public float accuracy; // the amount that the bullet spread is allowed to vary in degrees 

    public GameObject bulletPrefab;

    public int launchForce;
    public int impactForce;
    public float bulletSize; //multiplier

    private float time = 0;


    private enum ShootState
    {
        Ready,
        Shooting
    }

    private ShootState shootState = ShootState.Ready;

    /*public Gun(int gun_id,
                float bulletDamage,
                float bulletPierce,
                float reloadTime,
                int roundsPerShot,
                float accuracy,
                int launchForce,
                int impactForce,
                float bulletSize
        return this;
    )
    {
        this.gun_id = gun_id;
        this.bulletDamage = bulletDamage;
        this.bulletPierce = bulletPierce;
        this.reloadTime = reloadTime;
        this.roundsPerShot = roundsPerShot;
        this.accuracy = accuracy;
        this.launchForce = launchForce;
        this.impactForce = impactForce;
        this.bulletSize = bulletSize;
    }*/

    public void initialize()
    {
        time = 0;
    }
    public void Update()
    {
        Debug.Log("This should never happen and if it does you should probaly investigate");
        /*Debug.Log(shootState);

        switch (shootState)
        {
            case ShootState.Ready:
                if (Input.GetButtonDown("Fire1"))
                {
                    //Shoot();
                }
                break;
            case ShootState.Shooting:
                // If the gun is ready to shoot again...

                if (Time.time > reloadTime)
                {
                    shootState = ShootState.Ready;
                }
                break;
            case ShootState.Reloading:
                // If the gun has finished reloading...
                if (Time.time > reloadTime)
                {
                    shootState = ShootState.Ready;
                }
                break;
        }*/

    }

    public bool Shoot(Vector3 pos, Transform trans, string allegiance, Color color)
    {
        //time += Time.deltaTime;
        switch (shootState)
        {
            /*case ShootState.Ready:
                if (Input.GetButtonDown("Fire1"))
                {
                    //Shoot();
                }
                break;*/
            case ShootState.Shooting:
                // If the gun is ready to shoot again...
/*
                Debug.Log("time:" + time);

                Debug.Log("time+reloadtime:" + time + "  " + reloadTime);
                Debug.Log("Time.time:" + Time.time);
                Debug.Log(time + reloadTime <= Time.time);
*/

                if (time + reloadTime <= Time.time)
                {
                    shootState = ShootState.Ready;
                    time = Time.time;
                    break;
                }
                return false;
                /*case ShootState.Reloading:
                    // If the gun has finished reloading...
                    if (Time.time > reloadTime)
                    {
                        shootState = ShootState.Ready;
                    }
                    break;*/
        }

        /*if (shootState != ShootState.Ready)
        {
            Debug.Log("false");
            return false;
        }*/

        shootState = ShootState.Shooting;

        //creating bullet 
        for (int i = 0; i < roundsPerShot; i++)
        { 
            GameObject bullet = Instantiate(bulletPrefab, pos, trans.rotation);

            //Debug.Log("bullet.transform.up: " + bullet.transform.up);

            bullet.GetComponent<Bullet>().allegiance = allegiance;

            SpriteRenderer render = bullet.GetComponent<SpriteRenderer>();
            render.color = color;
            ParticleSystem ps = bullet.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = ps.main;
            ma.startColor = color;

            Rigidbody2D rbullet = bullet.GetComponent<Rigidbody2D>();
            Bullet hurt = bullet.GetComponent<Bullet>();

            hurt.pierce = bulletPierce;
            hurt.damage = bulletDamage;
            hurt.transform.localScale = new Vector3(bulletSize * 0.125f, bulletSize * 0.125f, 1f);
            hurt.impact = impactForce;

            bullet.transform.up = (new Vector3(
                        bullet.transform.up.x + Random.Range(-1f, 1f) * accuracy/360,
                        bullet.transform.up.y + Random.Range(-1f, 1f) * accuracy/360,
                        0
                    ));
/*
            //Debug.Log("accruracy test: " + accuracy);
            float x = Random.Range(-1f, 1f) * accuracy/100;
            //Debug.Log("number to change aim by: " + x);
            Debug.Log("rotation before: " + rbullet.rotation);

            rbullet.rotation = trans.rotation.y + x;
            Debug.Log("bullet.transform.up: " + bullet.transform.up);
*/
            //Debug.Log("random: " + Random.Range(-1.00f, 1.00f));

            //trans.up = new Vector3(0, Random.Range(-1.00f, 1.00f), 0);

            rbullet.AddForce(bullet.transform.up * launchForce / roundsPerShot, ForceMode2D.Force);        //bulletspeed 
                                                                                //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Force);
                                                                                //rb.AddForce(firePoint.up, ForceMode2D.Impulse);
                                                                                //rb.AddForce(firePoint.normalized * bulletForce);

        }
        /*bullet.transform.Rotate(
                    0,
                    0,
                    Random.Range(-1f, 1f) * 100);*/

        //rb.AddForce(firePoint.up * 0, ForceMode2D.Impulse);
        return true;
    }
}
