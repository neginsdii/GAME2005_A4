using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] actors;
    public SphereBehavior[] sp_actors;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        actors = FindObjectsOfType<CubeBehaviour>();

        sp_actors = FindObjectsOfType<SphereBehavior>();
        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if (i != j)
                {
                    CheckAABBs(actors[i], actors[j]);
                }
            }
        }

        for (int i = 0; i < sp_actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {

                CheckAABBSphere(sp_actors[i], actors[j]);

            }
        }
        for (int i = 0; i < sp_actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {

                CheckAABBSphere1(sp_actors[i], actors[j]);

            }
        }

        for (int i = 0; i < sp_actors.Length; i++)
        {
            for (int j = 0; j < sp_actors.Length; j++)
            {
                if (i != j)
                {
                    CheckSphereSphere(sp_actors[i], sp_actors[j]);
                }
            }
        }

        sp_actors = null;
    }

    public static void CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;

            }
            //if ((a.min.x <= b.max.x && a.max.x >= b.min.x))
            //{
            //    a.transform.position.Set(b.min.x - a.size.x, a.transform.position.y, a.transform.position.z);
            //    Debug.Log("MI X" + a.min.x);
            //}
            if ((a.min.y <= b.max.y && a.max.y >= b.min.y))
            {
                a.isBottom = true;
                a.transform.position.Set(a.transform.position.x, b.min.y + a.size.y / 2, a.transform.position.z);
                Debug.Log("MIN Y" + a.min.y);
            }
            else
            {
                a.isBottom = false;
            }
            //if (a.min.z <= b.max.z && a.max.z >= b.min.z)
            //{
            //    a.transform.position.Set(a.transform.position.x, a.transform.position.y, b.min.z - b.size.x);
            //}
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
            }

        }
    }

    public static void CheckAABBSphere(SphereBehavior a, CubeBehaviour b)
    {
        var x = Mathf.Max(b.min.x, Mathf.Min(a.transform.position.x, b.max.x));
        var y = Mathf.Max(b.min.y, Mathf.Min(a.transform.position.y, b.max.y));
        var z = Mathf.Max(b.min.z, Mathf.Min(a.transform.position.z, b.max.z));

        var distance = Mathf.Sqrt((x - a.transform.position.x) * (x - a.transform.position.x) +
                                  (y - a.transform.position.y) * (y - a.transform.position.y) +
                                  (z - a.transform.position.z) * (x - a.transform.position.z));

        if (distance < a.size.x / 2)
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
                Debug.Log("collision ");
                if (b.isGround)
                {
                    a.direction.Set(a.direction.x, -a.direction.y, a.direction.z);
                }
                else
                {
                    a.direction = (((a.mass - b.mass) * a.velocity) / (a.mass + b.mass)) + ((2*b.mass*b.velocity)/(a.mass+b.mass));
                    a.speed = a.direction.magnitude;
                    if(a.speed !=0)
                    a.direction.Set(a.direction.x/a.direction.magnitude, a.direction.y / a.direction.magnitude, a.direction.z / a.direction.magnitude);


                    //a.collisionNormal.x = (a.transform.position.x - x);
                    //a.collisionNormal.y = (a.transform.position.x - y);
                    //a.collisionNormal.z = (a.transform.position.x - z);
                    //if (a.collisionNormal.magnitude != 0)
                    //    a.collisionNormal.Set(a.collisionNormal.x / a.collisionNormal.magnitude, a.collisionNormal.y / a.collisionNormal.magnitude, a.collisionNormal.z / a.collisionNormal.magnitude);

                    //a.relativeVelocity.Set(b.velocity.x - a.velocity.x, b.velocity.y - a.velocity.y, b.velocity.z - a.velocity.z);

                    //a.collisionRestitution = Mathf.Min(a.coefficientOfRestitution, b.coefficientOfRestitution);

                    //a.impulse = ((1 + a.collisionRestitution) * (Vector3.Dot(a.relativeVelocity, a.collisionNormal))) / ((1 / a.mass) + (1 / b.mass));
                    //a.t = a.relativeVelocity - (Vector3.Dot(a.relativeVelocity, a.collisionNormal) * a.collisionNormal);
                    //a.jt = ((1 + a.collisionRestitution) * (Vector3.Dot(a.relativeVelocity, a.t))) / ((1 / a.mass) + (1 / b.mass));
                    //float friction = Mathf.Sqrt(b.coFriction * a.coFriction);
                    //a.jt = Mathf.Max(a.jt, -1 * a.impulse * friction);
                    //a.jt = Mathf.Min(a.jt, a.impulse * friction);
                }
               
            }
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
               // a.impulse = 0.0f;
            }
        }
    }
    public static void CheckAABBSphere1(SphereBehavior a, CubeBehaviour b)
    {
        var x = Mathf.Max(b.min.x, Mathf.Min(a.transform.position.x, b.max.x));
        var y = Mathf.Max(b.min.y, Mathf.Min(a.transform.position.y, b.max.y));
        var z = Mathf.Max(b.min.z, Mathf.Min(a.transform.position.z, b.max.z));

        var distance = Mathf.Sqrt((x - a.transform.position.x) * (x - a.transform.position.x) +
                                  (y - a.transform.position.y) * (y - a.transform.position.y) +
                                  (z - a.transform.position.z) * (x - a.transform.position.z));
        //  Debug.Log("Collider Center : " + a.transform.position.x);
        if (distance < a.size.x / 2)
        {
            if (!b.sp_contacts.Contains(a))
            {

                b.direction = (((b.mass - a.mass) * b.velocity) / (b.mass + a.mass)) + ((2 * a.mass * a.velocity) / (b.mass + a.mass));
                b.speed = b.direction.magnitude;
                if (a.speed != 0)
                b.direction.Set(b.direction.x / b.direction.magnitude, b.direction.y / b.direction.magnitude, b.direction.z / b.direction.magnitude);


                b.sp_contacts.Add(a);
                b.isColliding = true;
                //b.collisionNormal.x = (x - a.transform.position.x);
                //b.collisionNormal.y = (y - a.transform.position.y);
                //b.collisionNormal.z = (z - a.transform.position.z);
                //if (b.collisionNormal.magnitude != 0)
                //    b.collisionNormal.Set(b.collisionNormal.x / b.collisionNormal.magnitude, b.collisionNormal.y / b.collisionNormal.magnitude, b.collisionNormal.z / b.collisionNormal.magnitude);

                //b.relativeVelocity.Set(a.velocity.x - b.velocity.x, a.velocity.y - b.velocity.y, a.velocity.z - b.velocity.z);
                //b.collisionRestitution = Mathf.Min(b.coefficientOfRestitution, a.coefficientOfRestitution);
                //b.t = b.relativeVelocity - (Vector3.Dot(b.relativeVelocity, b.collisionNormal) * b.collisionNormal);

                //b.impulse = ((1 + b.collisionRestitution) * (Vector3.Dot(b.relativeVelocity, b.collisionNormal))) / ((1 / a.mass) + (1 / b.mass));
                //b.jt = ((1 + b.collisionRestitution) * (Vector3.Dot(b.relativeVelocity, b.t))) / ((1 / a.mass) + (1 / b.mass));
                //float friction = Mathf.Sqrt(b.coFriction * a.coFriction);
                //b.jt = Mathf.Max(b.jt, -1 * b.impulse * friction);
                //b.jt = Mathf.Min(b.jt, b.impulse * friction);
            }
        }
        else
        {
            if (b.sp_contacts.Contains(a))
            {
                b.sp_contacts.Remove(a);
                b.isColliding = false;
              //  b.impulse = 0.0f;
            }
        }
    }
    public static void CheckSphereSphere(SphereBehavior a, SphereBehavior b)
    {

        var distance = Mathf.Sqrt((b.transform.position.x - a.transform.position.x) * (b.transform.position.x - a.transform.position.x) +
                                  (b.transform.position.y - a.transform.position.y) * (b.transform.position.y - a.transform.position.y) +
                                  (b.transform.position.z - a.transform.position.z) * (b.transform.position.z - a.transform.position.z));
        //  Debug.Log("Collider Center : " + a.transform.position.x);
        if (distance < (a.size.x / 2 + b.size.x / 2))
        {
            if (!a.sp_contacts.Contains(b))
            {

                a.direction = (((a.mass - b.mass) * a.velocity) / (a.mass + b.mass)) + ((2 * b.mass * b.velocity) / (a.mass + b.mass));
                a.speed = a.direction.magnitude;
                if (a.speed != 0)
                a.direction.Set(a.direction.x / a.direction.magnitude, a.direction.y / a.direction.magnitude, a.direction.z / a.direction.magnitude);


                a.sp_contacts.Add(b);
                a.isColliding = true;
                // calculate the collision vector
                //a.collisionNormal.x = (a.transform.position.x - b.transform.position.x);
                //a.collisionNormal.y = (a.transform.position.y - b.transform.position.y);
                //a.collisionNormal.z = (a.transform.position.z - b.transform.position.z);
                //// normalize the collision vector
                //if (a.collisionNormal.magnitude != 0)
                //    a.collisionNormal.Set(a.collisionNormal.x / a.collisionNormal.magnitude, a.collisionNormal.y / a.collisionNormal.magnitude, a.collisionNormal.z / a.collisionNormal.magnitude);
                //a.relativeVelocity.Set(b.velocity.x - a.velocity.x, b.velocity.y - a.velocity.y, b.velocity.z - a.velocity.z);
                //a.collisionRestitution = Mathf.Min(a.coefficientOfRestitution, b.coefficientOfRestitution);
                //// calculate the impulse

                //a.impulse = ((1 + a.collisionRestitution) * (Vector3.Dot(a.relativeVelocity, a.collisionNormal))) / ((1 / a.mass) + (1 / b.mass));
                //a.t = a.relativeVelocity - (Vector3.Dot(a.relativeVelocity, a.collisionNormal) * a.collisionNormal);
                //a.jt = ((1 + a.collisionRestitution) * (Vector3.Dot(a.relativeVelocity, a.t))) / ((1 / a.mass) + (1 / b.mass));
                //float friction = Mathf.Sqrt(b.coFriction * a.coFriction);
                //a.jt = Mathf.Max(a.jt, -1 * a.impulse * friction);
                //a.jt = Mathf.Min(a.jt, a.impulse * friction);
            }
        }
        else
        {
            if (a.sp_contacts.Contains(b))
            {
                a.sp_contacts.Remove(b);
                a.isColliding = false;
               // a.impulse = 0.0f;
            }

        }
    }

}
