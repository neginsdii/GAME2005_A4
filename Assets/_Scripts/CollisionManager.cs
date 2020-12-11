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
        actors = FindObjectsOfType<CubeBehaviour>();
        sp_actors= FindObjectsOfType<SphereBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("scale : "+ a.transform.localScale);
        if(distance<a.size.x/2)
		{
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
            }
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
                b.sp_contacts.Add(a);
                b.isColliding = true;
            }
        }
        else
        {
            if (b.sp_contacts.Contains(a))
            {
                b.sp_contacts.Remove(a);
                b.isColliding = false;
            }
        }
    }
}
