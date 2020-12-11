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

	public List<CubeBehaviour> contacts;
	// Start is called before the first frame update
	void Start()
	{

		direction = can.up;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += speed * direction * Time.deltaTime;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		//Debug.Log("size"+ size.x / 2);
		Gizmos.DrawWireSphere(transform.position, size.x/2);
	}
}
