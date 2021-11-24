using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMartyr : Enemy
{
    public override void deathExplosion()
    {
        Character[] foundCharacters = FindObjectsOfType<Character>();
        //Debug.Log(foundCharacters + " : " + foundCharacters.Length);
        Vector2 pos = target.transform.position;   
        Vector2 martyrPos = rb.position;

        Vector2 heading;
        Vector2 distance;

        
        // loops throgh all the enemys and checks that its not the target and not self then moves that character towards target
        foreach (var character in foundCharacters)
        {
            if (martyrPos != (Vector2)character.transform.position)
            {
                distance = martyrPos - (Vector2)character.transform.position;
                heading = pos - (Vector2)character.transform.position;
                float mag = distance.magnitude;
                if (mag < 7)
                {
                    character.rb.AddForce(heading.normalized * force, ForceMode2D.Force);
                    //Debug.Log(heading.sqrMagnitude);
                }
                /*else
                {
                    character.rb.AddForce(heading.normalized * force * 1 / (character.rb.mass), ForceMode2D.Force);
                }*/
            }
        }

        ParticleSystem PartSystem = DeathExplosion.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule ma = PartSystem.main;
        ma.startColor = getColorPrimary();

        GameObject e = Instantiate(DeathExplosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }

    public EnemyMartyr()
    {
        force /= 3;
    }

    public override Color getColorPrimary()
    {
        return Color.yellow;
    }

    public override Color getColorSecondary()
    {
        return Color.red;
    }

    public override Color deathExplosionParticleColor()
    {
        return getColorSecondary();
    }

}
