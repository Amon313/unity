using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Weapon
{
    public struct Gun
    {
        public int gun_id;
        public float bulletDamage;
        public float bulletPierce;
        public float reloadTime;
        public int roundsPerShot;
        public float accuracy; // the amount that the bullet spread is allowed to vary in degrees 


        public int launchForce;
        public int impactForce;
        public float bulletSize; //multiplier

        public Gun(int gun_id,
                    float bulletDamage,
                    float bulletPierce,
                    float reloadTime,
                    int roundsPerShot,
                    float accuracy,
                    int launchForce,
                    int impactForce,
                    float bulletSize
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
        }
    }
}
