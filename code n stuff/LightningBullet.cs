using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : Bullet
{
    //private GameObject target;
    private GameObject target;
    private LineRenderer lineRend;
    private float arcLength = 0.5f;
    private float arcVariation = 1.0f;
    private float inaccuracy = 0.5f;
    public float distance;
    private bool dead = false;

    //public LinkedList<int> alreayZapped = new LinkedList<int>();

    void Start()
    {
        bulletStart();
        lineRend = gameObject.GetComponent<LineRenderer>();
        lineRend.positionCount = (1);
        //LR = GetComponent<LineRenderer>();
    }

    public override void contactWithEnemy(Collider2D collision)
    {
        if (!dead)
        {
            dead = true;
        }
        else
        {
            Debug.Log("no touching");
            return;
        }
        int lightningVert = 1;
        int i = 0;
        lineRend.SetPosition(0, transform.position);//make the origin of the LR the same as the transform
        Vector3 lastPoint = transform.position;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //gos = sort(gos);
        GameObject target;
        while (pierce > 0 && i < gos.Length)
        {


            //Debug.Log("hit enemy pierce: " + pierce);
            //new Vector2((collision.transform.position - transform.position).normalized.x * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x), (collision.transform.position - transform.position).normalized.y * Mathf.Sqrt(rb.velocity.y * rb.velocity.y + rb.velocity.x * rb.velocity.x));
            //Debug.Log(pierce);
            //Debug.Log("colllid");
            /*collisionID = collision.GetInstanceID();
            alreadyCollided.AddFirst(collisionID);*/

            /*
                    collision.attachedRigidbody.AddForce((collision.transform.position - transform.position).normalized * impact);

                    collision.gameObject.GetComponent<Character>().damage(damage);

                    pierce--;
            */

            target = FindClosestTarget(gos);
            //target = gos[i];
            i++;
            if (target == null)
            {
                Debug.Log("there is no closest");
                break;
            }
            // uncomment this line to have the lightning start from the first one hit to bounce around to all the others and comment the one up above
            //Vector3 lastPoint = transform.position; // uncomment this one to 

            while (Vector3.Distance(target.transform.position, lastPoint) > 0.5f)
            {//was the last arc not touching the target?
                lineRend.positionCount = (lightningVert + 1);//then we need a new vertex in our line renderer
                Vector3 fwd = target.transform.position - lastPoint;//gives the direction to our target from the end of the last arc
                fwd.Normalize();//makes the direction to scale
                fwd = Randomize(fwd, inaccuracy);//we don't want a straight line to the target though
                fwd *= Random.Range(arcLength * arcVariation, arcLength);//nature is never too uniform
                fwd += lastPoint;//point + distance * direction = new point. this is where our new arc ends
                lineRend.SetPosition(lightningVert, fwd);//this tells the line renderer where to draw to
                lightningVert++;
                lastPoint = fwd;//so we know where we are starting from for the next arc
            }
            pierce--;
            lineRend.positionCount = (lightningVert + 1);
            lineRend.SetPosition(lightningVert, target.transform.position);
            Debug.Log(target);

            zap(target);

            
            //lightTrace.TraceLight(gameObject.transform.position, target.transform.position);
            //delete();
        }
        delete();
        
        
    }

    private void zap(GameObject target)
    {
        target.GetComponent<Character>().damage(damage);
    }

    private GameObject[] sort(GameObject[] gos)
    {
        int i, j, min;
        float x;
        GameObject val;
        Debug.Log("hello hello");

        Vector3 pos = transform.position;

        for (i = 0; i < gos.Length; i++)
        {
            min = i;
            val = gos[i];
            x = (val.transform.position - pos).sqrMagnitude;
            Debug.Log("x: " + x);
            if (x <= distance)
            {
                
                for (j = i + 1; j < gos.Length; j++)
                {
                    if (x > (gos[j].transform.position - transform.position).sqrMagnitude)
                    {
                        min = j;
                    }
                }
                if(min == i) //not too sure how to solve this problem where the first item is always closest to itself
                {
                    Debug.Log("min == i");
                    return gos;
                }
                Debug.Log("gos[i], gos[min]: " + gos[i] + "  " + gos[min]);

                gos[i] = gos[min];
                gos[min] = val;
                pos = gos[i].transform.position;

                Debug.Log("gos[i], gos[min]: " + gos[i] + "  " + gos[min]);
            }
            else
            {
                for(; i < gos.Length; i++)
                {
                    gos[i] = null;
                }
            }
        }
        return gos;
    }
     // this is the old way I used to do it 
        private GameObject FindClosestTarget(GameObject[] gos)
        {

            GameObject closest = null;
            float tempDistance = distance;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                if (alreadyCollided.Find(go.GetInstanceID()) == null)
                {

                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;

                    if (curDistance < tempDistance)
                    {
                        closest = go;
                        tempDistance = curDistance;
                    }
                }
            }
            if (closest != null)
            {
                alreadyCollided.AddFirst(closest.GetInstanceID());
            }
            //Debug.Log("closest target: " + closest);
            return closest;
        }
    

    private Vector3 Randomize(Vector3 newVector, float devation)
    {
        newVector += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * devation;
        newVector.Normalize();
        return newVector;
    }
}
