using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    /*public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce;

    private BoxCollider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        Debug.Log(bulletForce);
    }

   
        
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab,
                                        new Vector3(firePoint.position.x + playerCollider.size.x/2, firePoint.position.y + playerCollider.size.y/2, firePoint.position.z),
                                        firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up.normalized * bulletForce, ForceMode2D.Force);
        //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Force);
        //rb.AddForce(firePoint.up, ForceMode2D.Impulse);
        //rb.AddForce(firePoint.normalized * bulletForce);

        //rb.AddForce(firePoint.up * 0, ForceMode2D.Impulse);

    }*/
}
