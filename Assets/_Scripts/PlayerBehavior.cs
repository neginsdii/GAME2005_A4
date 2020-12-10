using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBehavior : MonoBehaviour
{

    public Transform BulletSpawn;
    public GameObject bullet;
    public int FireRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
    }

    private void _Fire()
	{
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {

            if (Time.frameCount % FireRate == 0)
            {
                Instantiate(bullet, BulletSpawn.position, Quaternion.identity);
                Debug.Log("Fire!!");
            }
        }
    }
}
