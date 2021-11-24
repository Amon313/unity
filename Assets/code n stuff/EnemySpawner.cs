using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : EnemyAbility
{
    public GameObject enemy;
    public GameObject SpawnExplosion;

    public EnemySpawner()
    {
        size *= 2;
        maxHealth *= 3;
        speed /= 2;
        abilityTimer = 35f;
    }

    void Start()
    {
        beginEnemy();
        rb.mass *= 2;

    }

    public override void ability(Transform target)
    {
        GameObject baby = (GameObject)Instantiate(enemy, new Vector3(rb.transform.position.x + boxCollider.size.x * lookDir.x * size / (1), rb.transform.position.y + boxCollider.size.y * lookDir.y * size / (1), rb.transform.position.z), Quaternion.identity);

        baby.GetComponent<Rigidbody2D>().AddForce(rb.transform.up * 2.5f * force, ForceMode2D.Force);

        rb.AddForce(-rb.transform.up * 10 * force / rb.mass, ForceMode2D.Force);  //recoil 
        counter = abilityTimer * 0.85f;
        //baby.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 375);
    }

    public override void abilityExplosion()
    {
        ParticleSystem PartSystem = SpawnExplosion.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = PartSystem.main;
        //ma.startColor = getColorPrimary();
        GameObject e = Instantiate(SpawnExplosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }

    public override Color getColorPrimary()
    {
        return new Vector4(0.6705883f, 0.9529412f, 0.6392157f, 1);
    }

    public override Color getColorSecondary()
    {
        return new Vector4(0.6705883f, 0.9529412f, 0.6392157f, 1);
    }

}
