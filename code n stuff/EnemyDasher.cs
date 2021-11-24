using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDasher : EnemyAbility
{

    //abilityTimer = 30f;

    /*    public float counter = 0f;*/
    //float abilityTimer = 10f;

    



    
    private int charge = 10;

    //abilityTimer = 30f;
    private float i = 0;
    public EnemyDasher()
    {
        abilityTimer = 50f;
    }

    private void Update()
    {
        Move(target.transform); // do a follow
        if(counter < abilityTimer)
        {
            Look();
        }
    }

    public override void Move(Transform target)
    {

        
        if (counter >= abilityTimer)
        {
            if (i <= charge)
            {
                //render.color *= Color.white;
                i += Time.fixedDeltaTime;
                //Debug.Log(i);
                //Debug.Log(render.color);
                render.color = gradient.Evaluate(1 - (i/charge));
                rb.transform.localScale = new Vector3(size + size * (1 * (i / charge)), size + size * (1 * (i / charge)), 1f);

            }
            else
            {
                i = 0;
                counter = 0;
                ability(target.transform);
                rb.transform.localScale = new Vector3(size, size, 1f);
                //render.color = Color.green;
            }
            

            //Debug.Log("abil");
        }
        else
        {
            counter += Time.fixedDeltaTime;
            render.color = gradient.Evaluate((counter / abilityTimer));
            rb.AddForce((target.transform.position - transform.position).normalized * speed);
        }
    }

    public override void ability(Transform target)
    {
        //add force to my rigid body to make me move
        //rb.velocity = new Vector2(0, 0);
        //Vector2 vec = new Vector2((target.position - transform.position).normalized.x * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x), (target.position - transform.position).normalized.y * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x));
        //rb.velocity = vec;
        //new Vector2((target.position - transform.position).normalized.x * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x), (target.position - transform.position).normalized.y * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x));
        //rb.AddForce(speed * 11);
        //rb.AddForce((target.transform.position - transform.position).normalized * (speed * 11));
        //render.color = new Color(0, 1, 0, 1);
 
        rb.AddForce((target.transform.position - transform.position).normalized * speed * 2000);
        //yield return new WaitForSeconds(5);

        //Debug.Log((target.transform.position - transform.position).normalized * speed);
    }

    public override Color getColorPrimary()
    {
        return Color.green;
    }

    public override Color getColorSecondary()
    {
        return Color.white;
    }
}
