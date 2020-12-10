using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

[System.Serializable]
public class SphereBehavior : MonoBehaviour
{
	public MeshFilter meshFilter;
	public Bounds bounds;
	public Vector3 size;
	public Vector3 center;
	public bool isColliding;

	public List<CubeBehaviour> contacts;
	// Start is called before the first frame update
	void Start()
	{
		meshFilter = GetComponent<MeshFilter>();
		bounds = meshFilter.mesh.bounds;
		size = bounds.size;
		center = bounds.center;
	}

	// Update is called once per frame
	void Update()
	{
		center = bounds.center;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;

		Gizmos.DrawWireSphere(transform.position, size.x/2);
	}
}
