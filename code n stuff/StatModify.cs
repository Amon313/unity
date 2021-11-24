using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModify : MonoBehaviour
{
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.

    public float speed = 0;            //force that is added to player when they move 
    public float force = 0;           //force applied to an enemy when they make contact 
    public float contactDamage = 0;    //damage applied to an enemy when they make contact 
    public float maxHealth = 0;        //maxium amount of health you can have 
    public float size = 1;          //size of character (multiplicity)
    
    public float bulletForce = 0;           //the amount of force the bullet shoots out with   also launchForce
    public float bulletDamage = 0;          //damage a bullet does   
    public float bulletPierce = 0;          //the amount of enemies a bullet can hurt 
    public float reloadTime = 0;            //the time it takes for the gun to be able to fire again 
    public int roundsPerShot = 0;           //the number of rounds per "shot" each round get an eqaul amount of force dived equally 
    public float accuracy = 0;              //the amount that the bullet spread is allowed to vary in degrees 
    public float impactForce = 0;             //the impact the bullet has on an enemy when it makes contact 
    public float bulletSize = 1;            //the size of bullet (multiplicity)

    //Constructor (not necessary, but helpful)
    public StatModify(){}

    public void increaseSpeed(float x)
    {
        speed += x; 
    }
    public void increaseForce(float x)
    {
        force += x;
    }
    public void increaseContactDamage(float x)
    {
        contactDamage += x;
    }
    public void increaseMaxHealth(float x)
    {
        maxHealth += x;
    }
    public void increaseSize(float x)
    {
        size *= x;
    }
    public void increaseBulletForce(float x)
    {
        bulletForce += x;
    }
    public void increaseBulletDamage(float x)
    {
        bulletDamage += x;
    }
    public void increaseBulletPierce(float x)
    {
        bulletPierce+= x;
    }
    public void increaseReloadTime(float x)
    {
        reloadTime += x;
    }
    public void increaseRoundsPerShot(int x)
    {
        roundsPerShot += x;
    }
    public void increaseAccuracy(float x)
    {
        accuracy += x;
    }
    public void increaseImpactForce(float x)
    {
        impactForce += x;
    }
    public void increaseBulletSize(float x)
    {
        bulletSize *= x;
    }
}