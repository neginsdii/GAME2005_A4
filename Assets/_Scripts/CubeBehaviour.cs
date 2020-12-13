 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;


[System.Serializable]
public class CubeBehaviour : MonoBehaviour
{
    private Vector3 gravity;
    public float speed;
    public Vector3 direction;
    public float mass;
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public List<CubeBehaviour> contacts;
    public List<SphereBehavior> sp_contacts;
    public MeshFilter meshFilter;
    public Bounds bounds;
    public Vector3 velocity;
    public Vector3 relativeVelocity;
    private Vector3 acceleration;
    public bool isPlane;
    public Vector3 collisionNormal;
    public float coefficientOfRestitution;
    public float collisionRestitution;
    public float impulse;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        acceleration.Set(0.0f, 0.0f, 0.0f);
        velocity.Set(0.0f, 0.0f, 0.0f);
        relativeVelocity.Set(0.0f, 0.0f, 0.0f);
        bounds = meshFilter.mesh.bounds;
        size = bounds.size;
        gravity.Set(0.0f, -9.8f, 0.0f);
        collisionNormal.Set(0.0f, 0.0f, 0.0f);
        coefficientOfRestitution = 0.2f;
        collisionRestitution = 0.0f;
        impulse = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlane)
        {
            if (impulse != 0)
            {
                speed = impulse / mass;
                direction = collisionNormal;
                // velocity = velocity + (impulse / mass) * collisionNormal;
            }

            acceleration += (gravity + acceleration) * Time.deltaTime*1/10;
            velocity = speed * direction + acceleration;
            if(contacts.Count >0)
            { velocity.Set(0.0f, 0.0f, 0.0f); }
            transform.position += velocity * Time.deltaTime;
        }
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
    }
}
