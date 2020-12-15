using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

[System.Serializable]
public class SphereBehavior : MonoBehaviour
{
	public float speed;
	public Vector3 direction;
	public float mass;
	public Vector3 size;
	public Transform can;
	public bool isColliding;
	public Vector3 gravity;
	public Vector3 velocity;
	public Vector3 relativeVelocity;
	private float startTime;
	private Vector3 acceleration;
	public Vector3 collisionNormal;
	public float coefficientOfRestitution;
	public float collisionRestitution;
	public float impulse;
	public List<SphereBehavior> sp_contacts;
	public List<CubeBehaviour> contacts;
	public bool showGizmos;
	public float coFriction;
	public Vector3 t;
	public float jt;
	// Start is called before the first frame update
	void Start()
	{
		acceleration.Set(0.0f, 0.0f, 0.0f);
		velocity.Set(0.0f, 0.0f, 0.0f);
		relativeVelocity.Set(0.0f, 0.0f, 0.0f);
		startTime = Time.time;
		collisionNormal.Set(0.0f, 0.0f, 0.0f);
		coefficientOfRestitution = 0.7f;
		collisionRestitution = 0.0f;
		impulse = 0.0f;
		coFriction = 0.8f;
	}
	// Update is called once per frame
	void Update()
	{
		//if (impulse != 0)
		//{
		//	speed = jt / mass;
		//	//Debug.Break();
		//	direction = collisionNormal;


		//	// velocity = velocity + (impulse / mass) * collisionNormal;
		//}
		//acceleration += (gravity + acceleration) * Time.deltaTime * 1 / 5;
		//if (velocity.magnitude != 0)
		velocity = speed * direction + acceleration;
		transform.position += velocity * Time.deltaTime;
		_BackToPool();

	}

	private void OnDrawGizmos()
	{
		if (showGizmos)
		{
			Gizmos.color = Color.magenta;
			//Debug.Log("size"+ size.x / 2);
			Gizmos.DrawWireSphere(transform.position, size.x / 2);
		}
	}
	private void _BackToPool()
	{
		if (Time.time - startTime > 5.0f)
		{
			Debug.Log("scale : ");
			gameObject.SetActive(false);
			acceleration.Set(0.0f, 0.0f, 0.0f);
			velocity.Set(0.0f, 0.0f, 0.0f);
			startTime = Time.time;
			relativeVelocity.Set(0.0f, 0.0f, 0.0f);
			//startTime = Time.time;
			collisionNormal.Set(0.0f, 0.0f, 0.0f);
			coefficientOfRestitution = 0.7f;
			collisionRestitution = 0.0f;
			direction.Set(0.0f, 0.0f, 0.0f);
			impulse = 0.0f;
			speed = 8.0f;
			//direction = can.up;
		}
	}
}
