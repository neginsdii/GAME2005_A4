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

	private Vector3 acceleration; 
	public List<CubeBehaviour> contacts;
	// Start is called before the first frame update
	void Start()
	{
		acceleration.Set(0.0f, 0.0f, 0.0f);
		velocity.Set(0.0f, 0.0f, 0.0f);
		direction = can.up;
	}

	// Update is called once per frame
	void Update()
	{
		acceleration += (gravity + acceleration) * Time.deltaTime;
		velocity = speed * direction + acceleration;
		transform.position += velocity * Time.deltaTime;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		//Debug.Log("size"+ size.x / 2);
		Gizmos.DrawWireSphere(transform.position, size.x/2);
	}
}
