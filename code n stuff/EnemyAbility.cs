using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbility : Enemy
{

    protected float abilityTimer = 10f;
    protected float counter = 0f;

    /*void Start()
    {
        begin();
    }*/

    public override void Move(Transform target)
    {
        counter += Time.fixedDeltaTime;
        rb.transform.localScale = new Vector3(size * (1 + (counter / abilityTimer)), size * (1 + (counter / abilityTimer)), 1f);

        if (counter >= abilityTimer)
        {
            counter = 0;

            ability(target.transform);
            abilityExplosion();
            //Debug.Log("abil");
        }
        else
        {
            rb.AddForce((target.transform.position - transform.position).normalized * speed);
        }
    }

    public virtual void ability(Transform target)
    {
        
    }


    public virtual void abilityExplosion()
    {
        deathExplosion();
    }

}