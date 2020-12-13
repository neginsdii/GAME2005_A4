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
	private float startTime;
	private Vector3 acceleration;

	public List<SphereBehavior> sp_contacts;
	public List<CubeBehaviour> contacts;
	// Start is called before the first frame update
	void Start()
	{

		acceleration.Set(0.0f, 0.0f, 0.0f);
		velocity.Set(0.0f, 0.0f, 0.0f);
		startTime = Time.time;

	}
	// Update is called once per frame
	void Update()
	{
		
		acceleration += (gravity + acceleration) * Time.deltaTime*1/5;
		velocity = speed * direction + acceleration;
		transform.position += velocity * Time.deltaTime;
	   _BackToPool();
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		//Debug.Log("size"+ size.x / 2);
		Gizmos.DrawWireSphere(transform.position, size.x/2);
	}
	private void _BackToPool()
	{
		if (Time.time - startTime>5.0f)
		{
			Debug.Log("scale : ");
			gameObject.SetActive(false);
            acceleration.Set(0.0f, 0.0f, 0.0f);
            velocity.Set(0.0f, 0.0f, 0.0f);
			startTime = Time.time;
            //direction = can.up;
        }
	}
}
